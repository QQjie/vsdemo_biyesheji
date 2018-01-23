using OA.DAL;
using OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.DAL
{
    interface IStudenDao
    {
        IList<Student> queryAll();

        Student queryStudent(int id);

        Student EditStudent(Student s);

        Student createStudent(Student s);

        Student deleteStudent(int id);
    }
}
