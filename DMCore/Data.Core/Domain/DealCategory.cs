using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DMCore.Data.Core.Domain
{
    public class DealCategory
    {
        public DealCategory()
        {
            Stores = new HashSet<Store>();
            Deals = new HashSet<Deal>();
            Coupons = new HashSet<Coupon>();
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Status { get; set; }
        public int SortSeq { get; set; }
        public bool PublicCategory { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedTS { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedTS { get; set; }

        public virtual ICollection<Deal> Deals { get; set; }
        public virtual ICollection<Coupon> Coupons { get; set; }
        public virtual ICollection<Store> Stores { get; set; }
    }
}
