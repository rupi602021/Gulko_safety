using final_proj_gulkosafety.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_proj_gulkosafety.Models
{
    public class defect_in_report
    {
        int report_num;
        int defect_num;
        DateTime fix_date;
        DateTime fix_time;
        string picture_link;
        int fix_status;
        string description;
        string defect_name;
        string defect_type_name;


        public int Report_num { get => report_num; set => report_num = value; }
        public int Defect_num { get => defect_num; set => defect_num = value; }
        public DateTime Fix_date { get => fix_date; set => fix_date = value; }
        public DateTime Fix_time { get => fix_time; set => fix_time = value; }
        public string Picture_link { get => picture_link; set => picture_link = value; }
        public int Fix_status { get => fix_status; set => fix_status = value; }
        public string Description { get => description; set => description = value; }
        public string Defect_name { get => defect_name; set => defect_name = value; }
        public string Defect_type_name { get => defect_type_name; set => defect_type_name = value; }

        public defect_in_report() { }

        public defect_in_report(int report_num, int defect_num, DateTime fix_date, DateTime fix_time, string picture_link, int fix_status, string description, string defect_name, string defect_type_name)
        {
            Report_num = report_num;
            Defect_num = defect_num;
            Fix_date = fix_date;
            Fix_time = fix_time;
            Picture_link = picture_link;
            Fix_status = fix_status;
            Description = description;
            Defect_name = defect_name;
            Defect_type_name = defect_type_name;
        }

        public List<defect_in_report> ReadDefectsInReport(int report_num)
        {
            DBServices dbs = new DBServices();
            List<defect_in_report> defectsInReportList = dbs.ReadDefectsInReport(report_num);
            return defectsInReportList;
        }
        public void DeleteDefectInReport(int report_num, int defect_num)
        {
            DBServices dbs = new DBServices();
            dbs.DeleteDefectInReport(report_num, defect_num);

        }
        public void UpdateDefectInReport()
        {
            DBServices dbs = new DBServices();
            dbs.UpdateDefectInReport(this);

        }
        public void InsertDefectInReport()
        {
            DBServices dbs = new DBServices();
            dbs.InsertDefectInReport(this);
        }

        public List<defect_in_report> readLastReportDefect(int proj_num,DateTime date)
        {
            DBServices dbs = new DBServices();
            List<defect_in_report> lastReportDefect = dbs.readLastReportDefects(proj_num, date);
            return lastReportDefect;


        }
        public void UpdateDefectInReportImg(string picture_link)
        {
            DBServices dbs = new DBServices();
            dbs.UpdateDefectInReportImg(this, picture_link);

        }

        public void UpdateDefectInReportStatus(string picture_link, int fix_status)
        {
            DBServices dbs = new DBServices();
            dbs.UpdateDefectInReportStatus(picture_link, fix_status);
        }
    }
}