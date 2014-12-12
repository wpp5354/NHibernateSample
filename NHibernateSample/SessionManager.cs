using NHibernate;
using NHibernate.Cfg;

namespace NHibernateSample
{
    public class SessionManager
    {
        private static ISessionFactory _sessionFactory;
        private static ISession _session;
        private static object _objLock = new object();
        private SessionManager()
        {

        }
        public static ISessionFactory GetSessionFactory()
        {
            if (_sessionFactory == null)
            {
                lock (_objLock)
                {
                    if (_sessionFactory == null)
                    {
                        HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
                        _sessionFactory = (new Configuration()).Configure().BuildSessionFactory();
                    }
                }
            }
            return _sessionFactory;
        }
        public static ISession GetSession()
        {
            _sessionFactory = GetSessionFactory();
            if (_session == null)
            {
                lock (_objLock)
                {
                    if (_session == null)
                    {
                        _session = _sessionFactory.OpenSession();
                    }
                }
            }

            return _session;
        }
    }
}
