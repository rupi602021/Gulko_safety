using final_proj_gulkosafety.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_proj_gulkosafety.Models
{
    public class user
    {
        string email;
        string name;
        string phone;
        string password;
        int user_type_num;
        string type_name;
        int delete_status;

        public user(string email, string name, string phone, string password, int user_type_num, string type_name, int delete_status)
        {
            Email = email;
            Name = name;
            Phone = phone;
            Password = password;
            User_type_num = user_type_num;
            Type_name = type_name;
            Delete_status = delete_status;
        }

        public string Email { get => email; set => email = value; }
        public string Name { get => name; set => name = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Password { get => password; set => password = value; }
        public int User_type_num { get => user_type_num; set => user_type_num = value; }
        public string Type_name { get => type_name; set => type_name = value; }
        public int Delete_status { get => delete_status; set => delete_status = value; }

        public user() { }

        public List<user> Read()
        {
            DBServices dbs = new DBServices();
            return dbs.ReadUsers();
        }

        public void InsertUser()
        {
            DBServices dbs = new DBServices();
            dbs.InsertUser(this);
        }
        public List<user> Read_user_in_project(string manager_email, string foreman_email)
        {
            DBServices dbs = new DBServices();
            List<user> userListInProj = dbs.Read_user_in_project(manager_email, foreman_email);

            return userListInProj;
        }
        public user checkUserLogIn(string email, string password)
        {
             DBServices dbs = new DBServices();
            user u = dbs.checkUserLogIn(email, password);
            return u;
        }
        public void UpdateUser()
        {
            DBServices dbs = new DBServices();
            dbs.UpdateUser(this);

        }
        public void DeleteUser()
        {
            DBServices dbs = new DBServices();
            dbs.DeleteUser(this);

        }
    }
}