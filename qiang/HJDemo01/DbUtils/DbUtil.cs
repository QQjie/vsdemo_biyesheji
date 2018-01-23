using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace HJDemo01.DbUtils
{
    public class DbUtil
    {
        
        private static MySqlConnection mycon = null;
        
        //连接数据库
       public static void Conn() {

            string constr = "server=huangjie;User Id=root;password=Hj1995815;Database=mydatabase";

            mycon = new MySqlConnection(constr);
            try
            {
                mycon.Open();
            }catch(Exception e){
                
            }
        
        }
        //关闭数据库
        private static void closeConn() {
            mycon.Close();
            
        }
        

    }
}