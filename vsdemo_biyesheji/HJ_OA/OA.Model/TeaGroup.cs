using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Model
{
    public class TeaGroup
    {
        public virtual int? TG_Id { get; set; }
        public virtual string TG_Name { get; set; }
        public virtual Department Department { set; get; }
        public virtual IList<Teacher> Teachers { get; set; }
        public virtual IList<Theme> Themes { get; set; }
    }
}
