using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NHibernate.Domain.Entities;
using NHibernateSample;

namespace NHibernate.Business
{
    public class CustomerBusiness
    {
        private CustomerData _customerData;
        public CustomerBusiness()
        {
            _customerData = new CustomerData();
        }

        public bool AddCustomer(Customer customer)
        {
            return _customerData.AddCustomer(customer);
        }

        public bool DelCustomer(Customer customer)
        {
            return _customerData.DeleteCustomer(customer);
        }

        public IList<Customer> GetCustomerList(Expression<Func<Customer, bool>> where)
        {
            return _customerData.GetCustomerList(where);
        }

        public IList<Customer> Search(int count)
        {
            return _customerData.GetCustomers(count);
        }

        public bool SaveOrUpdateByTrans(Customer customer)
        {
            return _customerData.SaveOrUpdateByTrans(customer);
        }
    }
}
