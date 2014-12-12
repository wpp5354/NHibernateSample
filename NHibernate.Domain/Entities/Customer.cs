using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernate.Domain.Entities
{
    public class Customer
    {
        public virtual Guid Id { get; set; }
        public virtual string LastName { get; set; }
        public virtual int Version { get; set; }
        public virtual Name NameAddress { get; set; }
        public virtual ISet<Orders> orders { get; set; }
    }
}
