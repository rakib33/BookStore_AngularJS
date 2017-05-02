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
    public class BookController : ApiController
    {
        private IBook bookRepository = null;
        private BookController() {
            bookRepository = new BookRepository();
        }
        private ApplicationDbContext db = new ApplicationDbContext();


       

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
            return Ok(new { msg = message, list = book });
        }

        // GET api/Book/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult GetBook(string id)
        {
            
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // PUT api/Book/5
        public IHttpActionResult PutBook(string id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.Id)
            {
                return BadRequest();
            }

            db.Entry(book).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Book
        [ResponseType(typeof(Book))]
        public IHttpActionResult PostBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Books.Add(book);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BookExists(book.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = book.Id }, book);
        }

        // DELETE api/Book/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult DeleteBook(string id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            db.Books.Remove(book);
            db.SaveChanges();

            return Ok(book);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookExists(string id)
        {
            return db.Books.Count(e => e.Id == id) > 0;
        }
    }
}