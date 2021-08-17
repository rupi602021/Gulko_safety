using final_proj_gulkosafety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace final_proj_gulkosafety.Controllers
{
    public class defectController : ApiController
    {

        public List<defect> Get()
        {
            try
            {
                defect _defect = new defect();
                return _defect.ReadDefect();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] defect _defect)
        {
            try
            {
                {
                    _defect.InsertDefect();

                }

                return Request.CreateResponse(HttpStatusCode.Created, _defect);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put([FromBody] defect _defect)
        {
            try
            {
                {
                    _defect.UpdateDefect();
                }

                return Request.CreateResponse(HttpStatusCode.Created, _defect);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete([FromBody] defect _defect)
        {

            try
            {
                {
                    _defect.DeleteDefect(_defect.Defect_num);
                }

                return Request.CreateResponse(HttpStatusCode.Created, _defect);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }
    }
}