using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringNet
{
    class UserInfoService
    {
        public IUserInfoDal userInfoDal { get; set; }
        public void Show() {
            userInfoDal.Show();
            Console.WriteLine("UserInfoService");
        }
    }
}
