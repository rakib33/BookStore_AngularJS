using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.Models.Infrastructure
{
    public abstract class BaseClass
    {

        [StringLength(128)]
        public string Id { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        [StringLength(100)]
        public string UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; }
        
    }
}