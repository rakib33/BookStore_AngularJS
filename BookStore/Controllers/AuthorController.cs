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
   // [RoutePrefix("api/author")]
    public class AuthorController : ApiController
    {
        private IAuthor authorRepository = null;

        public AuthorController() {
            authorRepository = new AuthorRepository();
        }
               
        
       // private ApplicationDbContext db = new ApplicationDbContext();

      
       
        [Route("author/list")]
        //default GET api/Author
        public IHttpActionResult GetAuthors()
        {
            List<Author> author = new List<Author>();
            string message = "ok";
            try
            {
                author =authorRepository.GetAuthorList();
            }
            catch (Exception ex)
            {               
                message = ex.Message;
            }
         
            return Ok(new { list = author,message=message });
          
        }

        [Route("author/getAuthor/{id}")]
        //default uri GET api/Author/5
        [ResponseType(typeof(Author))]
        public IHttpActionResult GetAuthor(string id)
        {
            Author author = authorRepository.FindAuthor(id); 
            if (author == null)
            {
                return NotFound();
            }

            return Ok(new { list = author });
        }


        [Route("author/Update/{id}")]
        //Default uri PUT api/Author/5
        public IHttpActionResult PutAuthor(string id, Author author)
        {
            string message = "ok";
            if (!ModelState.IsValid)
            {
                message = "Invalid model data.";              
            }

            if (id != author.Id)
            {
                message = "bad request!";              
            }

            try
            {
                Author update = authorRepository.Edit(author);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            #region PriviousCode
            //db.Entry(author).State = EntityState.Modified;
            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!AuthorExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}
            #endregion

            return Ok(new { id = author.Id,message=message });
          
        }

        // POST api/Author
        [Route("author/post")]
        [ResponseType(typeof(Author))]
        public IHttpActionResult PostAuthor(Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string message = "ok";

            author.Id = Guid.NewGuid().ToString("N"); 
            author.CreatedDate = DateTime.Now;
            author.CreatedBy = author.FirstName;

            try
            {
                Author save = authorRepository.Save(author);
            }
            catch (DbUpdateException)
            {
                if (authorRepository.FindAuthor(author.Id) !=null)   //if (AuthorExists(author.Id))
                {
                    message =author.FirstName+' '+author.LastName +" already exists!";
                    //return Conflict();
                }
                else
                {
                    message="Can not Save data due to Server Error.";
                   // throw;
                }
            }
            return Ok(new {id= author.Id,message=message });
         
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                IDisposable d = authorRepository as IDisposable;
                if (d != null)
                    d.Dispose();
                //GC.SupressFinalize(this);
            }
            base.Dispose(disposing);
        }
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region BlockCode
        //// DELETE api/Author/5
        //[ResponseType(typeof(Author))]
        //public IHttpActionResult DeleteAuthor(string id)
        //{
        //    Author author = db.Authors.Find(id);
        //    if (author == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Authors.Remove(author);
        //    db.SaveChanges();

        //    return Ok(author);
        //}

        //private bool AuthorExists(string id)
        //{
        //    return db.Authors.Count(e => e.Id == id) > 0;
        //}
        #endregion
    }
}