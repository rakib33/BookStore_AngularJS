using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using BookStore.Models.Infrastructure;

namespace BookStore.Models.bookStore
{
    public class Author:BaseClass
    {
        //[StringLength(50)]
        //public string Name { get; set; }

        [StringLength(5)]
        public string Initials { get; set; }

        [StringLength(20)]
        public string FirstName { get; set; } 

        [StringLength(20)]
        public string LastName { get; set; }

        [StringLength(15)]
        public string ZipCode { get; set; }

        [StringLength(200)]
        public string Address { get; set; }
        
        [StringLength(100)]
        public string Country { get; set; }

    }
}