using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Model
{
    public class Message
    {
        public virtual int? Mes_Id { get; set; }
        public virtual string Zhu_Id { get; set; }
        public virtual string  Ke_Id { get; set; }
        public virtual string Content { get; set; }
        public virtual DateTime SubTime { get; set; }
    }
}
