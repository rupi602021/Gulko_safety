using final_proj_gulkosafety.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_proj_gulkosafety.Models
{
    public class report
    {
        int report_num;
        DateTime date;
        DateTime time;
        string comment;
        double grade;
        int project_num;
        string user_mail;
        string proj_name;


        public int Report_num { get => report_num; set => report_num = value; }
        public DateTime Date { get => date; set => date = value; }
        public DateTime Time { get => time; set => time = value; }
        public string Comment { get => comment; set => comment = value; }
        public double Grade { get => grade; set => grade = value; }
        public int Project_num { get => project_num; set => project_num = value; }
        public string User_mail { get => user_mail; set => user_mail = value; }
        public string Proj_name { get => proj_name; set => proj_name = value; }


        public report(int report_num, DateTime date, DateTime time, string comment, double grade, int project_num, string user_mail, string proj_name)
        {
            Report_num = report_num;
            Date = date;
            Time = time;
            Comment = comment;
            Grade = grade;
            Project_num = project_num;
            User_mail = user_mail;
            Proj_name = proj_name;
        }
        public report(int report_num, DateTime date, DateTime time, string comment, double grade, int project_num, string user_mail)
        {
            Report_num = report_num;
            Date = date;
            Time = time;
            Comment = comment;
            Grade = grade;
            Project_num = project_num;
            User_mail = user_mail;
        }

        public report() { }

        public void InsertReport()
        {
            DBServices dbs = new DBServices();
            dbs.InsertReport(this);
        }

        public List<report> ReadReportFromHome()
        {
            DBServices dbs = new DBServices();
            List<report> reportList = dbs.ReadReportFromHome();
            return reportList;
        }
        public List<report> ReadReport(int proj_num)
        {
            DBServices dbs = new DBServices();
            List<report> reportList = dbs.ReadReport(proj_num);
            return reportList;
        }

        public void DeleteReport(int report_num)
        {
            DBServices dbs = new DBServices();
            dbs.DeleteReport(report_num);

        }
        public report readLastReport(int proj_num)
        {
            DBServices dbs = new DBServices();
            report lastreport = dbs.ReadLastReport(proj_num);
            return lastreport;
        }

        public void UpdateReport()
        {
            DBServices dbs = new DBServices();
            dbs.UpdateReport(this);
        }

        public void UpdateReportGrade(int report_num, double grade)
        {
            DBServices dbs = new DBServices();
            dbs.UpdateReportGrade(report_num,grade);
        }

    }
}