using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Model
{
    public class Admin
    {
        public virtual int? Ad_Id { get; set; }
        public virtual string Ad_Num { get; set; }
        public virtual string Ad_Name { get; set; }
        public virtual string Ad_Pwd { get; set; }
        public virtual IList<AdminFile> AdminFiles { get; set; }
    }
}
