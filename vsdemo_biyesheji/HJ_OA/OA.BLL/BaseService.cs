using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  OA.IDAL;
using OA.DALFactory;

namespace OA.BLL
{
    public abstract class BaseService<T>where T:class,new()
    {
        //如何实现父类逼迫子类首先给父类自己的属性赋值，之后，才可以使用
        //赋值的操作必须在父类的方法调用之前先执行
        public IBaseDal<T> CurrenDal{get;set;}

        public BaseService() { //基类的构造函数
            SetCurrentDal();   //调用抽象方法  第一种方法
        }

        //第二种方法 就是
        //public BaseService(IBaseDal<T> CurrenDal)
        //{ //基类的构造函数
        //    this.CurrenDal = CurrenDal;   //调用抽象方法  第一种方法
        //}
        public abstract void SetCurrentDal();//抽象方法：要求子类必须实现
       
        public T Add(T entity)
        {
           
            return CurrenDal.Add(entity);//子类必须先给这个属性赋值 然后才可以使用
        }

    }
}
