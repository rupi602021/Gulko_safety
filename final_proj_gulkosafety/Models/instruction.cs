using final_proj_gulkosafety.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_proj_gulkosafety.Models
{
    public class instruction
    {
        int instruction_num;
        string company;
        DateTime date;
        DateTime time;
        string address;
        int participants_num;
        int pay_status;
        int total_price;
        int invoice_num;
        string instructor_email;
        int instruction_type_num;
        string type_name;
        int expiration;
        int delete_status;

        public instruction(int instruction_num, string company, DateTime date, DateTime time, string address, int participants_num, int pay_status, int total_price, int invoice_num, string instructor_email, int instruction_type_num, string type_name, int expiration, int delete_status)
        {
            Instruction_num = instruction_num;
            Company = company;
            Date = date;
            Time = time;
            Address = address;
            Participants_num = participants_num;
            Pay_status = pay_status;
            Total_price = total_price;
            Invoice_num = invoice_num;
            Instructor_email = instructor_email;
            Instruction_type_num = instruction_type_num;
            Type_name = type_name;
            Expiration= expiration;
            Delete_status = delete_status;
        }
        public instruction() { }

        public int Instruction_num { get => instruction_num; set => instruction_num = value; }
        public string Company { get => company; set => company = value; }
        public DateTime Date { get => date; set => date = value; }
        public DateTime Time { get => time; set => time = value; }
        public string Address { get => address; set => address = value; }
        public int Participants_num { get => participants_num; set => participants_num = value; }
        public int Pay_status { get => pay_status; set => pay_status = value; }
        public int Total_price { get => total_price; set => total_price = value; }
        public int Invoice_num { get => invoice_num; set => invoice_num = value; }
        public string Instructor_email { get => instructor_email; set => instructor_email = value; }
        public int Instruction_type_num { get => instruction_type_num; set => instruction_type_num = value; }
        public string Type_name { get => type_name; set => type_name = value; }
        public int Expiration { get => expiration; set => expiration = value; }
        public int Delete_status { get => delete_status; set => delete_status = value; }

        public List<instruction> ReadInstruction()
        {
            DBServices dbs = new DBServices();
            return dbs.ReadInstruction();
        }
        public List<instruction> ReadExpiredInstruction(DateTime date)
        {
            DBServices dbs = new DBServices();
            return dbs.ReadExpiredInstruction(date);
        }
        public void DeleteInstruction(int instruction_num , int delete_status)
        {
            DBServices dbs = new DBServices();
            dbs.DeleteInstruction(instruction_num, delete_status);

        }
        public void UpdateInstruction()
        {
            DBServices dbs = new DBServices();
            dbs.UpdateInstruction(this);

        }
        public void InsertInstruction()
        {
            DBServices dbs = new DBServices();
            dbs.InsertInstruction(this);
        }
    }
}