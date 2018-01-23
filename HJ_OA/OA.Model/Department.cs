using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Model
{
    public class Department
    {
        public virtual int? Dep_Id { get; set; }
       
        public virtual string Dep_Name { get; set; }

        public virtual IList<Profession> Professions { get; set; }
        public virtual IList<Theme> Themes { get; set; }
        public virtual IList<Student> Students { get; set; }
    }
}
