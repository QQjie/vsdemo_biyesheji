using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA.DAL;
using OA.Model;

namespace OA.BLL
{
    public class GradeTempService
    {
        GradeTempDal gratDal = new GradeTempDal();

        #region 根据themeid查询临时成绩对象集合
        /// <summary>
        /// 根据themeid查询临时成绩对象集合
        /// </summary>
        /// <param name="theid"></param>
        /// <returns></returns>
        public IQueryable<GradeTemp> GetGradeTempByTheme(int theid) 
        {
           return gratDal.GetEntities(grat=>grat.Theme.Theme_Id==theid);
        }
        #endregion


        #region 根据老师查询临时成绩对象
       /// <summary>
        ///  根据老师查询临时成绩对象
       /// </summary>
       /// <param name="tea">老师对象</param>
       /// <param name="theid">themeid</param>
       /// <returns></returns>
        public GradeTemp GetGradeTempByTeacher(Teacher tea,int theid)
        {
            GradeTemp g = new GradeTemp();
            IQueryable<GradeTemp> iq=gratDal.GetEntities(grat => grat.Theme.Theme_Id == theid&&grat.Teacher.Tea_Id==tea.Tea_Id);
            foreach (var item in iq)
            {
                g = item;
            }
            return g;
        }
        #endregion

        #region 添加一个临时成绩对象

        /// <summary>
        /// 添加一个临时成绩对象
        /// </summary>
        /// <param name="g"></param>
        public void AddGradeTemp(GradeTemp g) 
        {
            gratDal.Add(g);
        }
        #endregion

        #region 修改临时成绩对象
        /// <summary>
        /// 修改临时成绩对象
        /// </summary>
        /// <param name="g"></param>
        /// <param name="score"></param>
        public void ModifyGradeTemp(GradeTemp g,int score) 
        {
            g.ScoreTemp = score;
            gratDal.UpdateEntity(g);
        }
        #endregion

    }
}
