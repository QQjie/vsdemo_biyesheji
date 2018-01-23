using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Model
{
    public class ChoseTheme
    {

        public virtual int? CT_Id { get; set; }
       // public virtual string Stu_Num { get; set; }
        public virtual Student Student { get; set; }
       // public virtual int Theme_Id { get; set; }
        public virtual Theme Theme { get; set; }
       // public virtual string  Tea_Num { get; set; }
        public virtual Teacher Teacher { get; set; }
       // public virtual int Status { get; set; }
        public virtual DateTime SubTime { get; set; }
    }
}
