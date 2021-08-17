using final_proj_gulkosafety.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_proj_gulkosafety.Models
{
    public class defect
    {
        int defect_num;
        string name;
        double grade;
        int defect_type_num;


        public int Defect_num { get => defect_num; set => defect_num = value; }
        public string Name { get => name; set => name = value; }
        public double Grade { get => grade; set => grade = value; }
        public int Defect_type_num { get => defect_type_num; set => defect_type_num = value; }

        public defect(int defect_num, string name, double grade, int defect_type_num)
        {
            Defect_num = defect_num;
            Name = name;
            Grade = grade;
            Defect_type_num = defect_type_num;
        }

        public defect() { }
 
        public void InsertDefect()
        {
            DBServices dbs = new DBServices();
            dbs.InsertDefect(this);
        }

        public List<defect> ReadDefect()
        {
            DBServices dbs = new DBServices();
            List<defect> defectList = dbs.ReadDefect();
            return defectList;
        }
        public void UpdateDefect()
        {
            DBServices dbs = new DBServices();
            dbs.UpdateDefect(this);

        }
        public void DeleteDefect(int Defect_num)
        {
            DBServices dbs = new DBServices();
            dbs.DeleteDefect(Defect_num);

        }
    }
}