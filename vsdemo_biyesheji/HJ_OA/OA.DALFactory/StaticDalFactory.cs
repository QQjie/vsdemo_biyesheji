using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA.DAL;
using OA.IDAL;
using System.Reflection;

namespace OA.DALFactory
{
    //用工厂隔离了bll层与dal层的依赖
    public class StaticDalFactory
    {
        //将方法的返回值设置为接口  这样不管返回的是哪个dal都能被接收
        public static IUserInfoDal GetUserInfoDal() 
        {
            
            //简单工厂的方法
            // return new UserInfoDal(); //此处需要new还是不完美  因为需求一改变就要改这里的代码，改代码就要重新生成
            //new关键字使得这个工厂还是和dal层紧密结合在一起
            

            //抽象工厂
            //最完美的就是改一下配置文件就能完成上面的功能，这样就又降低了dal层与工厂的耦合度
            //使用反射创建实例就是简单工厂变为了抽象工厂
            //使用反射创建实例第一。需要拿到程序集  第二用程序集创建一个实例
       
            //关键就是要拿到不同dal的程序集
            //return  Assembly.Load("OA.NHDAL").CreateInstance("UserInfoDal") as UserInfoDal;

            //最完美的就是改一下配置文件就能完成上面的功能
            //需要读取配置文件就必须添加system.configration引用
            string assemblyname=System.Configuration.ConfigurationManager.AppSettings["DalAssemblyName"];
            return Assembly.Load(assemblyname).CreateInstance(assemblyname+".UserInfoDal") as UserInfoDal;
        }

        public static IOrderInfoDal GetOrderInfoDal()
        {
            string assemblyname = System.Configuration.ConfigurationManager.AppSettings["DalAssemblyName"];
            return Assembly.Load(assemblyname).CreateInstance(assemblyname + ".OrderInfoDal") as OrderInfoDal;
        }
        public static ChoseThemeDal GetChoseThemeDal()
        {
            return new ChoseThemeDal();
        }



    }
}
