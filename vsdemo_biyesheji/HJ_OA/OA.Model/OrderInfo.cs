using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Model
{
    public class OrderInfo
    {
        public virtual int? OrderId { get; set; }
        public virtual string Content { get; set; }
        public virtual int UserInfoId { get; set; }
    }
}
