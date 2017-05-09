using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MobileService.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        [Column(TypeName = "varchar")]
        public string Address { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(15)]
        public string ContactNo { get; set; }
    }
}