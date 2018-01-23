using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HJDemo01.Models
{
    public class Student
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public int Age{ get; set; }
        public string Email { get; set; }
        
        
        public Student(){}

        public Student(int id,string name,string sex,int age,string email) {
            this.Id = id;
            this.Name = name;
            this.Sex = sex;
            this.Age = age;
            this.Email = email;
        
        }
    }
}