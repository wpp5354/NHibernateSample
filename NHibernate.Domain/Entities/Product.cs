using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernate.Domain.Entities
{
    public class Product
    {
        public virtual Guid ProductId { get; set; }
        public virtual string Name { get; set; }
        public virtual decimal Cost { get; set; }

        ///多对多
        public virtual IList<Orders> Orders { get; set; }
    }
}
