using final_proj_gulkosafety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace final_proj_gulkosafety.Controllers
{
    public class user_typeController : ApiController
    {
        // GET all types api/<controller>
        public List<user_type> Get()
        {
            try
            {
                user_type ut = new user_type();
                List<user_type> utList = ut.Read();
                return utList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        // POST new type api/<controller>
        public HttpResponseMessage Post([FromBody] user_type ut)
        {
            try
            {
                {
                    ut.Insert();

                }

                return Request.CreateResponse(HttpStatusCode.Created, ut);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put([FromBody]user_type _user_type)
        {
            try
            {
                {
                    _user_type.UpdateUserType();
                }

                return Request.CreateResponse(HttpStatusCode.Created, _user_type);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete([FromBody] user_type _user_type)
        {

            try
            {
                {
                    _user_type.DeleteUserType(_user_type.User_type_num);
                }

                return Request.CreateResponse(HttpStatusCode.Created, _user_type);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }
    }
}