using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using BookStore.Models.bookStore;
using BookStore.Models.Infrastructure;

namespace BookStore.Models.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        //public static ApplicationDbContext Create()
        //{
        //    return new ApplicationDbContext();
        //}

        public IDbSet<Author> Authors { get; set; }
        public IDbSet<UserImage> Images { get; set; }
        public IDbSet<Book> Books { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
                       
            modelBuilder.Entity<UserImage>().HasKey(t => t.Id).ToTable("UserImage");
            modelBuilder.Entity<Author>().HasKey(t => t.Id).ToTable("Author");
            modelBuilder.Entity<Book>().HasKey(t => t.Id).ToTable("Book");
                        
            base.OnModelCreating(modelBuilder);

            //For Identity version 1
            modelBuilder.Entity<ApplicationUser>().ToTable("User");
            modelBuilder.Entity<IdentityUser>().ToTable("User");

            modelBuilder.Entity<ApplicationRole>().ToTable("Role");
            modelBuilder.Entity<IdentityRole>().ToTable("Role");

            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim");
            
        }



    }
}