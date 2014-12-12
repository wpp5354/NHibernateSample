using System;
using NHibernate.Domain.Entities;
using NHibernateSample;
using NUnit.Framework;

namespace NHibernate.Data.Test
{
    [TestFixture]
    public class NHibernateSampleFixture
    {
        private ISession _session;
        private SessionManager _helper;
        private NHibernateSample.NHibernateSample _sample;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            //_helper = new SessionManager();
        }

        [Test]
        public void GetCustomerById1Test()
        {
            var tempCutomer = new Customer { NameAddress = { FirstName = "aaa", CustomerAddress = "address" }, LastName = "wp" };
            _sample.CreateCustomer(tempCutomer);
            Customer customer = _sample.GetCustomerById(1);
            Guid customerId = customer.Id;
            Assert.AreEqual(1, customerId);
        }
    }
}
