using final_proj_gulkosafety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace final_proj_gulkosafety.Controllers
{
    public class testDinRController : ApiController
    {
        // GET api/<controller>

        // GET api/<controller>/5


        // POST api/<controller>

        // PUT api/<controller>/5


        // DELETE api/<controller>/5
        public HttpResponseMessage PutImg(string picture_link, [FromBody] defect_in_report defectInReport)
        {

            try
            {
                {
                    defectInReport.UpdateDefectInReportImg(picture_link);

                }

                return Request.CreateResponse(HttpStatusCode.Created, defectInReport);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }
    }
}