using final_proj_gulkosafety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace final_proj_gulkosafety.Controllers
{
    public class defect_in_reportController : ApiController
    {
        public List<defect_in_report> Get(int report_num)
        {
            try
            {
                defect_in_report defectsInReport = new defect_in_report();
                List<defect_in_report> defect_in_reportList = defectsInReport.ReadDefectsInReport(report_num);
                return defect_in_reportList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        //מחזירה את כל הליקוים בדוח שלפני הדוח הנוכחי. בשביל השוואה בין הדוחות, לאלגוריתם החכם
        public List<defect_in_report> GetLastReportDefects(int proj_num,DateTime reportDate)
        {
            try
            {
                defect_in_report LastReportDefect = new defect_in_report();
                List<defect_in_report> LastReportDefects = LastReportDefect.readLastReportDefect(proj_num, reportDate);
                return LastReportDefects;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public HttpResponseMessage DeleteDefectInReport([FromBody] defect_in_report dr)
        {
            try
            {
                {
                    dr.DeleteDefectInReport(dr.Report_num, dr.Defect_num);
                }

                return Request.CreateResponse(HttpStatusCode.Created, dr);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        public HttpResponseMessage Post([FromBody] defect_in_report p)
        {
            try
            {
                {
                    p.InsertDefectInReport();

                }

                return Request.CreateResponse(HttpStatusCode.Created, p);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }

        }

        //update defect in report without 
        public HttpResponseMessage Put([FromBody] defect_in_report defectInReport)
        {
            try
            {
                {
                    defectInReport.UpdateDefectInReport();
                }

                return Request.CreateResponse(HttpStatusCode.Created, defectInReport);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        //[Route("api/defect_in_report/updateImage")]
        public HttpResponseMessage PutImg(string picture_link, [FromBody] defect_in_report defectInReport)
        {
           
            try
            {
                {
                    defectInReport.UpdateDefectInReportImg(picture_link);

                }

                return Request.CreateResponse(HttpStatusCode.Created, defectInReport);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        public HttpResponseMessage PutDefectStatus(string picture_link, int fix_status)
        {

            try
            {
                {
                    defect_in_report df = new defect_in_report();
                    df.UpdateDefectInReportStatus(picture_link, fix_status);

                }

                return Request.CreateResponse(HttpStatusCode.Created, fix_status);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }


    }
}