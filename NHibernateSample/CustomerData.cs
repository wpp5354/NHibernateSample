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
    }
}
