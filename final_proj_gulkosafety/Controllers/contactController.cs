using final_proj_gulkosafety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace final_proj_gulkosafety.Controllers
{
    public class contactController : ApiController
    {

        public List<contact> Get(int instruction_num)
        {
            try
            {
                contact c = new contact();
                List<contact> cList = c.Read(instruction_num);
                return cList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public List<contact> Get()
        {
            try
            {
                contact c = new contact();
                List<contact> cList = c.Read();
                return cList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //POST api/<controller>
        public HttpResponseMessage Post([FromBody] contact c)
        {
            try
            {
                {
                    c.InsertContact();

                }

                return Request.CreateResponse(HttpStatusCode.Created, c);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }


        // PUT api/<controller>/5
        public HttpResponseMessage Put([FromBody] contact c)
        {
            try
            {
                {
                    c.UpdateContact();
                }

                return Request.CreateResponse(HttpStatusCode.Created, c);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }
    }
}