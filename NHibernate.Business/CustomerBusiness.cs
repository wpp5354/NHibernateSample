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

        public List<Customer> GetCustomersOrders(Guid customerId)
        {
            var list = _customerData.GetCustomersOrders(customerId);
            return (List<Customer>)list;
        }

        public List<Customer> GetCustomersOrdersByHQL(Guid customerId)
        {
            var list = _customerData.GetCustomersOrdersByHQL(customerId);
            return (List<Customer>)list;
        }

        public List<Customer> GetCustomerOrdersProductsBySQL(Guid customerId)
        {
            var list = _customerData.GetCustomerOrdersProductsBySQL(customerId);

            return (List<Customer>)list;
        }

        public List<Customer> GetCustomerOrdersProductsByHQL(Guid customerId)
        {
            var list = _customerData.GetCustomerOrdersProductsByHQL(customerId);

            return (List<Customer>)list;
        }

        public List<Customer> GetCustomerOrdersProductsByCriteriaAPI(Guid customerId)
        {
            var list = _customerData.GetCustomerOrdersProductsByCriteriaAPI(customerId);

            return (List<Customer>)list;
        }

        public Customer GetCustomerByLazyLoad(Guid customerId)
        {
            return _customerData.GetCustomerByLazyLoad(customerId);
        }
    }
}
