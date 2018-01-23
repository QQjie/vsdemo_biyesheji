using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Model
{
    public class ReMessage
    {
        public virtual int? Rem_Id { get; set; }
        public virtual string  Zhu_Id { get; set; }
        public virtual string Ke_Id { get; set; }
        public virtual int Meg_Id { get; set; }
        public virtual string ReContent { get; set; }
        public virtual DateTime SubTime { get; set; }
    }
}
