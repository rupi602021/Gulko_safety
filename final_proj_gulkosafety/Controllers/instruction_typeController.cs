using final_proj_gulkosafety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace final_proj_gulkosafety.Controllers
{
    public class instruction_typeController : ApiController
    {
        public List<instruction_type> Get()
        {
            try
            {
                instruction_type i = new instruction_type();
                List<instruction_type> iList = i.ReadInstruction_type();
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
        public void Post([FromBody] instruction_type _instruction_type)
        {
            _instruction_type.InsertInstructionType();
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put([FromBody]instruction_type _instruction_type)
        {
            try
            {
                {
                    _instruction_type.UpdateInstructionType();
                }

                return Request.CreateResponse(HttpStatusCode.Created, _instruction_type);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete([FromBody] instruction_type _instruction_type)
        {

            try
            {
                {
                    _instruction_type.DeleteInstructionType(_instruction_type.Instruction_type_num);
                }

                return Request.CreateResponse(HttpStatusCode.Created, _instruction_type);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }
    }
}