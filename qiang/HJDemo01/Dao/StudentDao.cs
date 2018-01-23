using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using HJDemo01.DbUtils;
using HJDemo01.Models;


namespace HJDemo01.Dao
{
    public class StudentDao
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
        public static List<Student> queryAll()
        {

            List<Student> list = new List<Student>();        //定义一个学生集合

            MySqlConnection con = StudentDao.Conn();

            string querysql = "select t_id,t_name,t_sex,t_age,t_email from students";

            using (MySqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = querysql;
                MySqlDataAdapter dp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                dp.Fill(ds);
                DataTable table = ds.Tables[0];
                for (int i = 0; i < table.Rows.Count; i++)    //根据表中的记录查询出所有的学生记录并放入集合中
                {
                    DataRow row = table.Rows[i];
                    int ID = Int32.Parse(row[0].ToString());
                    string NAME = row[1].ToString();
                    string SEX = row[2].ToString();
                    int AGE = Int32.Parse(row[3].ToString());
                    string EMAIL = row[4].ToString();
                    Student s = new Student() { Id = ID, Name = NAME, Sex = SEX, Age = AGE, Email = EMAIL };
                    list.Add(s);
                }
            }
            return list;
        }
        #endregion


        #region    查询一个学生  quetyStudent
        /*
        *@return 一个学生记录的集合
        *
        */
        //查询一个学生  
        public static Student queryStudent(int id)
        {

            MySqlConnection con = StudentDao.Conn();
            Student s = new Student();
           
            string querysql = "select t_id,t_name,t_sex,t_age,t_email from students where t_id="+id;

            using (MySqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = querysql;
               
                MySqlDataAdapter dp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                dp.Fill(ds);
                DataTable table = ds.Tables[0];
              
                DataRow row = table.Rows[0];
                s.Id = Int32.Parse(row[0].ToString());
                s.Name = row[1].ToString();
                s.Sex = row[2].ToString();
                s.Age = Int32.Parse(row[3].ToString());
                s.Email = row[4].ToString();
               
                
                
            }
            StudentDao.closeConn();
            return s;
        }
        #endregion

        #region    查询一个学生  quetyStudent
        /*
        *@return 一个学生记录的集合
        *
        */
        //查询所有学生  
        public static Student EditStudent(int id,string name,string sex,string email,int age)
        {

           
            Student s = new Student();

            string querysql = "update students set t_name=?name,t_sex=?sex,t_age=?age,t_email=?email  where t_id=" + id;
            string PARM_NAME = "?name"; string PARM_SEX = "?sex"; string PARM_AGE = "?age"; string PARM_EMAIL = "?EMAIL";

            MySqlParameter[] parms = new MySqlParameter[] { new MySqlParameter(PARM_NAME,MySqlDbType.VarChar,20),
          new MySqlParameter(PARM_SEX,MySqlDbType.VarChar,2), new MySqlParameter(PARM_AGE,MySqlDbType.Int32,4), new MySqlParameter(PARM_EMAIL,MySqlDbType.VarChar,40) };
            parms[0].Value = name;
            parms[1].Value = sex;
            parms[2].Value =age;
            parms[3].Value = email;
            MySqlConnection con = StudentDao.Conn();
            using (MySqlCommand cmd = con.CreateCommand())
            {
              

                cmd.CommandText = querysql;

                foreach(MySqlParameter pram in parms){
                    
                    cmd.Parameters.Add(pram);

                }

                cmd.ExecuteNonQuery();

                StudentDao.closeConn();

            }

            return null;
        }
        #endregion




        /*
         下面附上测试通过的代码： string connStr = WebConfigurationManager. ConnectionStrings["WhateverName"].ConnectionString;
         * MySqlConnection conn = new MySqlConnection(connStr); 
         * if (conn != null) conn.Open(); 
         * else return; string SQL_INSERT_TOPIC ="insert into test values (null,?title,?type,now())"; 
         * string PARM_TITLE ="?title"; string PARM_TYPE ="?type"; 
         * MySqlParameter[] parms = new MySqlParameter[] { new MySqlParameter(PARM_TITLE,MySqlDbType.VarChar,80),
         * new MySqlParameter(PARM_TYPE,MySqlDbType.VarChar,1) };
         * parms[0].Value ="welcome to beijing";
         * parms[1].Value ="C"; 
         * MySqlCommand cmd = new MySqlCommand();
         * cmd.Connection = conn; 
         * cmd.CommandType = CommandType.Text; 
         * cmd.CommandText = SQL_INSERT_TOPIC;
         * foreach (MySqlParameter pram in parms) cmd.Parameters.Add(pram); c
         * md.ExecuteNonQuery(); 
         * conn.Close();

          5.连接本机mysql数据库方法例子如---mysql-connector-odbc
         
         */


        /* internal static object EditStudent(int id, string name, string sex, int age)
         {
             throw new NotImplementedException();
         }*/
    }
}