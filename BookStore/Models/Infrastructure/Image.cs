using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookStore.Models.Infrastructure
{
    public class UserImage :BaseClass
    {
        public byte[] ImageStream { get; set; }

        [ForeignKey("ApplicationUser")]
        public string FK_User_Id { get; set; } //FK_<TableName>_<primary Key>
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}