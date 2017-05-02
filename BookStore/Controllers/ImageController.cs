using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using BookStore.Models.bookStore;
using BookStore.Models.Infrastructure;

namespace BookStore.Controllers
{
    public class ImageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult GetImage(string id)
        {
            try
            {
                Book book = db.Books.Find(id);
                byte[] image = book.BookImage;
                return File(image, "image/jpg");
            }
            catch { return null; }
        }

        public ActionResult Save(HttpPostedFileBase file)
        {
            Book book = new Book();
            if (file != null)
            {
                book.BookImage = new byte[file.ContentLength];
                file.InputStream.Read(book.BookImage,0,file.ContentLength);
                db.Books.Add(book);
                db.SaveChanges();
              
            }
            return null;
        }
	}
}