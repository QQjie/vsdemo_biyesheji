using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringNet
{
    interface IUserInfoDal
    {
        void Show();
        string Name { get; set; }
    }
}
