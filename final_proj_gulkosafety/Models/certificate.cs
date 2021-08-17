using final_proj_gulkosafety.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_proj_gulkosafety.Models
{
    public class certificate
    {
        int certificate_num;
        string company;
        DateTime date;
        string address;
        string description;
        int certificate_status;
        int pay_status;
        string invoice_num;
        int certificate_type_num;
        string user_email;
        string contact_id;
        string type_name;
        double price;
        int expiration;
        int delete_status;

        public certificate(int certificate_num, string company, DateTime date, string address, string description, int certificate_status, int pay_status, string invoice_num, int certificate_type_num, string user_email, string contact_id, string type_name, double price, int expiration, int delete_status)
        {
            Certificate_num = certificate_num;
            Company = company;
            Date = date;
            Address = address;
            Description = description;
            Certificate_status = certificate_status;
            Pay_status = pay_status;
            Invoice_num = invoice_num;
            Certificate_type_num = certificate_type_num;
            User_email = user_email;
            Contact_id = contact_id;
            Type_name = type_name;
            Price = price;
            Expiration = expiration;
            Delete_status = delete_status;
        }
        public certificate() { }

        public int Certificate_num { get => certificate_num; set => certificate_num = value; }
        public string Company { get => company; set => company = value; }
        public DateTime Date { get => date; set => date = value; }
        public string Address { get => address; set => address = value; }
        public string Description { get => description; set => description = value; }
        public int Certificate_status { get => certificate_status; set => certificate_status = value; }
        public int Pay_status { get => pay_status; set => pay_status = value; }
        public string Invoice_num { get => invoice_num; set => invoice_num = value; }
        public int Certificate_type_num { get => certificate_type_num; set => certificate_type_num = value; }
        public string User_email { get => user_email; set => user_email = value; }
        public string Contact_id { get => contact_id; set => contact_id = value; }
        public string Type_name { get => type_name; set => type_name = value; }
        public double Price { get => price; set => price = value; }
        public int Expiration { get => expiration; set => expiration = value; }
        public int Delete_status { get => delete_status; set => delete_status = value; }


        public List<certificate> ReadCertificate()
        {
            DBServices dbs = new DBServices();
            return dbs.ReadCertificate();
        }
        public List<certificate> ReadExpiredCertificates(DateTime date)
        {
            DBServices dbs = new DBServices();
            return dbs.ReadExpiredCertificates(date);
        }
        public void Insert()
        {
            DBServices dbs = new DBServices();
            dbs.InsertCertificate(this);
        }
        public void DeleteCertificate(int certificate_num, int delete_status)
        {
            DBServices dbs = new DBServices();
            dbs.DeleteCertificate(certificate_num, delete_status);

        }
        public void UpdateCertificate()
        {
            DBServices dbs = new DBServices();
            dbs.UpdateCertificate(this);

        }
    }

}