using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA.DAL;
using OA.Model;

namespace OA.BLL
{
    public class GradeService
    {
        GradeDal gd = new GradeDal();
        BaseDal<Grade> based=new BaseDal<Grade>();
        #region 为论文打成绩即添加一个成绩对象
        /// <summary>
        /// 为论文打成绩即添加一个成绩对象
        /// </summary>
        /// <param name="grade"></param>
        public void AddGrade(Grade grade)
        {
            gd.Add(grade); 
        
        }
        #endregion


        #region 修改成绩

        public void ModifyGrade(Grade grade,int score) 
        {
            grade.Score = score;
            gd.UpdateEntity(grade);
        }
        #endregion

        #region 根据学生查询成绩
        /// <summary>
        /// 根据学生查询成绩
        /// </summary>
        /// <param name="stu"></param>
        /// <returns></returns>
        public Grade GetGradeByStudent(Student stu) 
        {
            Grade g = new Grade();
            IQueryable<Grade> iq=gd.GetEntities(grade=>grade.Student.Stu_Num==stu.Stu_Num);
            if (iq.Count()==0)
            {
                return null;
            }
            foreach (var item in iq)
            {
                g = item;
            }
            return g;
        }
        #endregion

        #region 根据老师查询成绩

        public IList<Grade> GetGradeByTeacher(Teacher tea) 
        {
            IList<Grade> list = new List<Grade>();
            IQueryable<Grade> iq = gd.GetEntities(grade => grade.Teacher.Tea_Num == tea.Tea_Num);
            if (iq.Count() == 0)
            {
                return null;
            }
            foreach (var item in iq)
            {
                list.Add(item);
            }
            return list;
        }
        #endregion

        #region 根据专业查询所有的学生的成绩
        /// <summary>
        /// 根据专业查询所有的学生的成绩
        /// </summary>
        /// <param name="proname"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public IQueryable<Grade> GetAllGradeByPro(string proname,bool b) {
            GradeDal gradal = new GradeDal();
            IQueryable<Grade> iq =null;
            if (b)
            {
                 iq= based.GetEntities(u => u.Student.Profession.Pro_Name == proname); 
            }
            else
            {
                iq = based.GetEntities(u => u.Student.Profession.Pro_Name == proname); 
            }
            return iq;
        }
        #endregion


        #region 根据专业查询所有未通过学生的成绩
        public IQueryable<Grade> GetAllGradeByProAndNoPass(string proname,bool b) {
            GradeDal gradal = new GradeDal();
            IQueryable<Grade> iq =null;
            if (b)
            {
                 iq= gradal.GetEntities(u => u.Student.Profession.Pro_Name == proname&&u.Score<60).OrderBy<Grade, int>(s => s.Score); 
            }
            else
            {
                iq = gradal.GetEntities(u => u.Student.Profession.Pro_Name == proname&&u.Score<60).OrderByDescending<Grade, int>(s => s.Score); 
            }
            return iq;
        }
        #endregion
        #region 根据专业查询所有已通过学生的成绩
        public IQueryable<Grade> GetAllGradeByProAndPassed(string proname, bool b)
        {
            GradeDal gradal = new GradeDal();
            IQueryable<Grade> iq = null;
            if (b)
            {
                iq = gradal.GetEntities(u => u.Student.Profession.Pro_Name == proname && u.Score >=60).OrderBy<Grade, int>(s => s.Score);
            }
            else
            {
                iq = gradal.GetEntities(u => u.Student.Profession.Pro_Name == proname && u.Score >= 60).OrderByDescending<Grade, int>(s => s.Score);
            }
            return iq;
        }
        #endregion


    }
}
