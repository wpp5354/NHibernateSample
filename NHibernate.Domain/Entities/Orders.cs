using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernate.Domain.Entities
{
    public class Orders
    {
        public virtual Guid OrderId { get; set; }

        public virtual DateTime OrderDate { get; set; }

        public virtual Customer customer { get; set; }
    }
}
