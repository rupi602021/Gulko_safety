
using final_proj_gulkosafety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace final_proj_gulkosafety.Controllers
{
    public class itemController : ApiController
    {
        public List<item> Get()
        {
            try
            {
                item i = new item();
                List<item> iList = i.ReadItem();
                return iList;
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

        // POST api/<controller>
        public void Post([FromBody] item _item)
        {
            _item.InsertItem();
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put([FromBody]item _item)
        {
            try
            {
                {
                    _item.UpdateItem();
                }

                return Request.CreateResponse(HttpStatusCode.Created, _item);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete([FromBody] item _item)
        {

            try
            {
                {
                    _item.DeleteItem(_item.Item_num);
                }

                return Request.CreateResponse(HttpStatusCode.Created, _item);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }
    }
}
