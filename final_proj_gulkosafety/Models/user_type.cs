using final_proj_gulkosafety.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_proj_gulkosafety.Models
{
    public class user_type
    {
        int user_type_num;
        string type_name;

        public user_type(int user_type_num, string type_name)
        {
            User_type_num = user_type_num;
            Type_name = type_name;
        }

        public int User_type_num { get => user_type_num; set => user_type_num = value; }
        public string Type_name { get => type_name; set => type_name = value; }

        public user_type() { }

        public List<user_type> Read()
        {
          
            DBServices dbs = new DBServices();
            return dbs.ReadUserTypes();
        }

        public void Insert()
        {
            DBServices dbs = new DBServices();
            dbs.InsertUserType(this);
        }
        public void UpdateUserType()
        {
            DBServices dbs = new DBServices();
            dbs.UpdateUserType(this);

        }
        public void DeleteUserType(int User_type_num)
        {
            DBServices dbs = new DBServices();
            dbs.DeleteUserType(User_type_num);

        }
    }
}