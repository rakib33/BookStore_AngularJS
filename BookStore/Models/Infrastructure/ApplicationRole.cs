using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.Models.Infrastructure
{

    public class ApplicationRole : IdentityRole
    {   
        [StringLength(100)]
        public string Description { get; set; }
        
    }
   
}