using final_proj_gulkosafety.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_proj_gulkosafety.Models
{
    public class project_type
    {
        int project_type_num;
        string project_type_name;
        List<proj_type_weight> weights;

        public project_type(int project_type_num, string project_type_name, List<proj_type_weight> weights)
        {
            Project_type_num = project_type_num;
            Project_type_name = project_type_name;
            Weights = weights;
        }

        public int Project_type_num { get => project_type_num; set => project_type_num = value; }
        public string Project_type_name { get => project_type_name; set => project_type_name = value; }
        public List<proj_type_weight> Weights { get => weights; set => weights = value; }

        public project_type() { }

        public List<project_type> Read()
        {
            DBServices dbs = new DBServices();
            return dbs.ReadProjectTypes();
        }

        public void Insert()
        {
            DBServices dbs = new DBServices();
            dbs.InsertProjectType(this);
        }
    }
}