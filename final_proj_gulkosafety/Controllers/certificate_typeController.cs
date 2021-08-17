using final_proj_gulkosafety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace final_proj_gulkosafety.Controllers
{
    public class certificate_typeController : ApiController
    {

        public List<certificate_type> Get()
        {
            try
            {
                certificate_type c = new certificate_type();
                List<certificate_type> cList = c.ReadCertificate_type();
                return cList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        // POST api/<controller>
        public void Post([FromBody] certificate_type _certificate_type)
        {
            _certificate_type.InsertCertificateType();
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put([FromBody]certificate_type _certificate_type)
        {
            try
            {
                {
                    _certificate_type.UpdateCertificateType();
                }

                return Request.CreateResponse(HttpStatusCode.Created, _certificate_type);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete([FromBody] certificate_type _certificate_type)
        {

            try
            {
                {
                    _certificate_type.DeleteCertificateType(_certificate_type.Certificate_type_num);
                }

                return Request.CreateResponse(HttpStatusCode.Created, _certificate_type);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }
    }
}
