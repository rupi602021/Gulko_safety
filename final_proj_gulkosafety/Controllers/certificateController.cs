using final_proj_gulkosafety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace final_proj_gulkosafety.Controllers
{
    public class certificateController : ApiController
    {

        public List<certificate> Get()
        {
            try
            {
                certificate c = new certificate();
                List<certificate> cList = c.ReadCertificate();
                return cList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public List<certificate> Get(DateTime date)
        {
            try
            {
                certificate c = new certificate();
                List<certificate> cList = c.ReadExpiredCertificates(date);
                return cList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public HttpResponseMessage Post([FromBody] certificate c)
        {
            try
            {
                {
                    c.Insert();

                }

                return Request.CreateResponse(HttpStatusCode.Created, c);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage DeleteCertificate([FromBody] certificate c)
        {

            try
            {
                {
                    c.DeleteCertificate(c.Certificate_num,c.Delete_status);
                }

                return Request.CreateResponse(HttpStatusCode.Created, c);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }
        //update certificate
        public HttpResponseMessage Put([FromBody] certificate c)
        {
            try
            {
                {
                    c.UpdateCertificate();
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