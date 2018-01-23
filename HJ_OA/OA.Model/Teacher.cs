using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Model
{
    public class Teacher
    {
        public virtual int? Tea_Id { get; set; }
        public virtual string Tea_Num { get; set; }
        public virtual string Tea_Name { get; set; }
        public virtual TeaGroup TeaGroup { get; set; }
        public virtual string Tea_Pwd { get; set; }
        public virtual string Tea_Sex { get; set; }
        public virtual int Tea_StuCount { get; set; }
        public virtual int Tea_ChoseCount { get; set; }
        public virtual Department Department { get; set; }
        public virtual string Tea_Tel { get; set; }
        public virtual string Tea_Email { get; set; }
        public virtual string Tea_Photo { get; set; }
        public virtual IList<Theme> Themes { get; set; }
       public virtual IList<TeaFile> TeaFiles  { get; set; }
       public virtual IList<ChoseTheme>  ChoseThemes { get; set; }
    }
}
