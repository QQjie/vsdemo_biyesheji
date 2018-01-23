using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OA.Model
{
    public class Class
    {
        //int i=(int)Enum.MyEnum.post ;
        public virtual int? C_Id { get;set; }
       
        public virtual string C_Name { get; set; }
        public virtual Profession Profession { get; set; }
       public virtual Department Department { get; set; }

       public virtual IList<Student> Students { get; set; }
        
    }
}
