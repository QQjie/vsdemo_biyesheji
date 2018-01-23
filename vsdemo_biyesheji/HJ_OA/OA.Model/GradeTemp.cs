using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Model
{
    public class GradeTemp
    {
        public virtual int? GraTemp_Id { get; set; }
        public virtual Theme Theme { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual int ScoreTemp { get; set; }

    }
}
