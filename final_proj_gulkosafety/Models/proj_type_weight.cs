using final_proj_gulkosafety.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_proj_gulkosafety.Models
{
    public class proj_type_weight
    {
        int project_type_num;
        int defect_type_num;
        double weight;

       

    

        public proj_type_weight() { }

        public proj_type_weight(int project_type_num, int defect_type_num, double weight)
        {
            Project_type_num = project_type_num;
            Defect_type_num = defect_type_num;
            Weight = weight;
        }

        public int Project_type_num { get => project_type_num; set => project_type_num = value; }
        public int Defect_type_num { get => defect_type_num; set => defect_type_num = value; }
        public double Weight { get => weight; set => weight = value; }

        public List<proj_type_weight> ReadProj_type_weight(int proj_type)
        {
            DBServices dbs = new DBServices();
            List<proj_type_weight> proj_type_weightList = dbs.ReadProj_type_weight(proj_type);
            return proj_type_weightList;
        }
    }
}