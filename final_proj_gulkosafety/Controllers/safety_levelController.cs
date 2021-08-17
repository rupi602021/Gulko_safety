using final_proj_gulkosafety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace final_proj_gulkosafety.Controllers
{
    public class safety_levelController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        public SafetyLevel PutSafetyLevel([FromBody] SafetyLevel safety_lvl)
        {
            try
            {
                {
                    return safety_lvl.calcGradeAndSafetyLVL();
                }

            }
            catch (Exception ex)
            {
                return safety_lvl;
            }
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}