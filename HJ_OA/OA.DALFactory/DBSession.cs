using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA.IDAL;
using OA.DAL;




namespace OA.DALFactory
{
    //拥有所有dal的实例，是访问数据库的接口
    public class DBSession:IDBSession
    {
        public IUserInfoDal UserInfoDal {
            get 
            {
                return StaticDalFactory.GetUserInfoDal();
            }
        }
        public IOrderInfoDal OrderInfoDal 
        {
            get 
            {
                return StaticDalFactory.GetOrderInfoDal();
            }
        }
        public ChoseThemeDal ChoseThemeDal 
        {
            get {
                return StaticDalFactory.GetChoseThemeDal();
            }
        }

        public void SaveChanges() {
            if (!object.Equals(MySession.GetTran(),null))
            {
                MySession.TranCommit();
            }
            MySession.CloseSession();
        }
        
       
    }
}
