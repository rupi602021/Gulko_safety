using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_proj_gulkosafety.Models
{
    public class defect_for_algo
    {
        int defect_num;
        string defect_name;
        int defect_type_num;
        string defect_type_name;
        double grade;

        public defect_for_algo(int defect_num, string defect_name, int defect_type_num, string defect_type_name, double grade)
        {
            Defect_num = defect_num;
            Defect_name = defect_name;
            Defect_type_num = defect_type_num;
            Defect_type_name = defect_type_name;
            Grade = grade;
        }

        public int Defect_num { get => defect_num; set => defect_num = value; }
        public string Defect_name { get => defect_name; set => defect_name = value; }
        public int Defect_type_num { get => defect_type_num; set => defect_type_num = value; }
        public string Defect_type_name { get => defect_type_name; set => defect_type_name = value; }
        public double Grade { get => grade; set => grade = value; }

        public defect_for_algo() { }


    }
}