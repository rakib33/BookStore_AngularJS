using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using BookStore.Models.Infrastructure;

namespace BookStore.Models.bookStore
{
    public class Book:BaseClass
    {
     public byte[] BookImage { get; set; }

     [StringLength(50)]
     public string Title { get; set; }

     [StringLength(50)]
     public string ISBN { get; set; }
     public DateTime? PublishingDate { get; set; }
     public decimal? Price { get; set; }

     [StringLength(100)]
     public string PublishingHouse { get; set; }
    }
}