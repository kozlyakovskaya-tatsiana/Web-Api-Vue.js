using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using web_server.BLL;
using web_server.Models.Account;
using web_server.Repositories.Models;

namespace web_server.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        public async Task<IHttpActionResult> GetUserBookList()
        {
            try
            {
                var userName = User.Identity.Name ?? throw new Exception("User is not defined");

                var books = await Task.Run(()=>UserService.GetUserBookList(userName));

                return Ok(books);

            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("api/User/users")]
        public async Task<IHttpActionResult> GetUsers()
        {
            try
            {
                var users = await Task.Run(() => UserService.GetUsers());

                return Ok(users);

            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> AddBookToUserList([FromBody]int bookId)
        {
            try
            {
                var userName = User.Identity.Name ?? throw new Exception("User is not defined");

                await UserService.AddBookToList(bookId, userName);

                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("api/User/{bookId}")]
        public async Task<IHttpActionResult> DeleteBookFromUserList([FromUri]int bookId)
        {
            try
            {
                var userName = User.Identity.Name ?? throw new Exception("User is not defined");

                await UserService.DeleteBookFromUserList(bookId, userName);

                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message); 
            }
        }
    }
}
