using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Model
{
    public class StuFile
    {
        public virtual int? StuF_Id { get; set; }
        public virtual Student Student { get; set; }
        public virtual string  StuF_Name { get; set; }
        public virtual string  StuF_Intro { get; set; }
        public virtual string Url { get; set; }
        public virtual int Status { get; set; }
        public virtual DateTime SubTime { get; set; }
    }
}
