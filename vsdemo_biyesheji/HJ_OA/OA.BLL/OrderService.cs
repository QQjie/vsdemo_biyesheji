using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA.Model;
using OA.DAL;
using OA.DALFactory;
using OA.IDAL;

namespace OA.BLL
{
    public class OrderService:BaseService<OrderInfo>
    {
        
        //public OrderService() 
        //{
        //    this.CurrenDal = StaticDalFactory.GetOrderInfoDal();  第二种方法 就没有约束了
        //}

        public override void SetCurrentDal()
        {
            CurrenDal = StaticDalFactory.GetOrderInfoDal();
        }
    }
}
