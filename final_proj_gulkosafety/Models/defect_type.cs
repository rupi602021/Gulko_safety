using final_proj_gulkosafety.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_proj_gulkosafety.Models
{
    public class defect_type
    {
        int defect_type_num;
        string type_name;


        public int Defect_type_num { get => defect_type_num; set => defect_type_num = value; }
        public string Type_name { get => type_name; set => type_name = value; }

        public defect_type(int defect_type_num, string type_name)
        {
            Defect_type_num = defect_type_num;
            Type_name = type_name;
        }
        public defect_type() { }

        public List<defect_type> ReadDefectType()
        {
            DBServices dbs = new DBServices();
            return dbs.ReadDefectType();
        }

        public void InsertDefectType()
        {
            DBServices dbs = new DBServices();
            dbs.InsertDefectType(this);
        }
        
        public void UpdateDefectType()
        {
            DBServices dbs = new DBServices();
            dbs.UpdateDefectType(this);

        }
        public void DeleteDefectType(int Defect_type_num)
        {
            DBServices dbs = new DBServices();
            dbs.DeleteDefectType(Defect_type_num);

        }
    }
}