using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace final_proj_gulkosafety.Models.DAL
{
    public class DBServices
    {
        public SqlDataAdapter da;
        public DataTable dt;

        //--------------------------------------------------------------------------------------------------
        // This method creates a connection to the database according to the connectionString name in the web.config 
        //--------------------------------------------------------------------------------------------------
        public SqlConnection connect(String conString)
        {
            // read the connection string from the configuration file
            string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }
        //insert a new user
        public int InsertUser(user _user)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildInsertCommand(_user);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        private String BuildInsertCommand(user _user)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}','{3}', '{4}','{5}')", _user.Email, _user.Name, _user.Phone, _user.Password, _user.User_type_num,_user.Delete_status);
            String prefix = "INSERT INTO [user] " + "(email,name,phone,password,user_type_num,delete_status)";
            command = prefix + sb.ToString();

            return command;

        }
        //insert a new defect
        public int InsertDefect(defect def)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildInsertCommand(def);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        private String BuildInsertCommand(defect _defect)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}')", _defect.Name, _defect.Grade, _defect.Defect_type_num);
            String prefix = "INSERT INTO defect " + "(name, grade,defect_type_num)";
            command = prefix + sb.ToString();

            return command;

        }
        private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure

            return cmd;
        }


        // get all defect
        public List<defect> ReadDefect()
        {
            SqlConnection con = null;
            List<defect> defectList = new List<defect>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM defect";
                SqlCommand cmd = new SqlCommand(selectSTR, con);


                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {
                    defect _defect = new defect();
                    _defect.Defect_num = Convert.ToInt32(dr["defect_num"]);
                    _defect.Name = (string)dr["name"];
                    _defect.Grade = Convert.ToDouble(dr["grade"]);
                    _defect.Defect_type_num = Convert.ToInt32(dr["defect_type_num"]);

                    defectList.Add(_defect);
                }

                return defectList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }
        //update project user
        public int UpdateProjectUser(project p)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildupdateContactsCommand(p);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        private String BuildupdateContactsCommand(project p)
        {
            String command;
            command = "UPDATE project SET manager_email='" + p.Manager_email + "', foreman_email='" + p.Foreman_email + "' WHERE project_num =" + p.Project_num;

            return command;
        }



        //update project detail
        public int UpdateProjectDetails(project p)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildupdateCommand(p);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        private String BuildupdateCommand(project p)
        {
            String command;
            command = "UPDATE project SET name='" + p.Name + "', company='" + p.Company + "', address='" + p.Address + "', start_date='" + p.Start_date.ToString("yyyy-MM-dd") + "', end_date='" + p.End_date.ToString("yyyy-MM-dd") + "', status=" + p.Status + ", description='" + p.Description + "', project_type_num=" + p.Project_type_num + ", manager_email='" + p.Manager_email + "', foreman_email='" + p.Foreman_email + "', delete_status=" + p.Delete_status + " WHERE project_num =" + p.Project_num;

            return command;
        }

        //update project safety level
        public int UpdateProjectSafetyLvl(int project_num, double safety_lvl)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildupdateSafetyLvlCommand(project_num, safety_lvl);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        private String BuildupdateSafetyLvlCommand(int project_num, double safety_lvl)
        {
            String command;
            command = "UPDATE project SET safety_lvl=" + safety_lvl + " WHERE project_num =" + project_num;
            return command;
        }

        //update project status
        public int UpdateProjectStatus(int proj_num, int status)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            String cStr = BuildupdateCommand(proj_num, status);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }
        private String BuildupdateCommand(int proj_num, int status)
        {
            String command;
            command = "UPDATE project SET status=" + status + "WHERE project_num =" + proj_num;

            return command;
        }

        //returns all projects
        public List<project> ReadProjects()
        {
            SqlConnection con = null;
            List<project> projectList = new List<project>();

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "";

                selectSTR = "select * from project";



                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {
                    project p = new project();

                    p.Project_num = Convert.ToInt32(dr["project_num"]);
                    p.Name = (string)dr["name"];
                    p.Company = (string)dr["company"];
                    p.Address = (string)dr["address"];
                    p.Start_date = Convert.ToDateTime(dr["start_date"]);
                    p.End_date = Convert.ToDateTime(dr["end_date"]);
                    p.Status = Convert.ToInt32(dr["status"]);
                    p.Description = (string)dr["description"];
                    p.Safety_lvl = Convert.ToDouble(dr["safety_lvl"]);
                    p.Project_type_num = Convert.ToInt32(dr["project_type_num"]);
                    p.Manager_email = (string)dr["manager_email"];
                    p.Foreman_email = (string)dr["foreman_email"];
                    p.Delete_status = Convert.ToInt32(dr["delete_status"]);

                    projectList.Add(p);


                }

                return projectList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        //returns one project by his num
        public project ReadProject(int project_num)
        {
            SqlConnection con = null;
            project project = new project();

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "";

                selectSTR = "select * from project where project_num=" + project_num;



                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {
                    project.Project_num = Convert.ToInt32(dr["project_num"]);
                    project.Name = (string)dr["name"];
                    project.Company = (string)dr["company"];
                    project.Address = (string)dr["address"];
                    project.Start_date = Convert.ToDateTime(dr["start_date"]);
                    project.End_date = Convert.ToDateTime(dr["end_date"]);
                    project.Status = Convert.ToInt32(dr["status"]);
                    project.Description = (string)dr["description"];
                    project.Safety_lvl = Convert.ToDouble(dr["safety_lvl"]);
                    project.Project_type_num = Convert.ToInt32(dr["project_type_num"]);
                    project.Manager_email = (string)dr["manager_email"];
                    project.Foreman_email = (string)dr["foreman_email"];
                    project.Delete_status = Convert.ToInt32(dr["delete_status"]);
                }

                return project;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        //return all users in project
        public List<user> Read_user_in_project(string manager_email, string foreman_email)
        {
            SqlConnection con = null;
            List<user> userList = new List<user>();

            try
            {
                con = connect("DBConnectionString");

                String selectSTR = "  select * from [user]  WHERE email='" + manager_email + "' or email='" + foreman_email + "'";

                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {
                    user user_proj = new user();

                    user_proj.Email = (string)dr["email"];
                    user_proj.Name = (string)dr["name"];
                    user_proj.Phone = (string)dr["phone"];
                    user_proj.Password = (string)dr["password"];
                    user_proj.User_type_num = Convert.ToInt32(dr["user_type_num"]);
                    userList.Add(user_proj);

                }

                return userList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }
        //read projects' report
        public List<report> ReadReport(int proj_num)
        {
            SqlConnection con = null;
            List<report> reportList = new List<report>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM report WHERE project_num=" + proj_num;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    report _report = new report();
                    _report.Report_num = Convert.ToInt32(dr["report_num"]);
                    _report.Date = Convert.ToDateTime(dr["date"]);
                    _report.Time = Convert.ToDateTime(dr["time"]);
                    _report.Comment = (string)dr["comment"];
                    _report.Grade = Convert.ToDouble(dr["grade"]);
                    _report.Project_num = Convert.ToInt32(dr["project_num"]);
                    _report.User_mail = (string)dr["user_email"];
                    reportList.Add(_report);
                }

                return reportList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }
        //change delete status project 
        public int DeleteProject(int proj_num, int delete_status)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            String cStr = BuildDeleteProjCommand(proj_num, delete_status);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }
        private String BuildDeleteProjCommand(int proj_num, int delete_status)
        {
            String command;
            command = "UPDATE project SET delete_status = " + delete_status + " WHERE project_num=" + proj_num;
            return command;
        }

        //delete report 
        public int DeleteReport(int report_num)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            String cStr = BuildDeleteReportCommand(report_num);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }
        private String BuildDeleteReportCommand(int report_num)
        {
            String command;
            command = "delete from report where report_num=" + report_num;
            return command;
        }

        //insert a new report
        public int InsertReport(report _report)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildInsertCommand(_report);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        private String BuildInsertCommand(report _report)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}','{1}','{2}','{3}','{4}')", _report.Date.ToString("yyyy-MM-dd"), _report.Time.ToString("HH:mm"), _report.Grade, _report.Comment, _report.Project_num);
            String prefix = "INSERT INTO report " + "(date,time,grade,comment,project_num)";
            command = prefix + sb.ToString();

            return command;

        }

        //insert a new project
        public int InsertProject(project _project)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildInsertCommand(_project);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
        private String BuildInsertCommand(project _project)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}','{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}','{11}')", _project.Name, _project.Company, _project.Address, _project.Start_date.ToString("yyyy-MM-dd"), _project.End_date.ToString("yyyy-MM-dd"), _project.Status, _project.Description, _project.Safety_lvl, _project.Project_type_num, _project.Manager_email, _project.Foreman_email,_project.Delete_status);
            String prefix = "INSERT INTO project " + "(name,company,address,start_date,end_date,status,description,safety_lvl,project_type_num,manager_email,foreman_email,delete_status)";
            command = prefix + sb.ToString();

            return command;

        }

        //read all defects in report
        public List<defect_in_report> ReadDefectsInReport(int report_num)
        {
            SqlConnection con = null;
            List<defect_in_report> defectsInReportList = new List<defect_in_report>();

            try
            {
                con = connect("DBConnectionString");

                String selectSTR = "select di.*,d.name,dt.type_name,dt.defect_type_num from defect_in_report di inner join defect d on di.defect_num=d.defect_num inner join defect_type dt on d.defect_type_num=dt.defect_type_num where di.report_num=" + report_num;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    defect_in_report _defectInReport = new defect_in_report();

                    _defectInReport.Report_num = Convert.ToInt32(dr["report_num"]);
                    _defectInReport.Defect_num = Convert.ToInt32(dr["defect_num"]);
                    _defectInReport.Fix_date = Convert.ToDateTime(dr["fix_date"]);
                    _defectInReport.Fix_time = Convert.ToDateTime(dr["fix_time"]);
                    _defectInReport.Picture_link = (string)dr["picture_link"];
                    _defectInReport.Fix_status = Convert.ToInt32(dr["fix_status"]);
                    _defectInReport.Description = (string)dr["description"];
                    _defectInReport.Defect_name = (string)dr["name"];
                    _defectInReport.Defect_type_name = (string)dr["type_name"];
                    defectsInReportList.Add(_defectInReport);
                }

                return defectsInReportList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        // delete defect in report
        public int DeleteDefectInReport(int report_num, int defect_num)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            String cStr = BuildDeleteCommand(report_num, defect_num);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        private String BuildDeleteCommand(int report_num, int defect_num)
        {
            String command;
            command = "DELETE FROM defect_in_report WHERE report_num=" + report_num + "and defect_num=" + defect_num;
            return command;
        }

        //update defect in report- all fields- date, time, fix status, description
        public int UpdateDefectInReport(defect_in_report defectInReport)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            String cStr = BuildupdateCommand(defectInReport);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }
        private String BuildupdateCommand(defect_in_report defectInReport)
        {
            String command;
            command = "UPDATE defect_in_report SET fix_date='" + defectInReport.Fix_date.ToString("yyyy-MM-dd") + "', fix_time='" + defectInReport.Fix_time.ToString("HH:ss") + "', fix_status=" + defectInReport.Fix_status + " , description='" + defectInReport.Description + "' WHERE defect_num =" + defectInReport.Defect_num + " and report_num=" + defectInReport.Report_num;

            return command;
        }

        //update defect in report- only image link
        public int UpdateDefectInReportImg(defect_in_report defectInReport, string picture_link)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            String cStr = BuildupdateImgCommand(defectInReport, picture_link);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }
        private String BuildupdateImgCommand(defect_in_report defectInReport, string picture_link)
        {
            String command;
            command = "UPDATE defect_in_report SET picture_link='" + picture_link + "' WHERE defect_num =" + defectInReport.Defect_num + " and report_num=" + defectInReport.Report_num;

            return command;
        }

        public int UpdateDefectInReportStatus(string picture_link, int fix_status)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            String cStr = BuildupdateStatusCommand(picture_link, fix_status);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        private string BuildupdateStatusCommand(string picture_link, int fix_status)
        {
            String command;
            command = "UPDATE defect_in_report SET fix_status=" + fix_status + " WHERE picture_link ='" + picture_link +"'";

            return command;
        }

        //update report
        public int UpdateReport(report r)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildUpdateReportCommand(r);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }
        private String BuildUpdateReportCommand(report r)
        {
            String command;
            command = "UPDATE report SET date='" + r.Date.ToString("yyyy-MM-dd") + "', time='" + r.Time.ToShortTimeString() + "', comment='" + r.Comment + "' WHERE report_num =" + r.Report_num;

            return command;
        }

        //read all defect type
        public List<defect_type> ReadDefectType()
        {
            SqlConnection con = null;
            List<defect_type> defectTypeList = new List<defect_type>();

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "";

                selectSTR = "select * from defect_type";



                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    defect_type dt = new defect_type();

                    dt.Defect_type_num = Convert.ToInt32(dr["defect_type_num"]);
                    dt.Type_name = (string)dr["type_name"];

                    defectTypeList.Add(dt);
                }
                return defectTypeList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        public List<project_type> ReadProjectTypes()
        {

            SqlConnection con = null;
            List<project_type> ptlist = new List<project_type>();

            try
            {
                con = connect("DBConnectionString");

                String selectSTR = "SELECT * FROM [project_type]";
                SqlCommand cmd = new SqlCommand(selectSTR, con);


                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    project_type pt = new project_type();
                    pt.Project_type_num = Convert.ToInt32(dr["project_type_num"]);
                    pt.Project_type_name = (string)dr["project_type_name"];
                    ptlist.Add(pt);
                }

                return ptlist;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }
        //insert a whole new project type- with weights
        public void InsertProjectType(project_type pt)
        {

        }
        // return all users
        public List<user> ReadUsers()
        {
            SqlConnection con = null;
            List<user> uList = new List<user>();

            try
            {
                con = connect("DBConnectionString");

                String selectSTR = "select u.*, ut.type_name from [user] u inner join [user_type] ut on u.user_type_num = ut.user_type_num";
                SqlCommand cmd = new SqlCommand(selectSTR, con);


                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    user u = new user();
                    u.Email = (string)dr["email"];
                    u.Name = (string)dr["name"];
                    u.Phone = (string)dr["phone"];
                    u.Password = (string)dr["password"];
                    u.User_type_num = Convert.ToInt32(dr["user_type_num"]);
                    u.Type_name = (string)dr["type_name"];
                    u.Delete_status = Convert.ToInt32(dr["delete_status"]);
                    uList.Add(u);
                }

                return uList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        //insert new defect in report
        public int InsertDefectInReport(defect_in_report _defect_in_report)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildInsertCommand(_defect_in_report);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        private String BuildInsertCommand(defect_in_report _defect_in_report)
        {
            String command;

            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("Values('{0}', '{1}', '{2}','{3}', '{4}','{5}','{6}')", _defect_in_report.Report_num, _defect_in_report.Defect_num, _defect_in_report.Fix_date.ToString("yyyy-MM-dd"), _defect_in_report.Fix_time.ToShortTimeString(), _defect_in_report.Picture_link, _defect_in_report.Fix_status, _defect_in_report.Description); 
            String prefix = "INSERT INTO defect_in_report " + "(report_num,defect_num,fix_date,fix_time,picture_link,fix_status,description)";
            command = prefix + sb.ToString();

            return command;

        }


        //returns all user types
        public List<user_type> ReadUserTypes()
        {
                SqlConnection con = null;
                List<user_type> userTypeList = new List<user_type>();

                try
                {
                    con = connect("DBConnectionString");

                    String selectSTR = "SELECT * FROM user_type";

                    SqlCommand cmd = new SqlCommand(selectSTR, con);

                    // get a reader
                    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (dr.Read())
                    {

                    user_type _user_type = new user_type();
                    _user_type.User_type_num = Convert.ToInt32(dr["user_type_num"]);
                    _user_type.Type_name = (string)dr["type_name"];
                    userTypeList.Add(_user_type);
                    }

                    return userTypeList;
                }
                catch (Exception ex)
                {
                    // write to log
                    throw (ex);
                }
                finally
                {
                    if (con != null)
                    {
                        con.Close();
                    }

                }

            }
        //insert a new user type
        public int InsertUserType(user_type _user_type)
        {

                SqlConnection con;
                SqlCommand cmd;

                try
                {
                    con = connect("DBConnectionString");
                }
                catch (Exception ex)
                {
                    throw (ex);
                }

                String cStr = BuildInsertCommand(_user_type);

                cmd = CreateCommand(cStr, con);

                try
                {
                    int numEffected = cmd.ExecuteNonQuery();
                    return numEffected;
                }
                catch (Exception ex)
                {
                    throw (ex);
                }

                finally
                {
                    if (con != null)
                    {
                        // close the db connection
                        con.Close();
                    }
                }

        }
        private String BuildInsertCommand(user_type _user_type)
        {
            String command;

            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("Values('{0}')", _user_type.Type_name);
            String prefix = "INSERT INTO user_type " + "(type_name)";
            command = prefix + sb.ToString();

            return command;
        }

        //get last report
        public report ReadLastReport(int proj_num)
        {
            SqlConnection con = null;
            report lastreport = new report();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select top 1* from report where project_num=" + proj_num + " order by report_num desc";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                // Read till the end of the data into a row
                while (dr.Read())
                {
                    lastreport.Report_num = Convert.ToInt32(dr["report_num"]);
                    lastreport.Date = Convert.ToDateTime(dr["date"]);
                    lastreport.Time = Convert.ToDateTime(dr["time"]);
                    lastreport.Comment = (string)dr["comment"];
                    lastreport.Grade = Convert.ToDouble(dr["grade"]);
                    lastreport.Project_num = Convert.ToInt32(dr["project_num"]);
                    lastreport.User_mail = (string)dr["user_email"];
                }

                return lastreport;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        //get all project type weights
        public List<proj_type_weight> ReadProj_type_weight(int proj_type)
        {
            SqlConnection con = null;
            List<proj_type_weight> ptwList = new List<proj_type_weight>();

            try
            {
                con = connect("DBConnectionString");

                String selectSTR = "select* from weight_type_defects where project_type_num=" + proj_type;
                SqlCommand cmd = new SqlCommand(selectSTR, con);


                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    proj_type_weight ptw = new proj_type_weight();
                    ptw.Project_type_num = Convert.ToInt32(dr["project_type_num"]);
                    ptw.Defect_type_num = Convert.ToInt32(dr["defect_type_num"]);
                    ptw.Weight = Convert.ToDouble(dr["weight"]);

                    ptwList.Add(ptw);
                }

                return ptwList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }
        //get all last report defects
        public List<defect_in_report> readLastReportDefects(int proj_num, DateTime date)
        {
            SqlConnection con = null;
            List<defect_in_report> lastDinR = new List<defect_in_report>();

            try
            {
                con = connect("DBConnectionString");

                String selectSTR = "select di.*,d.name,dt.type_name,dt.defect_type_num from defect_in_report di inner join defect d on di.defect_num=d.defect_num inner join defect_type dt on d.defect_type_num=dt.defect_type_num where di.report_num=(select top 1 report_num from report where project_num=" + proj_num + " and date <'" + date.ToString("yyyy-MM-dd") + "' order by report_num desc)";

                SqlCommand cmd = new SqlCommand(selectSTR, con);


                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    defect_in_report _defectInReport = new defect_in_report();

                    _defectInReport.Report_num = Convert.ToInt32(dr["report_num"]);
                    _defectInReport.Defect_num = Convert.ToInt32(dr["defect_num"]);
                    _defectInReport.Fix_date = Convert.ToDateTime(dr["fix_date"]);
                    _defectInReport.Fix_time = Convert.ToDateTime(dr["fix_time"]);
                    _defectInReport.Picture_link = (string)dr["picture_link"];
                    _defectInReport.Fix_status = Convert.ToInt32(dr["fix_status"]);
                    _defectInReport.Description = (string)dr["description"];
                    _defectInReport.Defect_name = (string)dr["name"];
                    _defectInReport.Defect_type_name = (string)dr["type_name"];
                    lastDinR.Add(_defectInReport);
                }

                return lastDinR;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        //read alerts user email
        public List<alert> ReadAlerts(string user_email)
        {
            SqlConnection con = null;
            List<alert> alertsList = new List<alert>();

            try
            {
                con = connect("DBConnectionString");

                String selectSTR = "SELECT a.*,p.name FROM alert a left join project p on a.proj_num=p.project_num WHERE a.user_email='" + user_email + "'and a.status=0 order by [date] desc";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {

                    alert _alert = new alert();
                    _alert.Alert_num = Convert.ToInt32(dr["alert_num"]);
                    _alert.Content = (string)dr["content"];
                    _alert.Date = Convert.ToDateTime(dr["date"]);
                    _alert.Alert_type_num = Convert.ToInt32(dr["alert_type_num"]);
                    _alert.User_email = (string)dr["user_email"];
                    _alert.Status = Convert.ToInt32(dr["status"]);
                    _alert.Proj_num = Convert.ToInt32(dr["proj_num"]);
                    _alert.Proj_name = (string)dr["name"];
                    alertsList.Add(_alert);
                }

                return alertsList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }
        //read alerts by project number
        public List<alert> ReadAlerts(int proj_num)
        {
            SqlConnection con = null;
            List<alert> alertsList = new List<alert>();

            try
            {
                con = connect("DBConnectionString");

                String selectSTR = "SELECT a.*,p.name FROM alert a left join project p on a.proj_num=p.project_num WHERE a.proj_num='" + proj_num + "'and a.status=0 order by [date] desc";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {

                    alert _alert = new alert();
                    _alert.Alert_num = Convert.ToInt32(dr["alert_num"]);
                    _alert.Content = (string)dr["content"];
                    _alert.Date = Convert.ToDateTime(dr["date"]);
                    _alert.Alert_type_num = Convert.ToInt32(dr["alert_type_num"]);
                    _alert.User_email = (string)dr["user_email"];
                    _alert.Status = Convert.ToInt32(dr["status"]);
                    _alert.Proj_num = Convert.ToInt32(dr["proj_num"]);
                    _alert.Proj_name = (string)dr["name"];
                    alertsList.Add(_alert);
                }

                return alertsList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }
        //עדכון ציון דוח 
        public int UpdateReportGrade(int report_num, double grade)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception)
            {
                throw new Exception("The connection to sever is not good");
            }

            String cStr = BuildUpdateReportCommand(report_num, grade);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        private String BuildUpdateReportCommand(int report_num, double grade)
        {
            String command;
            command = "UPDATE report SET grade=" + grade + " WHERE report_num =" + report_num;

            return command;
        }

        //update alert
        public int UpdateAlert(alert a)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildUpdateAlertCommand(a);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception)
            {
                throw new Exception("The update of status alert failed");
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        private String BuildUpdateAlertCommand(alert a)
        {
            String command;
            command = "UPDATE alert SET content='" + a.Content + "', alert_type_num=" + a.Alert_type_num + ", date='" + a.Date.ToString("yyyy-MM-dd HH:mm") + "', User_email='" + a.User_email + "', status=" + a.Status + ", proj_num=" + a.Proj_num + " Where alert_num=" + a.Alert_num;

            return command;
        }


        public int InsertAlert(alert a)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildInsertAlertCommand(a);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw new Exception("The update of status alert failed");
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        private String BuildInsertAlertCommand(alert a)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", a.Content, a.Alert_type_num, a.User_email, a.Status, a.Date.ToString("yyyy-MM-dd HH:mm:ss"), a.Proj_num);
            String prefix = "INSERT INTO alert " + "(content, alert_type_num,user_email,status,date,proj_num) ";
            command = prefix + sb.ToString();

            return command;
        }

        //read Alert Archive by user_email
        public List<alert> ReadAlertArchive(string user_email)
        {
            SqlConnection con = null;
            List<alert> alertsArchiveList = new List<alert>();

            try
            {
                con = connect("DBConnectionString");

                String selectSTR = "SELECT a.*, at.type_name From alert a inner join alert_type at on a.alert_type_num = at.alert_type_num WHERE user_email='" + user_email + "'and status=1 order by [date] desc";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {

                    alert _alert = new alert();
                    _alert.Alert_num = Convert.ToInt32(dr["alert_num"]);
                    _alert.Content = (string)dr["content"];
                    _alert.Date = Convert.ToDateTime(dr["date"]);
                    _alert.Alert_type_num = Convert.ToInt32(dr["alert_type_num"]);
                    _alert.User_email = (string)dr["user_email"];
                    _alert.Status = Convert.ToInt32(dr["status"]);
                    _alert.Proj_num = Convert.ToInt32(dr["proj_num"]);
                    _alert.Type_name = (string)dr["type_name"];
                    alertsArchiveList.Add(_alert);
                }

                return alertsArchiveList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }
        //return all certificates
        public List<certificate> ReadCertificate()
        {
            SqlConnection con = null;
            List<certificate> certificateList = new List<certificate>();

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "";

                selectSTR = "SELECT * FROM certificate c inner join certificate_type ct on c.certificate_type_num = ct.certificate_type_num";



                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {
                    certificate c = new certificate();

                    c.Certificate_num = Convert.ToInt32(dr["certificate_num"]);
                    c.Company = (string)dr["company"];
                    c.Date = Convert.ToDateTime(dr["date"]);
                    c.Address = (string)dr["address"];
                    c.Description = (string)dr["description"];
                    c.Certificate_status = Convert.ToInt32(dr["certificate_status"]);
                    c.Pay_status = Convert.ToInt32(dr["pay_status"]);
                    c.Invoice_num = (string)dr["invoice_num"];
                    c.Certificate_type_num = Convert.ToInt32(dr["certificate_type_num"]);
                    c.User_email = (string)dr["user_email"];
                    c.Contact_id = (string)dr["contact_id"];
                    c.Type_name = (string)dr["type_name"];
                    c.Price = Convert.ToDouble(dr["price"]);
                    c.Expiration = Convert.ToInt32(dr["expiration"]);
                    c.Delete_status = Convert.ToInt32(dr["delete_status"]);

                    certificateList.Add(c);


                }

                return certificateList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        //returns all expired certificates
        public List<certificate> ReadExpiredCertificates(DateTime date)
        {
            SqlConnection con = null;
            List<certificate> certificateList = new List<certificate>();

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "";

                selectSTR = "SELECT c.*, ct.type_name, ct.price,ct.expiration FROM [certificate] c inner join certificate_type ct on c.certificate_type_num = ct.certificate_type_num WHERE DATEADD(YY, ct.expiration, c.date) >= '"+date.ToString("yyyy-MM-dd") +"' AND DATEADD(YY, ct.expiration, c.date) <= DATEADD(MM, 1, '"+ date.ToString("yyyy-MM-dd")+ "');";

                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {
                    certificate c = new certificate();

                    c.Certificate_num = Convert.ToInt32(dr["certificate_num"]);
                    c.Company = (string)dr["company"];
                    c.Date = Convert.ToDateTime(dr["date"]);
                    c.Address = (string)dr["address"];
                    c.Description = (string)dr["description"];
                    c.Certificate_status = Convert.ToInt32(dr["certificate_status"]);
                    c.Pay_status = Convert.ToInt32(dr["pay_status"]);
                    c.Invoice_num = (string)dr["invoice_num"];
                    c.Certificate_type_num = Convert.ToInt32(dr["certificate_type_num"]);
                    c.User_email = (string)dr["user_email"];
                    c.Contact_id = (string)dr["contact_id"];
                    c.Type_name = (string)dr["type_name"];
                    c.Price = Convert.ToDouble(dr["price"]);
                    c.Expiration = Convert.ToInt32(dr["expiration"]);
                    c.Delete_status = Convert.ToInt32(dr["delete_status"]);

                    certificateList.Add(c);


                }

                return certificateList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }
        
        //return all certificate_types
        public List<certificate_type> ReadCertificate_type()
        {
            SqlConnection con = null;
            List<certificate_type> certificateTypeList = new List<certificate_type>();

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "";

                selectSTR = "select * from certificate_type";



                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {
                    certificate_type c = new certificate_type();

                    c.Certificate_type_num = Convert.ToInt32(dr["certificate_type_num"]);
                    c.Type_name = (string)dr["type_name"];
                    c.Price = Convert.ToDouble(dr["price"]);
                    c.Expiration = Convert.ToInt32(dr["expiration"]);


                    certificateTypeList.Add(c);


                }

                return certificateTypeList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }
        //read all orders
        public List<order> ReadOrder()
        {
            SqlConnection con = null;
            List<order> ordersList = new List<order>();

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "";

                selectSTR = "select * from [order]";



                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {
                    order o = new order();

                    o.Order_num = Convert.ToInt32(dr["order_num"]);
                    o.Invoice_num = (string)dr["invoice_num"];
                    o.Date = Convert.ToDateTime(dr["date"]);
                    o.Contact_id = (string)dr["contact_id"];
                    o.Total_price = Convert.ToDouble(dr["total_price"]);
                    o.Delete_status = Convert.ToInt32(dr["delete_status"]);

                    ordersList.Add(o);

                }

                return ordersList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }
        //insert a new order
        public int InsertOrder(order _order)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildInsertCommand(_order);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
        private String BuildInsertCommand(order _order)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}','{3}','{4}')", _order.Invoice_num, _order.Date.ToString("yyyy-MM-dd"), _order.Total_price, _order.Contact_id, _order.Delete_status);
            String prefix = "INSERT INTO [order] " + "(invoice_num,date,total_price,contact_id,delete_status)";
            command = prefix + sb.ToString();

            return command;

        }
        //insert a new certificate
        public int InsertCertificate(certificate _certificate)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildInsertCommand(_certificate);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
        private String BuildInsertCommand(certificate _certificate)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')", _certificate.Certificate_status, _certificate.Pay_status, _certificate.Company, _certificate.Address, _certificate.Date.ToString("yyyy-MM-dd"), _certificate.Description , _certificate.User_email, _certificate.Invoice_num, _certificate.Certificate_type_num, _certificate.Contact_id,_certificate.Delete_status);
            String prefix = "INSERT INTO certificate " + "(certificate_status,pay_status,company,address,date,description,user_email,invoice_num,certificate_type_num, contact_id,delete_status)";
            command = prefix + sb.ToString();

            return command;

        }

        //return all instructions 
        public List<instruction> ReadInstruction()
        {
            SqlConnection con = null;
            List<instruction> instructionList = new List<instruction>();

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "";

                selectSTR = "SELECT i.*, it.type_name,it.expiration FROM instruction i left join instruction_type it on i.instruction_type_num = it.instruction_type_num ";



                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {
                    instruction i = new instruction();

                    i.Instruction_num = Convert.ToInt32(dr["instruction_num"]);
                    i.Company = (string)dr["company"];
                    i.Date = Convert.ToDateTime(dr["date"]);
                    i.Time = Convert.ToDateTime(dr["time"]);
                    i.Address = (string)dr["address"];
                    i.Participants_num = Convert.ToInt32(dr["participants_num"]);
                    i.Pay_status = Convert.ToInt32(dr["pay_status"]);
                    i.Total_price = Convert.ToInt32(dr["total_price"]);
                    i.Invoice_num = Convert.ToInt32(dr["invoice_num"]);
                    i.Instructor_email = (string)dr["instructor_email"];
                    i.Instruction_type_num = Convert.ToInt32(dr["instruction_type_num"]);
                    i.Type_name = (string)dr["type_name"];
                    i.Expiration = Convert.ToInt32(dr["expiration"]);
                    i.Delete_status = Convert.ToInt32(dr["delete_status"]);


                    instructionList.Add(i);


                }

                return instructionList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        //read about to expire instructions
        public List<instruction> ReadExpiredInstruction(DateTime date)
        {
            SqlConnection con = null;
            List<instruction> instructionList = new List<instruction>();

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "";

                selectSTR = "SELECT i.*, it.type_name, it.expiration FROM instruction i inner join instruction_type it on i.instruction_type_num = it.instruction_type_num where DATEADD(YY, it.expiration, i.date) >= '"+date.ToString("yyyy-MM-dd") + "' AND DATEADD(YY, it.expiration, i.date) <= DATEADD(MM, 1, '"+date.ToString("yyyy-MM-dd") + "');";

                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {
                    instruction i = new instruction();

                    i.Instruction_num = Convert.ToInt32(dr["instruction_num"]);
                    i.Company = (string)dr["company"];
                    i.Date = Convert.ToDateTime(dr["date"]);
                    i.Time = Convert.ToDateTime(dr["time"]);
                    i.Address = (string)dr["address"];
                    i.Participants_num = Convert.ToInt32(dr["participants_num"]);
                    i.Pay_status = Convert.ToInt32(dr["pay_status"]);
                    i.Total_price = Convert.ToInt32(dr["total_price"]);
                    i.Invoice_num = Convert.ToInt32(dr["invoice_num"]);
                    i.Instructor_email = (string)dr["instructor_email"];
                    i.Instruction_type_num = Convert.ToInt32(dr["instruction_type_num"]);
                    i.Type_name = (string)dr["type_name"];
                    i.Expiration = Convert.ToInt32(dr["expiration"]);
                    i.Delete_status = Convert.ToInt32(dr["delete_status"]);


                    instructionList.Add(i);


                }

                return instructionList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        // change delete status of certificate
        public int DeleteCertificate(int certificate_num, int delete_status)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            String cStr = BuildDeleteCertificateCommand(certificate_num, delete_status);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }
        private String BuildDeleteCertificateCommand(int certificate_num, int delete_status)
        {
            String command;
            command = "UPDATE certificate SET delete_status = " + delete_status + " WHERE certificate_num = " + certificate_num;
            return command;
        }

        //update certificate detail
        public int UpdateCertificate(certificate c)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildupdateCommand(c);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        private String BuildupdateCommand(certificate c)
        {
            String command;
            command = "UPDATE certificate SET certificate_status=" + c.Certificate_status + ", invoice_num='" +c.Invoice_num+ "', pay_status=" + c.Pay_status + ", address='" + c.Address + "', date='" + c.Date.ToString("yyyy-MM-dd") +  "', company='" + c.Company + "', description='" + c.Description + "' WHERE certificate_num =" + c.Certificate_num;

            return command;
        }

        //return all instruction_types
        public List<instruction_type> ReadInstruction_type()
        {
            SqlConnection con = null;
            List<instruction_type> instructionTypeList = new List<instruction_type>();

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "";

                selectSTR = "select * from instruction_type";



                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {
                    instruction_type i = new instruction_type();

                    i.Instruction_type_num = Convert.ToInt32(dr["instruction_type_num"]);
                    i.Type_name = (string)dr["type_name"];
                    i.Price = Convert.ToDouble(dr["price"]);
                    i.Expiration = Convert.ToInt32(dr["expiration"]);


                    instructionTypeList.Add(i);


                }

                return instructionTypeList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }
        // change delete status of instruction
        public int DeleteInstruction(int instruction_num, int delete_status)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            String cStr = BuildDeleteInstructionCommand(instruction_num, delete_status);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }
        private String BuildDeleteInstructionCommand(int instruction_num, int delete_status)
        {
            String command;
            command = "UPDATE instruction SET delete_status = " + delete_status + " WHERE instruction_num = " + instruction_num;
            return command;
        }

        //update instruction detail
        public int UpdateInstruction(instruction i)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildupdateCommand(i);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        private String BuildupdateCommand(instruction i)
        {
            String command;
            command = "UPDATE instruction SET pay_status=" + i.Pay_status + ",invoice_num="+ i.Invoice_num+ ",instruction_type_num = " + i.Instruction_type_num + ",total_price =" + i.Total_price + ",participants_num=" + i.Participants_num + ",address='" + i.Address + "', date='" + i.Date.ToString("yyyy-MM-dd") + "', company='" + i.Company + "', time='" + i.Time.ToString("HH:mm") + "' WHERE instruction_num =" + i.Instruction_num;

            return command;
        }

        //read report from home page
        public List<report> ReadReportFromHome()
        {
            SqlConnection con = null;
            List<report> reportList = new List<report>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT r.*,p.name FROM report r left join project p on r.project_num=p.project_num WHERE date>= DATEADD(day,-7, GETDATE())";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    report _report = new report();
                    _report.Report_num = Convert.ToInt32(dr["report_num"]);
                    _report.Date = Convert.ToDateTime(dr["date"]);
                    _report.Time = Convert.ToDateTime(dr["time"]);
                    _report.Comment = (string)dr["comment"];
                    _report.Grade = Convert.ToDouble(dr["grade"]);
                    _report.Project_num = Convert.ToInt32(dr["project_num"]);
                    _report.User_mail = (string)dr["user_email"];
                    _report.Proj_name= (string)dr["name"];
                    reportList.Add(_report);
                }

                return reportList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        //read alert from home page
        public List<alert> ReadAlertFromHome(string user_email, int alert_type_num)
        {
            SqlConnection con = null;
            List<alert> alertsList = new List<alert>();

            try
            {
                con = connect("DBConnectionString");

                String selectSTR = "SELECT a.*,p.name FROM alert a left join project p on a.proj_num=p.project_num WHERE user_email='" + user_email + "' and a.alert_type_num=" + alert_type_num + " and date <= getdate() and date >= dateadd(day, -3, getdate()) and a.status=0";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {

                    alert _alert = new alert();
                    _alert.Alert_num = Convert.ToInt32(dr["alert_num"]);
                    _alert.Content = (string)dr["content"];
                    _alert.Date = Convert.ToDateTime(dr["date"]);
                    _alert.Alert_type_num = Convert.ToInt32(dr["alert_type_num"]);
                    _alert.User_email = (string)dr["user_email"];
                    _alert.Status = Convert.ToInt32(dr["status"]);
                    _alert.Proj_num = Convert.ToInt32(dr["proj_num"]);
                    _alert.Proj_name = (string)dr["name"];
                    alertsList.Add(_alert);
                }

                return alertsList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }
        //insert a new instruction
        public int InsertInstruction(instruction _instruction)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildInsertCommand(_instruction);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
        private String BuildInsertCommand(instruction _instruction)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')", _instruction.Instructor_email, _instruction.Invoice_num, _instruction.Total_price, _instruction.Pay_status, _instruction.Participants_num, _instruction.Company, _instruction.Date.ToString("yyyy-MM-dd"), _instruction.Address, _instruction.Delete_status, _instruction.Instruction_type_num, _instruction.Time.ToString("HH:mm"));
            String prefix = "INSERT INTO instruction " + "(instructor_email,invoice_num,total_price,pay_status,participants_num,company,date,address,delete_status,instruction_type_num,time)";
            command = prefix + sb.ToString();

            return command;

        }
        //insert a new contact
        public int InsertContact(contact _contact)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildInsertCommand(_contact);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
        private String BuildInsertCommand(contact _contact)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}','{3}')", _contact.Id, _contact.Mail, _contact.Full_name, _contact.Phone );
            String prefix = "INSERT INTO contact " + "(id,mail,full_name,phone)";
            command = prefix + sb.ToString();

            return command;

        }

        //insert a new contact in instruction
        public int InsertContactInInstruction(contact_in_instruction _contact_in_instruction)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildInsertCommand(_contact_in_instruction);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
        private String BuildInsertCommand(contact_in_instruction _contact_in_instruction)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}')", _contact_in_instruction.Contact_id, _contact_in_instruction.Instruction_num);
            String prefix = "INSERT INTO contact_in_instruction " + "(contact_id,instruction_num)";
            command = prefix + sb.ToString();

            return command;

        }
        //check user by email and password
        public user checkUserLogIn(string email, string password)
        {
            SqlConnection con = null;
            user u = new user();

            try
            {
                con = connect("DBConnectionString");

                String selectSTR = "select* from [user] where email='"+email+"' and password='"+password+"'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
               
                while (dr.Read())
                {

                    u.Email = (string)dr["email"];
                    u.Name= (string)dr["name"];
                    u.Phone= (string)dr["phone"];
                    u.Password= (string)dr["password"];
                    u.User_type_num = Convert.ToInt32(dr["user_type_num"]);


                }

                return u;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }
        //read contacts by number of instruction
        public List<contact> ReadContact(int inst_num)
        {
            SqlConnection con = null;
            List<contact> contactList = new List<contact>();

            try
            {
                con = connect("DBConnectionString");

                String selectSTR = "SELECT c.*, ci.* From contact c inner join contact_in_instruction ci on c.id = ci.contact_id where ci.instruction_num=" + inst_num;

                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {

                    contact _contact = new contact();
                    _contact.Id = Convert.ToInt32(dr["id"]);
                    _contact.Full_name = (string)dr["full_name"];
                    _contact.Phone = (string)dr["phone"];
                    if (_contact.Phone.Length!=10)
                    { var numberBeforeChange = _contact.Phone;
                        _contact.Phone = "0" + numberBeforeChange;
                    }
                    _contact.Mail = (string)dr["mail"];
                    _contact.Instruction_num = Convert.ToInt32(dr["instruction_num"]);
                    contactList.Add(_contact);
                }

                return contactList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }
        //read all contacts 
        public List<contact> ReadContact()
        {
            SqlConnection con = null;
            List<contact> contactList = new List<contact>();

            try
            {
                con = connect("DBConnectionString");

                String selectSTR = "SELECT * FROM contact";

                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {

                    contact _contact = new contact();
                    _contact.Id = Convert.ToInt32(dr["id"]);
                    _contact.Full_name = (string)dr["full_name"];
                    _contact.Phone = (string)dr["phone"];
                    if (_contact.Phone.Length != 10)
                    {
                        var numberBeforeChange2 = _contact.Phone;
                        _contact.Phone = "0" + numberBeforeChange2;
                    }
                    _contact.Mail = (string)dr["mail"];
                    contactList.Add(_contact);
                }

                return contactList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }
        //delete contact in instruction
        public int DeleteContactInInstruction(string contact_id, int instruction_num)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            String cStr = BuildDeleteCommand(contact_id, instruction_num);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }
        private String BuildDeleteCommand(string contact_id, int instruction_num)
        {
            String command;
            command = "delete from contact_in_instruction where contact_id=" + contact_id + "and instruction_num="+ instruction_num;
            return command;
        }

        // update contact details
        public int UpdateContact(contact c)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildupdateCommand(c);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        private String BuildupdateCommand(contact c)
        {
            String command;
            command = "UPDATE contact SET id=" + c.Id + ",full_name = '" + c.Full_name + "',phone =" + c.Phone + ",mail='" + c.Mail + "' WHERE id=" + c.Id;

            return command;
        }

        // change delete status of order
        public int DeleteOrder(int order_num, int delete_status)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            String cStr = BuildDeleteOrderCommand(order_num, delete_status);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }
        private String BuildDeleteOrderCommand(int order_num, int delete_status)
        {
            String command;
            command = "UPDATE [order] SET delete_status = " + delete_status + " WHERE order_num = " + order_num;
            return command;
        }

        //read all items
        public List<item> ReadItem()
        {
            SqlConnection con = null;
            List<item> itemList = new List<item>();

            try
            {
                con = connect("DBConnectionString");

                String selectSTR = "SELECT * FROM item";

                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {

                    item _item = new item();
                    _item.Item_num = Convert.ToInt32(dr["item_num"]);
                    _item.Name = (string)dr["name"];
                    _item.Price = Convert.ToDouble(dr["price"]);
                    itemList.Add(_item);
                }

                return itemList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }
        public List<project> ReadProjectsType2(string userEmail)
        {
            SqlConnection con = null;
            List<project> projectList = new List<project>();

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "";

                selectSTR = "select* from project where manager_email= '"+ userEmail+"' or foreman_email='"+ userEmail+"'";



                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {
                    project p = new project();

                    p.Project_num = Convert.ToInt32(dr["project_num"]);
                    p.Name = (string)dr["name"];
                    p.Company = (string)dr["company"];
                    p.Address = (string)dr["address"];
                    p.Start_date = Convert.ToDateTime(dr["start_date"]);
                    p.End_date = Convert.ToDateTime(dr["end_date"]);
                    p.Status = Convert.ToInt32(dr["status"]);
                    p.Description = (string)dr["description"];
                    p.Safety_lvl = Convert.ToDouble(dr["safety_lvl"]);
                    p.Project_type_num = Convert.ToInt32(dr["project_type_num"]);
                    p.Manager_email = (string)dr["manager_email"];
                    p.Foreman_email = (string)dr["foreman_email"];
                    p.Delete_status= Convert.ToInt32(dr["delete_status"]);

                    projectList.Add(p);


                }

                return projectList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        //insert a new defect type
        public int InsertDefectType(defect_type _defect_type)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildInsertCommand(_defect_type);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
        private String BuildInsertCommand(defect_type _defect_type)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}')", _defect_type.Type_name);
            String prefix = "INSERT INTO defect_type " + "(type_name)";
            command = prefix + sb.ToString();

            return command;

        }

        //insert a new item in order
        public int InsertItemInOrder(items_in_order _items_in_order)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildInsertCommand(_items_in_order);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
        private String BuildInsertCommand(items_in_order _items_in_order)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}','{1}','{2}')", _items_in_order.Item_num, _items_in_order.Order_num, _items_in_order.Quantity);
            String prefix = "INSERT INTO items_in_order " + "(item_num,order_num,quantity)";
            command = prefix + sb.ToString();

            return command;

        }

        //read items in order by number of order
        public List<items_in_order> ReadItemsInOrder(int order_num)
        {
            SqlConnection con = null;
            List<items_in_order> itemInOrdertList = new List<items_in_order>();

            try
            {
                con = connect("DBConnectionString");

                String selectSTR = "SELECT i.*, io.order_num, io.quantity FROM item i inner join items_in_order io on i.item_num = io.item_num where io.order_num = " + order_num;

                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {

                    items_in_order _items_in_order = new items_in_order();
                    _items_in_order.Item_num = Convert.ToInt32(dr["item_num"]);
                    _items_in_order.Name1 = (string)dr["name"];
                    _items_in_order.Price = Convert.ToDouble(dr["price"]);
                    _items_in_order.Order_num = Convert.ToInt32(dr["order_num"]);
                    _items_in_order.Quantity = Convert.ToInt32(dr["quantity"]);
                    itemInOrdertList.Add(_items_in_order);
                }

                return itemInOrdertList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        //update order detail
        public int UpdateOrder(order o)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildupdateCommand(o);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        private String BuildupdateCommand(order o)
        {
            String command;
            command = "UPDATE [order] SET total_price=" + o.Total_price + ", invoice_num='" + o.Invoice_num + "', date='" + o.Date.ToString("yyyy-MM-dd") + "' WHERE order_num =" + o.Order_num;

            return command;
        }

        //update Defect Type 
        public int UpdateDefectType(defect_type _defect_type)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildupdateCommand(_defect_type);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        private String BuildupdateCommand(defect_type _defect_type)
        {
            String command;
            command = "UPDATE [defect_type] SET type_name='" + _defect_type.Type_name + "' WHERE defect_type_num =" + _defect_type.Defect_type_num;

            return command;
        }

        //delete defect_type 
        public int DeleteDefectType(int Defect_type_num)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            String cStr = BuildDeleteCommandDefect(Defect_type_num);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }
        private String BuildDeleteCommandDefect(int Defect_type_num)
        {
            String command;
            command = "delete from defect_type where defect_type_num=" + Defect_type_num;
            return command;
        }
        //insert a new instruction type
        public int InsertInstructionType(instruction_type _instruction_type)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildInsertCommand(_instruction_type);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
        private String BuildInsertCommand(instruction_type _instruction_type)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}')", _instruction_type.Type_name, _instruction_type.Expiration, _instruction_type.Price);
            String prefix = "INSERT INTO instruction_type " + "(type_name,expiration,price)";
            command = prefix + sb.ToString();

            return command;

        }

        //update Instruction Type 
        public int UpdateInstructionType(instruction_type _instruction_type)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildupdateCommand(_instruction_type);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        private String BuildupdateCommand(instruction_type _instruction_type)
        {
            String command;
            command = "UPDATE [instruction_type] SET type_name='" + _instruction_type.Type_name + "', expiration='" + _instruction_type.Expiration + "', price='" + _instruction_type.Price + "' WHERE instruction_type_num =" + _instruction_type.Instruction_type_num;

            return command;
        }

        //delete Instruction type 
        public int DeleteInstructionType(int Instruction_type_num)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            String cStr = BuildDeleteCommandInstruction(Instruction_type_num);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }
        private String BuildDeleteCommandInstruction(int Instruction_type_num)
        {
            String command;
            command = "delete from instruction_type where instruction_type_num=" + Instruction_type_num;
            return command;
        }

        //update User Type 
        public int UpdateUserType(user_type _user_type)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildupdateCommand(_user_type);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        private String BuildupdateCommand(user_type _user_type)
        {
            String command;
            command = "UPDATE [user_type] SET type_name='" + _user_type.Type_name + "' WHERE user_type_num =" + _user_type.User_type_num;

            return command;
        }

        //delete user_type 
        public int DeleteUserType(int User_type_num)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            String cStr = BuildDeleteCommandUser(User_type_num);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }
        private String BuildDeleteCommandUser(int User_type_num)
        {
            String command;
            command = "delete from user_type where user_type_num=" + User_type_num;
            return command;
        }

        //update item in order
        public int UpdateItemInOrder(items_in_order _items_in_order)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception)
            {
                throw new Exception("The connection to sever is not good");
            }

            String cStr = BuildUpdateItemInOrderCommand(_items_in_order);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        private String BuildUpdateItemInOrderCommand(items_in_order _items_in_order)
        {
            String command;
            command = "UPDATE items_in_order SET quantity=" + _items_in_order.Quantity + " WHERE item_num =" + _items_in_order.Item_num + "and order_num=" + _items_in_order.Order_num;

            return command;
        }

        //insert a new Certificate type
        public int InsertCertificateType(certificate_type _certificate_type)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildInsertCommand(_certificate_type);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
        private String BuildInsertCommand(certificate_type _certificate_type)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}')", _certificate_type.Type_name, _certificate_type.Expiration, _certificate_type.Price);
            String prefix = "INSERT INTO certificate_type " + "(type_name,expiration,price)";
            command = prefix + sb.ToString();

            return command;

        }

        //update Certificate Type 
        public int UpdateCertificateType(certificate_type _certificate_type)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildupdateCommand(_certificate_type);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        private String BuildupdateCommand(certificate_type _certificate_type)
        {
            String command;
            command = "UPDATE [certificate_type] SET type_name='" + _certificate_type.Type_name + "', expiration='" + _certificate_type.Expiration + "', price='" + _certificate_type.Price + "' WHERE certificate_type_num =" + _certificate_type.Certificate_type_num;

            return command;
        }

        //delete Certificate type 
        public int DeleteCertificateType(int Certificate_type_num)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            String cStr = BuildDeleteCommandCertificate(Certificate_type_num);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }
        private String BuildDeleteCommandCertificate(int Certificate_type_num)
        {
            String command;
            command = "delete from certificate_type where certificate_type_num=" + Certificate_type_num;
            return command;
        }


        //insert a new Item type
        public int InsertItem(item _item)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildInsertCommand(_item);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
        private String BuildInsertCommand(item _item)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}')", _item.Name, _item.Price);
            String prefix = "INSERT INTO item " + "(name,price)";
            command = prefix + sb.ToString();

            return command;

        }

        //update Item  
        public int UpdateItem(item _item)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildupdateCommand(_item);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        private String BuildupdateCommand(item _item)
        {
            String command;
            command = "UPDATE [item] SET name='" + _item.Name + "', price='" + _item.Price + "' WHERE item_num =" + _item.Item_num;

            return command;
        }

        //delete Item  
        public int DeleteItem(int Item_num)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            String cStr = BuildDeleteCommandItem(Item_num);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }
        private String BuildDeleteCommandItem(int Item_num)
        {
            String command;
            command = "delete from item where item_num=" + Item_num;
            return command;
        }

        //delete item in order
        public int DeleteItemInOrder(int item_num, int order_num)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            String cStr = BuildDeleteItemInOrderCommand(item_num, order_num);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }
        private String BuildDeleteItemInOrderCommand(int item_num,int order_num)
        {
            String command;
            command = "DELETE FROM items_in_order where item_num=" + item_num + "and order_num="+ order_num;
            return command;
        }

        //update User 
        public int UpdateUser(user _user)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildupdateCommand(_user);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        private String BuildupdateCommand(user _user)
        {
            String command;
            command = "UPDATE [user] SET name='" + _user.Name + "', phone='" + _user.Phone + "', password='" + _user.Password + "', user_type_num=" + _user.User_type_num+ ", delete_status=" + _user.Delete_status + " WHERE email ='" + _user.Email+"'";

            return command;
        }

        //delete user
        public int DeleteUser(user _user)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            String cStr = BuildDeleteCommandUser(_user);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }
        private String BuildDeleteCommandUser(user _user )
        {
            String command;
            command = "UPDATE [user] set delete_status ="+_user.Delete_status+" WHERE email ='" + _user.Email+"'";
            return command;
        }

        //update Defect 
        public int UpdateDefect(defect _defect)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildupdateCommand(_defect);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        private String BuildupdateCommand(defect _defect)
        {
            String command;
            command = "UPDATE [defect] SET name = '" + _defect.Name + "', grade = '" + _defect.Grade + "', defect_type_num = '" + _defect.Defect_type_num + "' WHERE defect_num =" + _defect.Defect_num;

            return command;
        }

        //delete Defect 
        public int DeleteDefect(int Defect_num)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            String cStr = BuildDeleteCommandDefectt(Defect_num);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }
        private String BuildDeleteCommandDefectt(int Defect_num)
        {
            String command;
            command = "delete from defect where defect_num=" + Defect_num;
            return command;
        }

    }

}
