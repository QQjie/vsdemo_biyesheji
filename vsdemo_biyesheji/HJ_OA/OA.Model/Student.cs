using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Model
{
    [Serializable]
    public class Student
    {
        public virtual int? Stu_Id { get; set; }
        public virtual string Stu_Num { get; set; }
        public virtual string Stu_Name { get; set; }
        public virtual string Stu_Pwd { get; set; }
        public virtual string Stu_Sex { get; set; }
        public virtual Profession Profession { get; set; }
        public virtual Department Department { get; set; }
        public virtual Class Class { get; set; }
        public virtual string Stu_Tel { get; set; }
        public virtual string Stu_Email { get; set; }
        public virtual string Stu_Photo { get; set; }
        //public virtual ChoseTheme ChoseTheme { get; set; }
        public virtual IList<StuFile> StuFiles { get; set; }
    }
}
