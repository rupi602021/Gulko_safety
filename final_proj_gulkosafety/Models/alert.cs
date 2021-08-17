using final_proj_gulkosafety.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_proj_gulkosafety.Models
{
    public class alert
    {
        int alert_num;
        string content;
        DateTime date;
        int alert_type_num;
        string user_email;
        int status;
        int proj_num;
        string type_name;
        string proj_name;

        public alert(int alert_num, string content,DateTime date,int alert_type_num, string user_email,int status,int proj_num, string type_name, string proj_name)
        {
            Alert_num = alert_num;
            Content = content;
            Date = date;
            Alert_type_num = alert_type_num;
            User_email = user_email;
            Status = status;
            Proj_num = proj_num;
            Type_name = type_name;
            Proj_name = proj_name;
        }
        public alert(int alert_num, string content, DateTime date, int alert_type_num, string user_email, int status, int proj_num, string type_name)
        {
            Alert_num = alert_num;
            Content = content;
            Date = date;
            Alert_type_num = alert_type_num;
            User_email = user_email;
            Status = status;
            Proj_num = proj_num;
            Type_name = type_name;
        }

        public alert() { }

        public int Alert_num { get => alert_num; set => alert_num = value; }
        public string Content { get => content; set => content = value; }
        public DateTime Date { get => date; set => date = value; }
        public int Alert_type_num { get => alert_type_num; set => alert_type_num = value; }
        public string User_email { get => user_email; set => user_email = value; }
        public int Status { get => status; set => status = value; }
        public int Proj_num { get => proj_num; set => proj_num = value; }
        public string Type_name { get => type_name; set => type_name = value; }
        public string Proj_name { get => proj_name; set => proj_name = value; }

        public List<alert> Read(string user_email)
        {
            DBServices dbs = new DBServices();
            List<alert> alertList = dbs.ReadAlerts(user_email);
            return alertList;
        }

        public List<alert> ReadAlertFromHome(string user_email,int Alert_type_num)
        {
            DBServices dbs = new DBServices();
            List<alert> alertList = dbs.ReadAlertFromHome(user_email,Alert_type_num);
            return alertList;
        }
        public List<alert> Read(int proj_num)
        {
            DBServices dbs = new DBServices();
            List<alert> alertList = dbs.ReadAlerts(proj_num);
            return alertList;
        }

        public void UpdateAlert()
        {
            DBServices dbs = new DBServices();
            dbs.UpdateAlert(this);
        }

        public List<alert> ReadAlertArchive(string user_email)
        {
            DBServices dbs = new DBServices();
            List<alert> alertArchiveList = dbs.ReadAlertArchive(user_email);
            return alertArchiveList;
        }

        public void InsertAlert()
        {
            DBServices dbs = new DBServices();
            dbs.InsertAlert(this);
        }
    }
}