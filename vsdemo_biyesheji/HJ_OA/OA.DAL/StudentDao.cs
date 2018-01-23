using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using MySql;
using OA.Common;

using OA.Models;


namespace OA.DAL
{
    public class StudentDao : IStudenDao
    {
        private static MySqlConnection mycon = null;

        #region  连接数据库
        public static MySqlConnection Conn()
        {

            string constr = "server=huangjie;User Id=root;password=Hj1995815;Database=mydatabase";

            mycon = new MySqlConnection(constr);

            mycon.Open();

            return mycon;
        }
        #endregion


        #region 关闭数据库

        public static void closeConn()
        {
            mycon.Close();

        }
        #endregion  


        #region    查询所有学生  quetyAll
        /*
        *@return 一个学生记录的集合
        *
        */
        //查询所有学生  
        //public IList<Student> queryAll()
        //{

        //    IList<Student> list = new List<Student>();        //定义一个学生集合

        //    MySqlConnection con = StudentDao.Conn();

        //    string querysql = "SELECT T_ID,T_NAME,T_SEX,T_AGE,T_EMAIL FROM STUDENTS";

        //    using (MySqlCommand cmd = con.CreateCommand())
        //    {
        //        cmd.CommandText = querysql;
        //        MySqlDataAdapter dp = new MySqlDataAdapter(cmd);
        //        DataSet ds = new DataSet();
        //        dp.Fill(ds);
        //        DataTable table = ds.Tables[0];
        //        for (int i = 0; i < table.Rows.Count; i++)    //根据表中的记录查询出所有的学生记录并放入集合中
        //        {
        //            DataRow row = table.Rows[i];
        //            int ID = Int32.Parse(row[0].ToString());
        //            string NAME = row[1].ToString();
        //            string SEX = row[2].ToString();
        //            int AGE = Int32.Parse(row[3].ToString());
        //            string EMAIL = row[4].ToString();
        //            Student s = new Student() { T_Id = ID, T_Name = NAME, T_Sex = SEX, T_Age = AGE, T_Email = EMAIL };   //new 一个student对象
        //            list.Add(s);                         //添加到数组中
        //        }
        //    }
        //    return list;
        //}
         #endregion
        public IList<Student> queryAll()
        {
            string querysql = "SELECT T_Id,T_Name,T_Sex,T_Age,T_Email FROM STUDENTS";

            DataSet dt = OA.Common.MySqlHelper.Query(querysql);
            IList<Student> ls=dt.Tables[0].ToEntity<Student>();
            //IList<Student> ls = OA.Common.MySqlHelper.GetEntityList<Student>(dt.Tables[0]);

            return ls;
        }

      


        #region    根据学生ID 查询一个学生  quetyStudent
        /*
         * @param id 学生id
         * 
        *@return  返回一个学生对象
        *
        */

        //public Student queryStudent(int id)
        //{

        //    MySqlConnection con = StudentDao.Conn();
        //    Student s = new Student();

        //    string querysql = "SELECT T_ID,T_NAME,T_SEX,T_AGE,T_EMAIL FROM STUDENTS WHERE T_ID=" + id;

        //    using (MySqlCommand cmd = con.CreateCommand())
        //    {
        //        cmd.CommandText = querysql;

        //        MySqlDataAdapter dp = new MySqlDataAdapter(cmd);
        //        DataSet ds = new DataSet();
        //        dp.Fill(ds);
        //        DataTable table = ds.Tables[0];
        //        DataRow row = table.Rows[0];
        //        s.Id = Int32.Parse(row[0].ToString());
        //        s.Name = row[1].ToString();
        //        s.Sex = row[2].ToString();
        //        s.Age = Int32.Parse(row[3].ToString());
        //        s.Email = row[4].ToString();

        //       // s=(Student)cmd.ExecuteScalar();
        //    }
        //    StudentDao.closeConn();
        //    return s;
        //}
        #endregion
        public Student queryStudent(int id)
        {
            Student s = new Student();
            string querysql = "SELECT T_Id,T_Name,T_Sex,T_Age,T_Email FROM STUDENTS WHERE T_Id=" + id;
            DataSet ds = OA.Common.MySqlHelper.Query(querysql);
            return ds.Tables[0].ToEntity<Student>()[0];
           
        }
        

        
        #region    编辑学生信息  EditStudent
        /*
         * @param 传入一个学生对象
         * 
         *@return 一个编辑学生后的学生对象
         *
         */
       
        public  Student EditStudent(Student s)
        {
            string querysql = "update students set T_Name=?name,T_Sex=?sex,T_Age=?age,T_Email=?email  where T_Id=" + s.T_Id;
            string PARM_NAME = "?name"; string PARM_SEX = "?sex"; string PARM_AGE = "?age"; string PARM_EMAIL = "?EMAIL";
            MySqlParameter[] parms = new MySqlParameter[] { new MySqlParameter(PARM_NAME,MySqlDbType.VarChar,20),
            new MySqlParameter(PARM_SEX,MySqlDbType.VarChar,2), new MySqlParameter(PARM_AGE,MySqlDbType.Int32,4), new MySqlParameter(PARM_EMAIL,MySqlDbType.VarChar,40) };
            parms[0].Value = s.T_Name;
            parms[1].Value = s.T_Sex;
            parms[2].Value =s.T_Age;
            parms[3].Value = s.T_Email;
            //MySqlConnection con = StudentDao.Conn();
            //using (MySqlCommand cmd = con.CreateCommand())
            //{
            //    cmd.CommandText = querysql;
            //    foreach(MySqlParameter pram in parms){
            //        cmd.Parameters.Add(pram);
            //    }
            //    cmd.ExecuteNonQuery();
            //    StudentDao.closeConn();
            //}
            OA.Common.MySqlHelper.ExecuteSql(querysql, parms);
            return null;
        }
        #endregion

        #region 新增加一个学生
        /*
         * @param 传入一个学生对象的参数 Student
         * 
         * @return 返回一个学生对象
         *
         */
        public  Student createStudent(Student s) {
            string querysql = "INSERT INTO STUDENTS VALUES(0,?name,?sex,?age,?email)";
            string PARM_NAME = "?name"; string PARM_SEX = "?sex"; string PARM_AGE = "?age"; string PARM_EMAIL = "?EMAIL";

            MySqlParameter[] parms = new MySqlParameter[] { new MySqlParameter(PARM_NAME,MySqlDbType.VarChar,20),
            new MySqlParameter(PARM_SEX,MySqlDbType.VarChar,2), new MySqlParameter(PARM_AGE,MySqlDbType.Int32,4), new MySqlParameter(PARM_EMAIL,MySqlDbType.VarChar,40) };
            parms[0].Value = s.T_Name;
            parms[1].Value = s.T_Sex;
            parms[2].Value = s.T_Age;
            parms[3].Value = s.T_Email;

            //MySqlConnection con = StudentDao.Conn();
            //using (MySqlCommand cmd = con.CreateCommand())
            //{

            //    cmd.CommandText = querysql;

            //    foreach (MySqlParameter pram in parms)
            //    {

            //        cmd.Parameters.Add(pram);

            //    }

            //    cmd.ExecuteNonQuery();

            //    StudentDao.closeConn();

            //s
            OA.Common.MySqlHelper.ExecuteSql(querysql, parms);
            return null;
                                }



        #endregion


        #region  删除一个学生
        /*
         * @param 传入一个学生对象的参数的id Student
         * 
         * @return 返回一个学生对象
         *
         */
        public  Student deleteStudent(int id)
        {
            string querysql = "DELETE FROM STUDENTS WHERE T_Id=?id";
            string PARM_ID = "?id";

            MySqlParameter parm = new MySqlParameter(PARM_ID, MySqlDbType.Int32);
            parm.Value = id;

            //MySqlConnection con = StudentDao.Conn();
            //using (MySqlCommand cmd = con.CreateCommand())
            //{

            //    cmd.CommandText = querysql;

            //    cmd.Parameters.Add(parm);

            //    cmd.ExecuteNonQuery();

            //    StudentDao.closeConn();

            //}
            OA.Common.MySqlHelper.ExecuteSql(querysql, parm);
            return null;
        }

        #endregion






       
    }
}