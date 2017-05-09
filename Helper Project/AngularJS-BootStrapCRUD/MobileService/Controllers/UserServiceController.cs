using MobileService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;


namespace MobileService.Controllers
{
    public class UserServiceController : ApiController
    {
        DataContext context = new DataContext();

        [HttpPost]
        public int AddUser(User user)
        {
            context.User.Add(user);
            context.SaveChanges();
            return user.UserId;
        }

        [HttpGet]
        public User GetUser(int id)
        {
            User user = context.User.Find(id);
            return user;
        }

        [HttpGet]
        public List<User> GetUsersList()
        {
            return context.User.ToList();
        }

        [HttpPut]
        public void ModifyUser(User user, int id)
        {
            if (user.UserId == id)
            {
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        [HttpDelete]
        public void DeleteUser(int id)
        {
            User user = context.User.Find(id);
            if (user != null)
            {
                context.User.Remove(user);
                context.SaveChanges();
            }

        }
    }
}
