using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_proj_gulkosafety.Models
{
    public class alert_type
    {
        int alert_type_num;
        string type_name;

        public alert_type(int alert_type_num, string type_name)
        {
            Alert_type_num = alert_type_num;
            Type_name = type_name;
        }

        public alert_type() { }

        public int Alert_type_num { get => alert_type_num; set => alert_type_num = value; }
        public string Type_name { get => type_name; set => type_name = value; }
    }
}