using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Models;
using BookStore.Models.bookStore;
using BookStore.Models.Infrastructure;
using System.Data.Entity;

namespace BookStore.Repository
{
    public class BookRepository :IBook
    {
        ApplicationDbContext contex = new ApplicationDbContext();
        public Book Save(Book book)
        {
            try
            {
                contex.Books.Add(book);
                contex.SaveChanges();
                return book;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Book Edit(Book book)
        {
            try
            {
                contex.Entry(book).State = EntityState.Modified;
                contex.SaveChanges();
                return book;
            }
            catch (Exception ex)
            {
                throw ex;
            }
         }

        public List<Book> GetBookList() {
            try
            {
                return contex.Books.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Book FindBook(string id)
        {
            try
            {
                return contex.Books.Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        }
    }

    interface IBook 
    {
        Book Save(Book book);
        Book Edit(Book book);

        List<Book> GetBookList();

        Book FindBook(string id);
    }
}