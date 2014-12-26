using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Domain.Entities;
using NHibernate.Linq;

namespace NHibernateSample
{
    public class CustomerData
    {
        //添加
        public bool AddCustomer(Customer customer)
        {
            try
            {
                var session = SessionManager.GetSession();

                session.SaveOrUpdate(customer);

                session.Flush();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //删除
        public bool DeleteCustomer(Customer customer)
        {
            var session = SessionManager.GetSession();
            session.Delete(customer);
            session.Flush();
            return true;
        }

        //修改
        public bool UpdateCustomer(Customer customer)
        {
            var session = SessionManager.GetSession();
            session.Update(customer);
            session.Flush();
            return true;
        }

        public bool SaveOrUpdateByTrans(Customer customer)
        {
            var session = SessionManager.GetSession();
            using (ITransaction transaction = session.BeginTransaction())
            {
                try
                {
                    session.SaveOrUpdate(customer);
                    session.Flush();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public IList<Customer> GetCustomerList(Expression<Func<Customer, bool>> where)
        {
            try
            {
                ISession session = SessionManager.GetSession();
                return session.Query<Customer>().Where(where).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<Customer> GetCustomers(int count)
        {
            ISession session = SessionManager.GetSession();
            ICriteria criteria = session.CreateCriteria(typeof(Customer));

            //FirstName以W开头
            criteria.Add(Restrictions.Like("FirstName", "W%"));
            //FirstName以W开头
            criteria.Add(Restrictions.Like("FirstName", "W", MatchMode.Start));
            //查询FirstName是zhangsan和lisi的
            criteria.Add(Restrictions.In("FirstName", new string[] { "zhangsan", "lisi" }));
            //FirstName不为NULL的
            criteria.Add(Restrictions.IsNotNull("FirstName"));
            //FirstName等于W
            criteria.Add(Restrictions.Eq("FirstName", "W"));
            //And条件
            criteria.Add(Restrictions.And(Restrictions.Eq("CustomerAddress", "地址"), Restrictions.Eq("FirstName", "W")));
            //排序
            criteria.AddOrder(NHibernate.Criterion.Order.Asc("FirstName"));
            criteria.SetMaxResults(count);
            return criteria.List<Customer>();
        }

        public IList<Customer> GetCustomersOrders(Guid customerId)
        {
            ISession session = SessionManager.GetSession();

            return session.CreateSQLQuery("select distinct c.*,o.* from Customer c inner join orders o on c.CustomerId=o.CustomerId where c.CustomerId=:id")
                    .AddEntity("Customer", typeof(Customer))
                    .SetGuid("id", customerId)
                    .List<Customer>();
        }

        public IList<Customer> GetCustomersOrdersByHQL(Guid customerId)
        {
            ISession session = SessionManager.GetSession();
            return session.CreateQuery("select c from Customer c inner join c.orders where c.Id=:id")
                .SetGuid("id", customerId)
                .List<Customer>();
        }

        public IList<Customer> GetCustomerOrdersProductsBySQL(Guid customerId)
        {
            ISession session = SessionManager.GetSession();

            return session.CreateSQLQuery("select distinct c.* from Customer c " +
                                           "inner join Orders o on c.CustomerId=o.CustomerId " +
                                           "inner join OrderProduct op on o.OrderId=op.OrderId " +
                                           "inner join Product p on op.ProductId=p.ProductId where c.CustomerId=:id")
                                           .AddEntity("Customer", typeof(Customer))
                                           .SetGuid("id", customerId)
                                           .List<Customer>();
        }

        public IList<Customer> GetCustomerOrdersProductsByHQL(Guid customerId)
        {
            ISession session = SessionManager.GetSession();

            return session.CreateQuery("select distinct c from Customer c inner join c.orders o inner join o.Products where c.Id=:id")
                                        .SetGuid("id", customerId)
                                        .List<Customer>();
        }

        public IList<Customer> GetCustomerOrdersProductsByCriteriaAPI(Guid customerId)
        {
            ISession session = SessionManager.GetSession();

            return session.CreateCriteria(typeof(Customer))
                .Add(Restrictions.Eq("Id", customerId))
                .CreateCriteria("orders")
                .CreateCriteria("Products")
                .List<Customer>();
        }


        /// <summary>
        /// 采用延迟加载的方式获得用户信息
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public Customer GetCustomerByLazyLoad(Guid customerId)
        {
            ISession session = SessionManager.GetSession();

            return session.Get<Customer>(customerId);

        }
    }
}
