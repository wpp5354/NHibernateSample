using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;

namespace NHibernateSample.Session
{
    public class NHibernateHelper
    {
        private ISessionFactory _sessionFactory;

        public NHibernateHelper()
        {
            _sessionFactory = GetSessionFactory();
        }

        private ISessionFactory GetSessionFactory()
        {
            return (new Configuration()).Configure().BuildSessionFactory();
        }

        public ISession GetSession()
        {
            return _sessionFactory.OpenSession();
        }
    }
}
