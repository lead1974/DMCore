using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMCore.Data.Core.Domain
{
    public class DealTag
    {
        public DealTag()
        {
            Deals = new HashSet<Deal>();
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Deal> Deals { get; set; }
    }
}
