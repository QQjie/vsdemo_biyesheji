using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA.Model;

namespace OA.IDAL
{
    public interface IUserInfoDal:IBaseDal<UserInfo>
    {
        UserInfo GetUserInfoById(int id);
        IList<UserInfo> GetAllUserInfo();
        UserInfo AddUserInfo(UserInfo userinfo) ;
        UserInfo DeleteUserInfoById(UserInfo userinfo);
        UserInfo DeleteUserInfo(UserInfo userinfo);
        UserInfo UpdateUserInfo(UserInfo userinfo);
    }
}
