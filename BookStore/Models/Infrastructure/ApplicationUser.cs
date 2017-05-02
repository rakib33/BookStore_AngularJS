using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace BookStore.Models.Infrastructure
{
   
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public ApplicationUser()
        {
            this.Images = new HashSet<UserImage>();
        }             
        
        [StringLength(200)]
        public string NID { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }

        [StringLength(100)]
        public string Country { get; set; }

        [StringLength(50)]
        public string FatherName { get; set; }

        [StringLength(50)]
        public string MotherName { get; set; }

        [StringLength(20)]
        public string BloodGroup { get; set; }

        //Flat Owner / Renter/ Application User
        [StringLength(50)]
        public string UserType { get; set; }

        [StringLength(150)]
        public string PresentAddress { get; set; }

        [StringLength(150)]
        public string PermanentAddress { get; set; }

        /// <summary>
        /// This contains ref eg Uncle Aunt etc His/her Name,Occupation,Phone,Address and Relation with User
        /// Text Format Name:Abdur Rahim,Occupation:Teacher,Relation:Uncle,Phone:***,Address:....
        /// </summary>
        [StringLength(300)]
        public string ReferenceInfo { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }

        [StringLength(100)]
        public string UpdateBy { get; set; }

        //mobile device or browser
        [StringLength(30)]
        public string Device_IP { get; set; }

        [StringLength(100)]
        public string BrowserName { get; set; }

        [StringLength(50)]
        public string BrowserVersion { get; set; }
        [StringLength(200)]
        public string Description { get; set; }                  
        
        public DateTime? JoiningDate { get; set; }

        public DateTime? ExpairedDate { get; set; }

        public bool AllAccess { get; set; }      
        public bool IsActive { get; set; }

        public virtual ICollection<UserImage> Images { get; set; }


    }
}