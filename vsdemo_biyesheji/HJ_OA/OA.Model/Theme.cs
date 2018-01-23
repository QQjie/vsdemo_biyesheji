using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Model
{
    public class Theme
    {
        public virtual int? Theme_Id { get; set; }
        public virtual string Theme_Name { get; set; }
        public virtual Department Department { get; set; }
        public virtual Profession Profession { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual TeaGroup TeaGroup { get; set; }
       // public virtual int CountStu { get; set; }
        public virtual string Introduce { get; set; }
        public virtual DateTime SubTime { get; set; }
        public virtual int? Examine { get; set; }
        public virtual DateTime? ExamineTime { get; set; }
       // public virtual int? ExamineStatus { get; set; }
       // public virtual ChoseTheme ChoseTheme { get; set; }

    }
}
