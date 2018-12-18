﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Environment;
using System.Linq;
using System.Threading.Tasks;

namespace DMCore.Data.Core.Domain
{
    public class Deal
    {
        public Deal()
        {
            Tags = new HashSet<DealTag>();
        }
        public long Id { get; set; }

        public string Title { get; set; }

        public string Instructions { get; set; }
        public IEnumerable<string> InstructionsList
        {
            get { return (Instructions ?? string.Empty).Split(NewLine); }
        }

        public string URL { get; set; }

        public string Price { get; set; }

        public string ImageURL { get; set; }

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
        public bool  DMProduct { get; set; }

        public DateTime StartTS { get; set; }
        public DateTime EndTS { get; set; }

        public bool  Approved { get; set; }
        public int  Status { get; set; }

        public int Views { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }

        public string  CreatedBy { get; set; }
        public DateTime CreatedTS { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedTS { get; set; }


        public DealCategory DealCategory { get; set; }
        public long DealCategoryId { get; set; }

        public AffiliateSite AffiliateSite { get; set; }
        public long? AffiliateSiteId { get; set; }

        public virtual ICollection<DealTag> Tags { get; set; }

    }
}