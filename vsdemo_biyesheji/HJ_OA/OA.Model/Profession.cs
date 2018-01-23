using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Model
{
    public class Profession
    {
        public virtual int? Pro_Id { get; set; }
       
        public virtual string Pro_Name { get; set; }
        public virtual int Pro_SYear { get; set; }
        public virtual Department Department { get; set; }
       public virtual IList<Class> Classes { get; set; }
       public virtual IList<Theme> Themes { get; set; }
        public virtual IList<Student> Students { get; set; }
    }
}
