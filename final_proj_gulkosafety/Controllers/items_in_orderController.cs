using final_proj_gulkosafety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace final_proj_gulkosafety.Controllers
{
    public class items_in_orderController : ApiController
    {
        //POST api/<controller>
        public HttpResponseMessage Post([FromBody] items_in_order i)
        {
            try
            {
                {
                    i.InsertItemInOrder();

                }

                return Request.CreateResponse(HttpStatusCode.Created, i);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }
        public List<items_in_order> Get(int order_num)
        {
            try
            {
                items_in_order i = new items_in_order();
                List<items_in_order> iList = i.Read(order_num);
                return iList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put([FromBody] items_in_order io)
        {
            try
            {
                {
                    io.UpdateItemInOrder();
                }

                return Request.CreateResponse(HttpStatusCode.Created, io);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }
        //DELETE api/<controller>/5
        public HttpResponseMessage Delete([FromBody] items_in_order _item_in_order)
        {

            try
            {
                {
                    _item_in_order.DeleteItemInOrder(_item_in_order.Item_num, _item_in_order.Order_num);
                }

                return Request.CreateResponse(HttpStatusCode.Created, _item_in_order);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }
    }
}