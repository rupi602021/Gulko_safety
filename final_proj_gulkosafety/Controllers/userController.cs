using final_proj_gulkosafety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace final_proj_gulkosafety.Controllers
{
    public class userController : ApiController
    {
        // GET all users api/<controller>
        public List<user> Get()
        {
            try
            {
                user u = new user();
                List<user> uList = u.Read();
                return uList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public List<user> Get(string manager_email, string foreman_email)
        {
            try
            {
                user p = new user();
                return p.Read_user_in_project(manager_email, foreman_email);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        [Route("api/user/{email}/{password}")]
        public user getLogInUser(string email,string password)
        {
            try
            {
                user p = new user();
                return p.checkUserLogIn(email, password);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public HttpResponseMessage Post([FromBody] user u)
        {
            try
            {
                {
                    u.InsertUser();

                }

                return Request.CreateResponse(HttpStatusCode.Created, u);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put([FromBody] user _user)
        {
            try
            {
                {
                    _user.UpdateUser();
                }

                return Request.CreateResponse(HttpStatusCode.Created, _user);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete([FromBody] user _user)
        {

            try
            {
                {
                    _user.DeleteUser();
                }

                return Request.CreateResponse(HttpStatusCode.Created, _user);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }


    }
}