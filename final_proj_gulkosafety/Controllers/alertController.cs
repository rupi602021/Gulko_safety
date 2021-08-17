using final_proj_gulkosafety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace final_proj_gulkosafety.Controllers
{
    public class alertController : ApiController
    {
        public List<alert> Get(string user_email)
        {
            try
            {
                alert a = new alert();
                List<alert> aList = a.Read(user_email);
                return aList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public List<alert> Get(string user_email,int alert_type_num)
        {
            try
            {
                alert a = new alert();
                List<alert> aList = a.ReadAlertFromHome(user_email,alert_type_num);
                return aList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public List<alert> Get(int proj_num)
        {
            try
            {
                alert a = new alert();
                List<alert> alertList = a.Read(proj_num);
                return alertList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        //  GET Alert Archive
        [Route("api/alert/{user_email}/AlertArchive")]
        public List<alert> GetAlertArchive(string user_email)
        {
            alert a = new alert();
            List<alert> alertArchiveList = a.ReadAlertArchive(user_email);
            return alertArchiveList;

        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] alert a)
        {
            try
            {
                {
                    a.InsertAlert();

                }

                return Request.CreateResponse(HttpStatusCode.Created, a);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put([FromBody] alert a)
        {
            try
            {
                {
                    a.UpdateAlert();

                }
                return Request.CreateResponse(HttpStatusCode.OK, a);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, ex.Message);
            }
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}