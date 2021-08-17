using final_proj_gulkosafety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace final_proj_gulkosafety.Controllers
{
    public class contact_in_instructionController : ApiController
    {


        //POST api/<controller>
        public HttpResponseMessage Post([FromBody] contact_in_instruction c)
        {
            try
            {
                {
                    c.InsertContactInInstruction();

                }

                return Request.CreateResponse(HttpStatusCode.Created, c);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }


        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }


        // DELETE api/<controller>/5
        public HttpResponseMessage DeleteContactInInstruction([FromBody] contact_in_instruction c)
        {

            try
            {
                {
                    c.DeleteContactInInstruction(c.Contact_id,c.Instruction_num);
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