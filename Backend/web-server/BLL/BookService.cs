using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using web_server.Entities;
using web_server.Repositories.Models;

namespace web_server.BLL
{
    public static class BookService
    {
        public static async Task AddBook(Book book)
        {
            using(var db = new UnitOfWork())
            {
                await Task.Run(()=> db.Books.Create(book));
            }
        }

        public static async Task<IEnumerable<Book>> GetAllBooks ()
        {
            using (var db = new UnitOfWork())
            {
                return await Task.Run(() => db.Books.GetAll());
            }
        }

        public static async Task<Book> GetBook(int id)
        {
            using (var db = new UnitOfWork())
            {
                return await Task.Run(() => db.Books.Get(id));
            }   
        }

        public static async Task DeleteBook(int id )
        {
            using (var db = new UnitOfWork())
            {
                 await Task.Run(() => db.Books.Delete(id));
            }
        }

        public static async Task EditBook(Book book)
        {
            using (var db = new UnitOfWork())
            {
                await Task.Run(() => db.Books.Update(book));
            }
        }

    }
}