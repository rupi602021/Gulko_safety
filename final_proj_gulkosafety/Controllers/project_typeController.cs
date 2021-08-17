using final_proj_gulkosafety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace final_proj_gulkosafety.Controllers
{
    public class project_typeController : ApiController
    {
        // GET all project types api/<controller>
        public List<project_type> Get()
        {
            try
            {
                project_type pt = new project_type();
                List<project_type> ptList = pt.Read();
                return ptList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST new project type api/<controller>
        public HttpResponseMessage Post([FromBody] project_type pt)
        {
            try
            {
                {
                    pt.Insert();

                }

                return Request.CreateResponse(HttpStatusCode.Created, pt);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}