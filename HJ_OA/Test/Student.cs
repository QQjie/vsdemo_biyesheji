using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class Student
    {
        public virtual int? Id { set; get; }
        public virtual string Name { set; get; }
        public virtual string Sex { set; get; }
        public virtual int Age { set; get; }
        public virtual string Email { set; get; }
    }
}
