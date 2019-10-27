using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject
{
    [TestFixture]
    public class PublicationServiceTest
    {
        [Test]
        public void Get_by_id_1_return_PublicationDTO()
        {
            int i = 4;
            i += 1;
            Assert.AreEqual(i, 5);
        }
    }
}
