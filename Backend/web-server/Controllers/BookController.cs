using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using web_server.BLL;
using web_server.Entities;
using web_server.Repositories.Models;

namespace web_server.Controllers
{
    [Authorize]
    public class BookController : ApiController
    {
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IHttpActionResult> AddBook(Book book)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    await BookService.AddBook(book);

                    return Ok();
                }
                else
                {
                    return BadRequest("Input data is incorrect. Check it.");
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
           
        }

        public async Task<IHttpActionResult> GetAllBooks()
        {
            try
            {
                var books=await BookService.GetAllBooks();

                return Ok(books);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        public async Task<IHttpActionResult> GetBook(int id)
        {
            try
            {
                var book = await BookService.GetBook(id) ??
                    throw new Exception("The book is not found");

                return Ok(book);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [Authorize(Roles = "admin")]
        public async Task<IHttpActionResult> DeleteBook(int id)
        {
            try
            {
                await BookService.DeleteBook(id);
                
                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IHttpActionResult> EditBook(Book book)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    await BookService.EditBook(book);

                    return Ok();
                }
                else
                {
                    return BadRequest("Input data is incorrect. Check it.");
                }

            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

    }
}
