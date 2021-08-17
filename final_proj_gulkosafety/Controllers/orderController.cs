using final_proj_gulkosafety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace final_proj_gulkosafety.Controllers
{
    public class orderController : ApiController
    {
        public List<order> Get()
        {
            try
            {
                order o = new order();
                List<order> oList = o.ReadOrder();
                return oList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public HttpResponseMessage Post([FromBody] order o)
        {
            try
            {
                {
                    o.Insert();

                }

                return Request.CreateResponse(HttpStatusCode.Created, o);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }
        //update order
        public HttpResponseMessage Put([FromBody] order o)
        {
            try
            {
                {
                    o.UpdateOrder();
                }

                return Request.CreateResponse(HttpStatusCode.Created, o);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }
        // DELETE api/<controller>/5
        public HttpResponseMessage DeleteOrder([FromBody] order o)
        {

            try
            {
                {
                    o.DeleteOrder(o.Order_num, o.Delete_status);
                }

                return Request.CreateResponse(HttpStatusCode.Created, o);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }
    }
}