using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using MySql.Data.MySqlClient;
//using PES.Agora.Utilities;
//using PES.Agora.Global.Config;

namespace OA.Common
{

    public  class MySqlHelper
    {
        #region 数据库连接字符串
        private static readonly string _conn = string.Empty;

        /// <summary>
        /// 设置连接信息
        /// </summary>
        static MySqlHelper()
        {

            _conn = "server=huangjie;User Id=root;password=Hj1995815;Database=biyesheji";
            //_conn = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
           // _conn = string.Format("server={0};Password={1};User ID={2};database={3};charset=utf8;Convert Zero Datetime=True;port={4};Allow User Variables=True;",
           // GlobalConfiguration.Configuration["DBIP"], GlobalConfiguration.Configuration["DBPsw"]
           // , GlobalConfiguration.Configuration["DBUser"], GlobalConfiguration.Configuration["MainSiteDBName"], GlobalConfiguration.Configuration["DBPort"]
           // );
            //_conn = string.Format("server={0};Password={1};User ID={2};database={3};charset=utf8;Convert Zero Datetime=True;port={4}",
            //        GlobalConfiguration.Configuration["MainSiteDBIP"], GlobalConfiguration.Configuration["MainSiteDBPsw"]
            //        , GlobalConfiguration.Configuration["MainSiteDBUser"], GlobalConfiguration.Configuration["MainSiteDBName"], GlobalConfiguration.Configuration["MainSiteDBUser"]
            //        );

        }

        static string Conn
        {
            get { return _conn; }
        }

        private static string _databaseName = string.Empty;

        public static string DatabaseName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_databaseName))
                {
                    using (MySqlConnection conn = new MySqlConnection(Conn))
                    {
                        
                        _databaseName = conn.Database;
                        if (string.IsNullOrWhiteSpace(_databaseName))
                        {
                            _databaseName = "mydatabase";
                        }
                    }
                }

                return _databaseName;
            }
        }

        #endregion

        #region PrepareCommand
        /// <summary>
        /// Command预处理
        /// </summary>
        /// <param name="conn">MySqlConnection对象</param>
        /// <param name="trans">MySqlTransaction对象，可为null</param>
        /// <param name="cmd">MySqlCommand对象</param>
        /// <param name="cmdType">CommandType，存储过程或命令行</param>
        /// <param name="cmdText">SQL语句或存储过程名</param>
        /// <param name="cmdParms">MySqlCommand参数数组，可为null</param>
        private static void PrepareCommand(MySqlConnection conn, MySqlTransaction trans, MySqlCommand cmd, CommandType cmdType, string cmdText, MySqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                foreach (MySqlParameter parm in cmdParms)
                {
                    parm.ParameterName = parm.ParameterName.Replace("@", "?");
                    cmd.Parameters.Add(parm);
                }
            }
        }

        #endregion

        #region 公用方法

        public static int GetMaxID(string FieldName, string TableName)
        {
            string strsql = "select max(" + FieldName + ")+1 from " + TableName;
            object obj = GetSingle(strsql);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }

        public static bool Exists(string strSql)
        {
            object obj = GetSingle(strSql.Replace("@", "?"));
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool Exists(string strSql, params MySqlParameter[] cmdParms)
        {
            object obj = GetSingle(strSql.Replace("@", "?"), cmdParms);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

        #region  执行简单SQL语句
        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString)
        {
            SQLString = SQLString.Replace("@", "?");
            SQLString = InjectTest(SQLString);
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                using (MySqlCommand cmd = new MySqlCommand(SQLString, conn))
                {
                    try
                    {
                        conn.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (MySql.Data.MySqlClient.MySqlException e)
                    {
                        if (conn.State == ConnectionState.Connecting)
                            conn.Close();
                        throw e;
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>		
        public static void ExecuteSqlTran(ArrayList SQLStringList)
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                MySqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n].ToString().Replace("@", "?");
                        strsql = InjectTest(strsql);
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
                catch (MySql.Data.MySqlClient.MySqlException e)
                {
                    tx.Rollback();
                    if (conn.State == ConnectionState.Connecting)
                        conn.Close();
                    throw e;
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString)
        {
            SQLString = SQLString.Replace("@", "?");
            SQLString = InjectTest(SQLString);
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                using (MySqlCommand cmd = new MySqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (MySql.Data.MySqlClient.MySqlException e)
                    {
                        if (connection.State == ConnectionState.Connecting)
                            connection.Close();
                        throw e;
                    }
                }
            }
        }


        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString)
        {
            SQLString = SQLString.Replace("@", "?");
            SQLString = InjectTest(SQLString);
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    MySqlDataAdapter command = new MySqlDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                   
                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    if (connection.State == ConnectionState.Connecting)
                        connection.Close();
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }

        public static DataSet Query(string SQLString, string TableName)
        {
            SQLString = SQLString.Replace("@", "?");
            SQLString = InjectTest(SQLString);
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    MySqlDataAdapter command = new MySqlDataAdapter(SQLString, connection);
                    command.Fill(ds, TableName);
                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    if (connection.State == ConnectionState.Connecting)
                        connection.Close();
                    throw ex;
                }
                return ds;
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet,设置命令的执行等待时间
        /// </summary>
        /// <param name="SQLString"></param>
        /// <param name="Times"></param>
        /// <returns></returns>
        public static DataSet Query(string SQLString, int Times)
        {
            SQLString = SQLString.Replace("@", "?");
            SQLString = InjectTest(SQLString);
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    MySqlDataAdapter command = new MySqlDataAdapter(SQLString, connection);
                    command.SelectCommand.CommandTimeout = Times;
                    command.Fill(ds, "ds");
                }
                catch (MySql.Data.MySqlClient.MySqlException e)
                {
                    if (connection.State == ConnectionState.Connecting)
                        connection.Close();
                    throw new Exception(e.Message);
                }
                return ds;
            }
        }
        #endregion


        /// <summary>
        /// 将DataTable转成实成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static T GetEntity<T>(DataTable table) where T : new()
        {
            T entity = new T();
            foreach (DataRow row in table.Rows)
            {
                foreach (var item in entity.GetType().GetProperties())
                {
                    if (row.Table.Columns.Contains(item.Name))
                    {
                        if (DBNull.Value != row[item.Name])
                        {
                            item.SetValue(entity, Convert.ChangeType(row[item.Name], item.PropertyType), null);
                        }
                    }
                }
            }
            return entity;
        }
        /// <summary>
        /// 将DataTable转成实成具有泛型对象的集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static IList<T> GetEntityList<T>(DataTable table) where T : new()
        {
            IList<T> list = new List<T>();
            foreach (DataRow row in table.Rows)
            {
                T entity = new T();
                foreach (var item in entity.GetType().GetProperties())
                {
                    if (row.Table.Columns.Contains(item.Name))
                    {
                        if (DBNull.Value != row[item.Name])
                        {
                            item.SetValue(entity, Convert.ChangeType(row[item.Name], item.PropertyType), null);
                        }
                    }
                }
                list.Add(entity);
            }
            return list;
        }

        #region 执行带参数的SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, params MySqlParameter[] cmdParms)
        {
            SQLString = SQLString.Replace("@", "?");
            SQLString = InjectTest(SQLString);
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (MySql.Data.MySqlClient.MySqlException e)
                    {
                        if (connection.State == ConnectionState.Connecting)
                            connection.Close();
                        throw e;
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的MySqlParameter[]）</param>
        public static void ExecuteSqlTran(Hashtable SQLStringList)
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    MySqlCommand cmd = new MySqlCommand();
                    try
                    {
                        //循环
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString().Replace("@", "?");

                            cmdText = InjectTest(cmdText);
                            MySqlParameter[] cmdParms = (MySqlParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        if (conn.State == ConnectionState.Connecting)
                            conn.Close();
                        throw e;
                    }
                }
            }
        }

        /// <summary>
        /// 执行2条SQL语句，得到刚刚insert后的主键ID。
        /// </summary>
        /// <param name="SQLString"></param>
        /// <param name="SQLCount"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static object ExecuteSqlList(string SQLString, string SQLCount , params MySqlParameter[] cmdParms)
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    MySqlCommand cmd = new MySqlCommand();
                    try
                    {
                        SQLString = SQLString.Replace("@", "?");
                        SQLString = InjectTest(SQLString);
                        PrepareCommand(cmd, conn, trans, SQLString, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        MySqlCommand cmmd = new MySqlCommand(SQLCount,conn);
                        object obj = cmmd.ExecuteScalar();
                        trans.Commit();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        if (conn.State == ConnectionState.Connecting)
                            conn.Close();
                        throw e;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SQLStringList"></param>
        public static void ExecuteSqlTran(List<KeyValuePair<string, MySqlParameter[]>> SQLStringList)
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    MySqlCommand cmd = new MySqlCommand();
                    try
                    {
                        //循环
                        foreach (KeyValuePair<string, MySqlParameter[]> myValuePair in SQLStringList)
                        {
                            string cmdText = myValuePair.Key.ToString().Replace("@", "?");

                            cmdText = InjectTest(cmdText);
                            MySqlParameter[] cmdParms = (MySqlParameter[])myValuePair.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }

                        trans.Commit();
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        if (conn.State == ConnectionState.Connecting)
                            conn.Close();
                        throw e;
                    }
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）,无数据时返回null</returns>
        public static object GetSingle(string SQLString, params MySqlParameter[] cmdParms)
        {
            SQLString = SQLString.Replace("@", "?");
            SQLString = InjectTest(SQLString);
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (MySql.Data.MySqlClient.MySqlException e)
                    {
                        if (connection.State == ConnectionState.Connecting)
                            connection.Close();
                        throw e;
                    }
                }
            }
        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString, params MySqlParameter[] cmdParms)
        {
            SQLString = SQLString.Replace("@", "?");
            SQLString = InjectTest(SQLString);
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                MySqlCommand cmd = new MySqlCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (MySql.Data.MySqlClient.MySqlException ex)
                    {
                        if (connection.State == ConnectionState.Connecting)
                            connection.Close();
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }



        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString, int StartIndex, int PageSize, params MySqlParameter[] cmdParms)
        {
            SQLString = SQLString.Replace("@", "?");
            SQLString = InjectTest(SQLString);
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                MySqlCommand cmd = new MySqlCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, StartIndex, PageSize, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (MySql.Data.MySqlClient.MySqlException ex)
                    {
                        if (connection.State == ConnectionState.Connecting)
                            connection.Close();
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }


        /// <summary>
        /// 执行存储过程，返回DataSet
        /// by cat 20161125
        /// </summary>
        /// <param name="ProName">存储过程名</param>
        /// <param name="cmdParms">参数</param>
        /// <returns></returns>
        public static DataSet QueryStoredProcedure(string ProName, params MySqlParameter[] cmdParms)
        {
            
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                MySqlCommand cmd = new MySqlCommand();
                PrepareCommand(connection, null, cmd, CommandType.StoredProcedure, ProName, cmdParms);
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (MySql.Data.MySqlClient.MySqlException ex)
                    {
                        if (connection.State == ConnectionState.Connecting)
                            connection.Close();
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }


        public static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, string cmdText, MySqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmdText = InjectTest(cmdText);
            cmd.CommandText = cmdText.Replace("@", "?"); ;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (MySqlParameter parameter in cmdParms)
                {
                    parameter.ParameterName = parameter.ParameterName.Replace("@", "?");
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }

        #endregion

        #region 参数转换
        /// <summary>
        /// 放回一个SQLiteParameter
        /// </summary>
        /// <param name="name">参数名字</param>
        /// <param name="type">参数类型</param>
        /// <param name="size">参数大小</param>
        /// <param name="value">参数值</param>
        /// <returns>SQLiteParameter的值</returns>
        public static MySqlParameter MakeSQLiteParameter(string name, MySqlDbType type, int size, object value)
        {
            MySqlParameter parm = new MySqlParameter(name, type, size);
            parm.Value = value;
            return parm;
        }

        public static MySqlParameter MakeSQLiteParameter(string name, DbType type, object value)
        {
            MySqlParameter parm = new MySqlParameter(name, type);
            parm.Value = value;
            return parm;
        }

        /// <summary>
        /// 防注入字符串处理
        /// </summary>
        /// <param name="SqlString">原SQL语句</param>
        /// <returns>转换后的语句</returns>
        public static string InjectTest(string SqlString)
        {
            string result = SqlString;
            List<string> InjectKeys = new List<string>() { ";", "\\", "\"" };
            foreach (var item in InjectKeys)
            {
                result = result.Replace(item, "");
            }
            return result;
        }
        #endregion
    }
}
