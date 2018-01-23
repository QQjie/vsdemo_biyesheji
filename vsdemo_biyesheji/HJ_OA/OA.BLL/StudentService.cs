using OA.DAL;
using OA.DALFactory;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.BLL
{
    public class StudentService
    {
        StudentDal stuDal = new StudentDal();

        #region 根据学生的学号查询学生对象
        /// <summary>
        /// 根据学生的学号返回学生对象
        /// </summary>
        /// <param name="id">学生学号</param>
        /// <returns>学生对象</returns>
        public Student GetStudentByNum(string num)
        {
            string stunum = num.Trim();
            Student s = new Student();
            IQueryable<Student> stuiq = stuDal.GetEntities(stu => stu.Stu_Num == stunum);
            if (stuiq.Count() == 0)
            {
                return null;
            }
            foreach (var item in stuiq)
            {
                s = item;
            }
            return s;
        }
        #endregion

        #region 根据学生的ID查询学生对象
        /// <summary>
        /// 根据学生的ID返回学生对象
        /// </summary>
        /// <param name="num">学生ID</param>
        /// <returns>学生对象</returns>
        public Student GetStudentById(int id)
        {
            Student s = new Student();
            IQueryable<Student> stuiq = stuDal.GetEntities(stu => stu.Stu_Id == id);
            if (stuiq.Count() == 0)
            {
                return null;
            }
            foreach (var item in stuiq)
            {
                s = item;
            }
            return s;
        }
        #endregion

        #region 根据学生的名字查询学生对象
        /// <summary>
        /// 根据学生的名字查询学生对象
        /// </summary>
        /// <param name="name">传入学生的名字</param>
        /// <returns>返回学生对象</returns>
        public Student GetStudentByName(string name) 
        {
            string stuname = name.Trim();
            Student s = new Student();
            IQueryable<Student> stuiq = stuDal.GetEntities(stu => stu.Stu_Num == stuname);
            if (stuiq.Count() == 0)
            {
                return null;
            }
            foreach (var item in stuiq)
            {
                s = item;
            }
            return s;
        }
        #endregion

        #region 学生的登录验证
        /// <summary>
        /// 学生的登录验证
        /// </summary>
        /// <param name="stunum">输入的学生学号</param>
        /// <param name="pwd">输入的学生密码</param>
        /// <returns>验证通过返回一个学对象否则返回一个null</returns>
        public Student StudentLoginByNumAndPwd(string stunum,string pwd) 
        {
            Student s = new Student();
            IQueryable<Student> stuiq = stuDal.GetEntities(stu => stu.Stu_Num == stunum && stu.Stu_Pwd == pwd);
            if (stuiq.Count()==0)
	         {
		          return null;  
	         }
            foreach (var item in stuiq)
            {
                s = item;
            }
            return s;
        }
        #endregion

        #region 修改学生密码
        /// <summary>
        /// 修改学生密码
        /// </summary>
        /// <param name="stu">传入的学生对象</param>
        /// <param name="newPwd">修改的新密码</param>
        /// <returns>true修改成功 false修改失败</returns>
        public bool ModifyStudentPwd(Student stu,string newPwd) 
        {
           stu.Stu_Pwd = newPwd;
           Student s= stuDal.UpdateEntity(stu);
           if (s==null)
           {
               return false;
           }
           return true; 
        
        }
        #endregion

        #region 修改学生的头像
        /// <summary>
        /// 修改学生的头像
        /// </summary>
        /// <param name="stu">传入一个学生对象</param>
        /// <param name="newPhoto">新的头像的路径</param>
        /// <returns>true修改成功 false修改失败</returns>
        public bool ModifyStudentPhoto(Student stu,string newPhoto) 
        {
           stu.Stu_Photo = newPhoto;
           Student s=stuDal.UpdateEntity(stu);
           if (s==null)
           {
               return false;
           }
           return true;
        }
        #endregion

        #region 修改学生的联系方式 电话号码
        /// <summary>
        /// 修改学生的联系方式 电话号码
        /// </summary>
        /// <param name="stu"></param>
        /// <param name="newEmail"></param>
        /// <returns></returns>
        public bool ModifyStudentTel(Student stu,string newTel) 
        {
            DBSession db = new DBSession();
            stu.Stu_Tel = newTel;
            Student s = stuDal.UpdateEntity(stu);
            if (s == null)
            {
                return false;
            }
            db.SaveChanges();
            return true;
        
        }
        #endregion

        #region 修改学生的联系方式 Email
        /// <summary>
        /// 修改学生的联系方式 Email
        /// </summary>
        /// <param name="stu"></param>
        /// <param name="newTel"></param>
        /// <returns></returns>
        public bool ModifyStudentEmail(Student stu, string newEmail)
        {
            DBSession db = new DBSession();
            stu.Stu_Email = newEmail;
            Student s = stuDal.UpdateEntity(stu);
            if (s == null)
            {
                return false;
            }
            db.SaveChanges();
            return true;

        }
        #endregion

        #region 根据班级查询学生集合
        /// <summary>
        /// 根据班级查询学生集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<Student> GetStudentsByClassId(int id) 
        {
           IQueryable<Student> iq= stuDal.GetEntities(stu=>stu.Class.C_Id==id);
            return iq ;
        }
        #endregion

        #region 根据pro查询所有学生
        public IQueryable<Student> GetAllStuByPro(string pro) {
            return stuDal.GetEntities(u=>u.Profession.Pro_Name==pro);
        }
        #endregion

    }
}
