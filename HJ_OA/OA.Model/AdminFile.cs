using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Model
{
     public class AdminFile
    {
        public virtual int? AdF_Id { get; set; }
        public virtual Admin Admin { get; set; }
        public virtual string AdF_Name { get; set; }
        public virtual string AdF_Intro { get; set; }
        public virtual string Url { get; set; }
        public virtual DateTime SubTime { get; set; }
    }
}
