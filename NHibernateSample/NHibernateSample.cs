using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Domain.Entities;

namespace NHibernateSample
{
    public class NHibernateSample
    {
        protected ISession Session { get; set; }

        public NHibernateSample(ISession session)
        {
            Session = session;
        }

        public void CreateCustomer(Customer customer)
        {
            Session.Save(customer);
            Session.Flush();
        }

        public Customer GetCustomerById(int customerId)
        {
            return Session.Get<Customer>(customerId);
        }
    }
}
