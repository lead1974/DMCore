using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DMCore.Data.Core.Domain.Deal
{
    public class DealCategory
    {
        public DealCategory()
        {
            Stores = new HashSet<Store>();
            Deals = new HashSet<Deal>();
            Coupons = new HashSet<Coupon>();
        }
        [ScaffoldColumn(false)]
        public long Id { get; set; }
        [Display(Name = "Category Name")]
        public string Name { get; set; }
        [Display(Name = "Category Short Name")]
        public string ShortName { get; set; }
        public string FAIcon { get; set; }
        public string Status { get; set; }
        public int SortSeq { get; set; }
        [Display(Name = "Is Category Public")]
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
