using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BookStore.Models.bookStore;
using BookStore.Models.Infrastructure;
using BookStore.Repository;

namespace BookStore.Controllers
{

//"N" - xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx (32 digits)
//"D" - xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx (32 digits separated by hyphens)
//"B" - {xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx} (same as "D" with addition of braces)
//"P" - (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx) (same as "D" with addition of parentheses)
//"X" - {0x00000000,0x0000,0x0000,{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}}
//Calling Guid.ToString("D") yields the same result as calling Guid.ToString().

    public class BookController : ApiController
    {
        private IBook bookRepository = null;
        private BookController() {
            bookRepository = new BookRepository();
        }
        private ApplicationDbContext db = new ApplicationDbContext();


       
        [Route("book/list")]
        // GET api/Book
        public IHttpActionResult GetBooks()
        {
            List<Book> book = new List<Book>();
            string message = "ok";
            try
            {
                book = bookRepository.GetBookList();
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            //check is author has records
            if (book == null)
                message = "No Record Found";
            return Ok(new { message = message, list = book });
        }

        [Route("book/getbook/{id}")]
        // GET api/Book/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult GetBook(string id)
        {
            Book book=null;
            string message = "ok";
            try
            {
                book = bookRepository.FindBook(id);
                //db.Books.Find(id);
                if (book == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Ok(new { list=book,message = message});
        }

         [Route("book/Update/{id}")]
        // PUT api/Book/5
        public IHttpActionResult PutBook(string id, Book book)
        {
            string message = "ok";
            if (!ModelState.IsValid)
            {
                message = "Invalid model data.";
               // return BadRequest(ModelState);
            }

            if (id != book.Id)
            {
                message = "bad request!"; 
                //return BadRequest();
            }


            //db.Entry(book).State = EntityState.Modified;

            try
            {
                Book update = bookRepository.Edit(book);
               // db.SaveChanges();
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!BookExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}
           // return StatusCode(HttpStatusCode.NoContent);
            return Ok(new { id = book.Id, message = message });
        }



        [Route("book/post")]
        // POST api/Book
        [ResponseType(typeof(Book))]
        public IHttpActionResult PostBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //db.Books.Add(book);
            // db.SaveChanges();
            string message = "ok";
            book.Id = Guid.NewGuid().ToString("N");
            book.CreatedDate = DateTime.Now;
           // book.CreatedBy = User.Identity.Name;
            try
            {
                Book save = bookRepository.Save(book);
            }
            catch (DbUpdateException)
            {
                if (bookRepository.FindBook(book.Id) !=null)  //BookExists(book.Id)
                {
                    message =book.ISBN + ' ' + book.Title + " already exists!";
                    //return Conflict();
                }
                else
                {
                    message = "Can not Save data due to Server Error.";
                    //throw;
                }
            }

            return Ok(new { id = book.Id, message = message });
          //  return CreatedAtRoute("DefaultApi", new { id = book.Id }, book);
        }

       protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                IDisposable d =bookRepository as IDisposable;
                if (d != null)
                    d.Dispose();
                //GC.SupressFinalize(this);
            }
            base.Dispose(disposing);
        }
        // DELETE api/Book/5
        //[ResponseType(typeof(Book))]
        //public IHttpActionResult DeleteBook(string id)
        //{
        //    Book book = db.Books.Find(id);
        //    if (book == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Books.Remove(book);
        //    db.SaveChanges();

        //    return Ok(book);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool BookExists(string id)
        //{
        //    return db.Books.Count(e => e.Id == id) > 0;
        //}
    }
}