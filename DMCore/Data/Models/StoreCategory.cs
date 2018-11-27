using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DMCore.Data.Models
{
    [Table("StoreCategories")]
    public class StoreCategory
    {
        public long Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedTS { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedTS { get; set; }
    }
}
