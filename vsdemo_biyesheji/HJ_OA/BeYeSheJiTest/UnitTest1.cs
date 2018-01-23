using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OA.DAL;
using System.Linq;
using OA.Model;

namespace BeYeSheJiTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ClassDal cd = new ClassDal();
           IQueryable<Class> c=  cd.GetEntities(u=>u.C_Id>0);
            Assert.AreEqual(true, c.Count()>0);
            Assert.AreEqual(true ,true);
        }
    }
}
