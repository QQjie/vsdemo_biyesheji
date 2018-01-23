using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Model
{
    public class Grade
    {
        public virtual int? Gra_Id { get; set; }
        public virtual Student Student { get; set; }
        public virtual Theme Theme { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual int Score { get; set; }
        public virtual DateTime SubTime { get; set; }
        public virtual string Content { get; set; }

    }
}
