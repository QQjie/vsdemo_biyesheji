using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA.DAL;

namespace OA.IDAL
{
    public interface IDBSession
    {
        IUserInfoDal UserInfoDal { get; }
        IOrderInfoDal OrderInfoDal { get; }
       
        void SaveChanges();
    }
}
