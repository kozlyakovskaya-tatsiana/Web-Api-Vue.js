using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using web_server.Entities;
using web_server.Models.Account;
using web_server.Repositories.Models;

namespace web_server.BLL
{
    public class UserService
    {
        public static async Task<IEnumerable<Book>> GetUserBookList(string userName)
        {
            using (var db = new UnitOfWork())
            {
                var user = await Task.Run(()=> db.UserManager.Users.Include(u => u.Books).FirstOrDefault(u => u.UserName == userName))
                    ?? throw new Exception("There is no such user in the database");

                var books = user.Books;

                return books;

            }
        }

        public static async Task<IEnumerable<ApplicationUser>> GetUsers()
        {
            using (var db = new UnitOfWork())
            {
                return  await Task.Run(()=>db.UserManager.Users.ToArray());
            }
        }

        public static async Task AddBookToList(int bookId, string userName)
        {
            using (var db = new UnitOfWork())
            {
                var users = await Task.Run(()=> db.UserManager.Users.Include(u => u.Books).ToArray());

                var book = await Task.Run(()=>db.Books.Get(bookId)) ??
                    throw new Exception("There is no such book in the database");

                var user = users.FirstOrDefault(u => u.UserName == userName) ??
                    throw new Exception("There is no such user in the database");

                if (user.Books.FirstOrDefault(b => b.Id == bookId) != null)
                    throw new Exception("This book is already in your booklist");

                await Task.Run(()=>user.Books.Add(book));

                db.Save();

            }
        }

        public static async Task DeleteBookFromUserList(int bookId, string userName)
        {
            using (var db = new UnitOfWork())
            {
                var book = await Task.Run(() => db.Books.Get(bookId)) ??
                    throw new Exception("There is no such book in database");

                var user =  await Task.Run(()=>db.UserManager.Users.Include(u=>u.Books).FirstOrDefault(u=>u.UserName==userName))??
                     throw new Exception("There is no such user in database");

                if (!user.Books.Contains(book))
                    throw new Exception("User doesn't have this book");

                user.Books.Remove(book);

                db.Save();

            }
        }
    }
}
