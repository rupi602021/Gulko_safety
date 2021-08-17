using final_proj_gulkosafety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace final_proj_gulkosafety.Controllers
{
    public class instructionController : ApiController
    {
        public List<instruction> Get()
        {
            try
            {
                instruction i = new instruction();
                List<instruction> iList = i.ReadInstruction();
                return iList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public List<instruction> Get(DateTime date)
        {
            try
            {
                instruction i = new instruction();
                List<instruction> iList = i.ReadExpiredInstruction(date);
                return iList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //POST api/<controller>
        public HttpResponseMessage Post([FromBody] instruction i)
        {
            try
            {
                {
                    i.InsertInstruction();

                }

                return Request.CreateResponse(HttpStatusCode.Created, i);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put([FromBody] instruction i)
        {
            try
            {
                {
                    i.UpdateInstruction();
                }

                return Request.CreateResponse(HttpStatusCode.Created, i);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage DeleteInstruction([FromBody] instruction i)
        {

            try
            {
                {
                    i.DeleteInstruction(i.Instruction_num, i.Delete_status);
                }

                return Request.CreateResponse(HttpStatusCode.Created, i);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }
    }
}