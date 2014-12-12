using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Domain.Entities;

namespace NHibernateSample
{
    public class OrderData
    {
        public bool AddOrder(Orders order)
        {
            try
            {
                var session = SessionManager.GetSession();
                session.SaveOrUpdate(order);
                session.Flush();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
