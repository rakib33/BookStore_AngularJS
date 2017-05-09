using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Models;
using BookStore.Models.Infrastructure;
using BookStore.Models.bookStore;
using System.Data.Entity;

namespace BookStore.Repository
{
    public class AuthorRepository :IAuthor
    {
       ApplicationDbContext contex = new ApplicationDbContext();
       public Author Save(Author author)
        {
            try
            {
                contex.Authors.Add(author);
                contex.SaveChanges();
                return author;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Author Edit(Author author)
        {
            try
            {
                contex.Entry(author).State = EntityState.Modified;
                contex.SaveChanges();
                return author;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Author> GetAuthorList()
        {
            try
            {

                //List<Author> list = new List<Author> { 
                // new Author{ Id="1", FirstName="Rakibul", LastName="Islam", Address="Dhaka", Country="BD"},
                // new Author{ Id="2", FirstName="Masud", LastName="Islam", Address="Dhaka", Country="BD"},
                //};
                //return list;
                return contex.Authors.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Author FindAuthor(string id)
        {
            try
            {
                return contex.Authors.Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }

    interface IAuthor
    {
        Author Save(Author author);
        Author Edit(Author author);

        List<Author> GetAuthorList();

        Author FindAuthor(string id);

    }
}