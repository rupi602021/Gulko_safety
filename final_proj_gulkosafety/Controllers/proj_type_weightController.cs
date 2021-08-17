using final_proj_gulkosafety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace final_proj_gulkosafety.Controllers
{
    public class proj_type_weightController : ApiController
    {
        // GET api/<controller>
        public List<proj_type_weight> Get(int proj_type)
        {
            proj_type_weight ptw = new proj_type_weight();
            List<proj_type_weight> proj_type_weightList = ptw.ReadProj_type_weight(proj_type);
            return proj_type_weightList;
        }

        // GET api/<controller>/5
      

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}