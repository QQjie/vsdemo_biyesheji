using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA.DAL;
using OA.Model;
using OA.DALFactory;

namespace OA.BLL
{
    public class TeacherService 
    {
        TeacherDal teaDal = new TeacherDal();

        #region 根据老师的工号查询老师对象
        /// <summary>
        /// 根据老师的工号查询老师对象
        /// </summary>
        /// <param name="num">老师的工号</param>
        /// <returns>老师对象</returns>
        public Teacher GetTeacherByNum(string num)
        {
            string teanum = num.Trim();
            Teacher s = new Teacher();
            IQueryable<Teacher> teaiq = teaDal.GetEntities(tea => tea.Tea_Num == teanum);
            if (teaiq.Count() == 0)
            {
                return null;
            }
            foreach (var item in teaiq)
            {
                s = item;
            }
            return s;
        }
        #endregion

        #region 根据老师的ID查询老师对象
        /// <summary>
        /// 根据老师的ID返回老师对象
        /// </summary>
        /// <param name="id">老师ID</param>
        /// <returns>老师对象</returns>
        public Teacher GetTeacherById(int id)
        {
            Teacher s = new Teacher();
            IQueryable<Teacher> teaiq = teaDal.GetEntities(tea => tea.Tea_Id == id);
            if (teaiq.Count() == 0)
            {
                return null;
            }
            foreach (var item in teaiq)
            {
                s = item;
            }
            return s;
        }
        #endregion

        #region 根据老师的名字查询老师对象
        /// <summary>
        /// 根据老师的名字查询老师对象
        /// </summary>
        /// <param name="name">传入老师的名字</param>
        /// <returns>返回老师对象</returns>
        public Teacher GetTeacherByName(string name)
        {
            string teaname = name.Trim();
            Teacher s = new Teacher();
            IQueryable<Teacher> teaiq = teaDal.GetEntities(tea => tea.Tea_Name == teaname);
            if (teaiq.Count() == 0)
            {
                return null;
            }
            foreach (var item in teaiq)
            {
                s = item;
            }
            return s;
        }
        #endregion

        #region 老师的登录验证
        /// <summary>
        /// 老师的登录验证
        /// </summary>
        /// <param name="teanum">输入的老师学号</param>
        /// <param name="pwd">输入的老师密码</param>
        /// <returns>验证通过返回一个学对象否则返回一个null</returns>
        public Teacher TeacherLoginByNumAndPwd(string teanum, string pwd)
        {
            Teacher t = new Teacher();
            IQueryable<Teacher> teaiq = teaDal.GetEntities(tea=> tea.Tea_Num == teanum && tea.Tea_Pwd == pwd);
            if (teaiq.Count() == 0)
            {
                return null;
            }
            foreach (var item in teaiq)
            {
                t = item;
            }
            return t;
        }
        #endregion

        #region 修改老师密码
        /// <summary>
        /// 修改老师密码
        /// </summary>
        /// <param name="tea">传入的老师对象</param>
        /// <param name="newPwd">修改的新密码</param>
        /// <returns>true修改成功 false修改失败</returns>
        public bool ModifyTeacherPwd(Teacher tea, string newPwd)
        {
            tea.Tea_Pwd= newPwd;
            Teacher s = teaDal.UpdateEntity(tea);
            if (s == null)
            {
                return false;
            }
            return true;

        }
        #endregion

        #region 修改老师的头像
        /// <summary>
        /// 修改老师的头像
        /// </summary>
        /// <param name="tea">传入一个老师对象</param>
        /// <param name="newPhoto">新的头像的路径</param>
        /// <returns>true修改成功 false修改失败</returns>
        public bool ModifyTeacherPhoto(Teacher tea, string newPhoto)
        {
            tea.Tea_Photo = newPhoto;
            Teacher s = teaDal.UpdateEntity(tea);
            if (s == null)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region 修改老师的联系方式 电话号码
        /// <summary>
        /// 修改老师的联系方式 电话号码
        /// </summary>
        /// <param name="tea"></param>
        /// <param name="newEmail"></param>
        /// <returns></returns>
        public bool ModifyTeacherTel(Teacher tea, string newTel)
        {
            tea.Tea_Tel = newTel;
            Teacher s = teaDal.UpdateEntity(tea);
            if (s == null)
            {
                return false;
            }
            return true;

        }
        #endregion

        #region 修改老师的联系方式 Email
        /// <summary>
        /// 修改老师的联系方式 Email
        /// </summary>
        /// <param name="tea"></param>
        /// <param name="newTel"></param>
        /// <returns></returns>
        public bool ModifyTeacherEmail(Teacher tea, string newEmail)
        {
            tea.Tea_Email = newEmail;
            Teacher s = teaDal.UpdateEntity(tea);
            if (s == null)
            {
                return false;
            }
            return true;

        }
        #endregion

        #region 添加一个老师

        public Teacher AddOneTea(Teacher tea) {
            DBSession db = new DBSession();
            Teacher tea1= teaDal.Add(tea);
            db.SaveChanges();
            return tea1;
        }
        #endregion

        #region 根据院系查询老师

        public IQueryable<Teacher> GetAllTeaByDep(int depid){
            IQueryable<Teacher> iq=teaDal.GetEntities(u=>u.Department.Dep_Id==depid);
            return iq;
        }
        #endregion

        public bool  UpdateTeaTg(Teacher tea,int tgid) 
        {
            DBSession db = new DBSession();
            tea.TeaGroup=null;
            if (teaDal.UpdateEntity(tea)!=null)
            {
                db.SaveChanges();
                return true;
            };
            return false;
        }
        public bool UpdateTeaAddCount(Teacher tea)
        {
            DBSession db = new DBSession();
            if (tea.Tea_ChoseCount==tea.Tea_StuCount)
            {
                return false;
            }
            tea.Tea_ChoseCount++;
            if (teaDal.UpdateEntity(tea) != null)
            {
                db.SaveChanges();
                return true;
            };
            return false;
        }

        public bool AddTeaToTG(string teaname,string tgname) {
            TeaGroupDal tgdal = new TeaGroupDal();
            Teacher t = new Teacher();
            TeaGroup tg = new TeaGroup();
           IQueryable<Teacher> iqtea = teaDal.GetEntities(u=>u.Tea_Name==teaname);
           foreach (var item in iqtea)
           {
               t = item;
           }
         IQueryable<TeaGroup> iqtg= tgdal.GetEntities(u=>u.TG_Name==tgname);
         foreach (var item in iqtg)
         {
             tg = item;
         }
            DBSession db = new DBSession();
            t.TeaGroup = tg;
            if (teaDal.UpdateEntity(t) != null)
            {
                db.SaveChanges();
                return true;
            };
            return false;
        
        }

        #region 删除老师
        public void DeleteTea(int teaid) {
            DBSession db = new DBSession();
            Teacher t = new Teacher();
           IQueryable<Teacher> iq= teaDal.GetEntities(u=>u.Tea_Id==teaid);
           foreach (var item in iq)
           {
               t = item;
           }
           if (teaDal.DeleteEntity(t)!=null)
           {
               db.SaveChanges();
           };
        }
        #endregion
    }
}
