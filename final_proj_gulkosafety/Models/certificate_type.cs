using final_proj_gulkosafety.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_proj_gulkosafety.Models
{
    public class certificate_type
    {
        int certificate_type_num;
        string type_name;
        double price;
        int expiration;

        public certificate_type(int certificate_type_num, string type_name, double price, int expiration)
        {
            Certificate_type_num = certificate_type_num;
            Type_name = type_name;
            Price = price;
            Expiration = expiration;
        }
        public certificate_type() { }

        public int Certificate_type_num { get => certificate_type_num; set => certificate_type_num = value; }
        public string Type_name { get => type_name; set => type_name = value; }
        public double Price { get => price; set => price = value; }
        public int Expiration { get => expiration; set => expiration = value; }

        public List<certificate_type> ReadCertificate_type()
        {
            DBServices dbs = new DBServices();
            return dbs.ReadCertificate_type();
        }
        public void InsertCertificateType()
        {
            DBServices dbs = new DBServices();
            dbs.InsertCertificateType(this);
        }
        public void UpdateCertificateType()
        {
            DBServices dbs = new DBServices();
            dbs.UpdateCertificateType(this);

        }
        public void DeleteCertificateType(int Certificate_type_num)
        {
            DBServices dbs = new DBServices();
            dbs.DeleteCertificateType(Certificate_type_num);

        }
    }
}
