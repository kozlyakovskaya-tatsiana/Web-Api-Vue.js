using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using web_server.EF;
using web_server.Entities;
using web_server.Models.Account;
using web_server.Repositories.Models;
using System.Data.Entity;

namespace web_server.Controllers
{
   // [Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
        public IHttpActionResult Get()
        {
            /*using (var db = new UnitOfWork())
            {
                return Ok(db.Authors.GetAll());   
            }*/
            return Ok();
        }

        // GET api/values/5
        public IHttpActionResult Get(int id)
        {
            //return Content(HttpStatusCode.Unauthorized, "errrrro 7855");
            return Content(HttpStatusCode.BadRequest, "erroorrr bad request");
        }

        // POST api/values
       /* public IHttpActionResult Post(AddBookToUserModel model)
        {

        }*/

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
