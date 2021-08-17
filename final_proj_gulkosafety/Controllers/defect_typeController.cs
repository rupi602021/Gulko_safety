using final_proj_gulkosafety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace final_proj_gulkosafety.Controllers
{
    public class defect_typeController : ApiController
    {
        public List<defect_type> Get()
        {
            try
            {
                defect_type _defect_type = new defect_type();
                List<defect_type> defectTypeList = _defect_type.ReadDefectType();
                return defectTypeList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //POST api/<controller>
         public void Post([FromBody] defect_type _defect_type)
        {
            _defect_type.InsertDefectType();
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put( [FromBody]defect_type _defect_type)
        {
            try
            {
                {
                    _defect_type.UpdateDefectType();
                }

                return Request.CreateResponse(HttpStatusCode.Created, _defect_type);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete([FromBody] defect_type _defect_type)
        {

            try
            {
                {
                    _defect_type.DeleteDefectType(_defect_type.Defect_type_num);
                }

                return Request.CreateResponse(HttpStatusCode.Created, _defect_type);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }
    }
}