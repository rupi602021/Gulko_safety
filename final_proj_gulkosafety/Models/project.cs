using final_proj_gulkosafety.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_proj_gulkosafety.Models
{
    public class project
    {
        int project_num;
        string name;
        string company;
        string address;
        DateTime start_date;
        DateTime end_date;
        int status;
        string description;
        double safety_lvl;
        int project_type_num;
        string manager_email;
        string foreman_email;
        int delete_status;

        public project(int project_num, string name, string company, string address, DateTime start_date, DateTime end_date, int status, string description, double safety_lvl, int project_type_num, string manager_email, string foreman_email, int delete_status)
        {
            Project_num = project_num;
            Name = name;
            Company = company;
            Address = address;
            Start_date = start_date;
            End_date = end_date;
            Status = status;
            Description = description;
            Safety_lvl = safety_lvl;
            Project_type_num = project_type_num;
            Manager_email = manager_email;
            Foreman_email = foreman_email;
            Delete_status = delete_status;
        }

        public project(int project_num, string manager_email, string foreman_email)
        {
            Project_num = project_num;
            Manager_email = manager_email;
            Foreman_email = foreman_email;
        }

        public int Project_num { get => project_num; set => project_num = value; }
        public string Name { get => name; set => name = value; }
        public string Company { get => company; set => company = value; }
        public string Address { get => address; set => address = value; }
        public DateTime Start_date { get => start_date; set => start_date = value; }
        public DateTime End_date { get => end_date; set => end_date = value; }
        public int Status { get => status; set => status = value; }
        public string Description { get => description; set => description = value; }
        public double Safety_lvl { get => safety_lvl; set => safety_lvl = value; }
        public int Project_type_num { get => project_type_num; set => project_type_num = value; }
        public string Manager_email { get => manager_email; set => manager_email = value; }
        public string Foreman_email { get => foreman_email; set => foreman_email = value; }
        public int Delete_status { get => delete_status; set => delete_status = value; }

        public project() { }

        public List<project> Read()
        {
            DBServices dbs = new DBServices();
            return dbs.ReadProjects();
        }

        public project ReadProject(int project_num)
        {
            DBServices dbs = new DBServices();
            project p = dbs.ReadProject(project_num);
            return p;
        }

        public List<project> Read(string userEmail)
        {
            DBServices dbs = new DBServices();
            return dbs.ReadProjectsType2(userEmail);
        }

        public void Insert()
        {
            DBServices dbs = new DBServices();
            dbs.InsertProject(this);
        }

        public void UpdateProjectDetails()
        {
            DBServices dbs = new DBServices();
            dbs.UpdateProjectDetails(this);

        }

        public void UpdateProjectStatus(int proj_num, int status)
        {
            DBServices dbs = new DBServices();
            dbs.UpdateProjectStatus(proj_num, status);
        }

        public void UpdateProjectUser()
        {
            DBServices dbs = new DBServices();
            dbs.UpdateProjectUser(this);
        }
        public void DeleteProject(int proj_num, int delete_status)
        {
            DBServices dbs = new DBServices();
            dbs.DeleteProject(proj_num, delete_status);

        }
    }
}