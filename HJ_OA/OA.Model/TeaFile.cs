using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Model
{
    public  class TeaFile
    {
        public virtual int? TeaF_Id { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual string TeaF_Name { get; set; }
        public virtual string TeaF_Intro { get; set; }
        public virtual string Url { get; set; }
        public virtual DateTime SubTime { get; set; }
    }
}
