using final_proj_gulkosafety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace final_proj_gulkosafety.Controllers
{
    public class reportController : ApiController
    {
        public List<report> Get()
        {
            try
            {
                report r = new report();
                List<report> reprotList = r.ReadReportFromHome();
                return reprotList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public List<report> Get(int proj_num)
        {
            try
            {
                report r = new report();
                List<report> reprotList = r.ReadReport(proj_num);
                return reprotList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [Route("api/report/{project_num}/lastReport")]
        public report GetLastReport(int project_num)
        {
            try
            {
                report r = new report();
                return r.readLastReport(project_num);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public HttpResponseMessage Post([FromBody] report r)
        {
            try
            {
                {
                    r.InsertReport();

                }

                return Request.CreateResponse(HttpStatusCode.Created, r);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        public void DeleteReport(int report_num)
        {
            report r = new report();
            r.DeleteReport(report_num);

        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put([FromBody] report r)
        {
            try
            {
                {
                    r.UpdateReport(); ;

                }

                return Request.CreateResponse(HttpStatusCode.Created, r);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        public void PUTReportGrage(int report_num,double grade)
        {
            report r = new report();
            r.UpdateReportGrade(report_num, grade);

        }
    }
}