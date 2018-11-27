using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Environment;
using System.Linq;
using System.Threading.Tasks;

namespace DMCore.Data.Models
{
    [Table("Coupons")]
    public class Coupon
    {
        public long Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public string Instructions { get; set; }
        public IEnumerable<string> InstructionsList
        {
            get { return (Instructions ?? string.Empty).Split(NewLine); }
        }

        public string URL { get; set; }

        [StringLength(255)]
        public string Code { get; set; }

        public DateTime StartTS { get; set; }
        public DateTime EndTS { get; set; }

        public bool Approved { get; set; }
        public int Status { get; set; }

        public int Views { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedTS { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedTS { get; set; }

        public CouponCategory CouponCategory { get; set; }
        public long CouponCategoryId { get; set; }

        public AffiliateSite AffiliateSite { get; set; }
        public long? AffiliateSiteId { get; set; }
    }
}
