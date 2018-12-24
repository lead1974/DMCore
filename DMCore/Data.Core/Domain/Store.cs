using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static System.Environment;

namespace DMCore.Data.Core.Domain
{
    public class Store
    {
        public Store()
        {
            Deals = new HashSet<Deal>();
            Coupons = new HashSet<Coupon>();
        }
        public long Id { get; set; }

        public string Name { get; set; }
        public string Domain { get; set; }
        public string Instructions { get; set; }
        public IEnumerable<string> InstructionsList
        {
            get { return (Instructions ?? string.Empty).Split(NewLine); }
        }

        public string StoreTips { get; set; }
        public IEnumerable<string> StoreTipsList
        {
            get { return (StoreTips ?? string.Empty).Split(NewLine); }
        }

        #region Image

        public byte[] Image { get; set; }

        public string ImageContentType { get; set; }

        public string GetInlineImageSrc()
        {
            if (Image == null || ImageContentType == null)
                return null;

            var base64Image = System.Convert.ToBase64String(Image);
            return $"data:{ImageContentType};base64,{base64Image}";
        }

        public void SetImage(Microsoft.AspNetCore.Http.IFormFile file)
        {
            if (file == null)
                return;

            ImageContentType = file.ContentType;

            using (var stream = new System.IO.MemoryStream())
            {
                file.CopyTo(stream);
                Image = stream.ToArray();
            }
        }

        #endregion

        public string CreatedBy { get; set; }
        public DateTime CreatedTS { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedTS { get; set; }

        public DealCategory DealCategory { get; set; }
        public long DealCategoryId { get; set; }

        public virtual ICollection<Deal> Deals { get; set; }
        public virtual ICollection<Coupon> Coupons { get; set; }
    }
}
