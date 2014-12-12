using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernate.Domain.Entities
{
    public class Name
    {
        public string CustomerAddress { get; set; }

        public string FirstName { get; set; }

        public string NameAddress
        {
            get { return this.FirstName + this.CustomerAddress; }
        }
    }
}
