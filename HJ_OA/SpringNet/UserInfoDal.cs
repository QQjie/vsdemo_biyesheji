using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringNet
{
    public class UserInfoDal:IUserInfoDal
    {
        //public UserInfoDal(string name)  //ctor快捷键
        //{
        //    this.Name = name;
        //}
        public string Name { get; set; }
        public void Show() {
            Console.WriteLine("Show"+Name);
        }
    }
}
