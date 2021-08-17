using final_proj_gulkosafety.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_proj_gulkosafety.Models
{
    public class contact
    {
        int id;
        string full_name;
        string phone;
        string mail;
        int instruction_num;


        public int Id { get => id; set => id = value; }
        public string Full_name { get => full_name; set => full_name = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Mail { get => mail; set => mail = value; }
        public int Instruction_num { get => instruction_num; set => instruction_num = value; }

        public contact(int id, string full_name, string phone, string mail)
        {
            Id = id;
            Full_name = full_name;
            Phone = phone;
            Mail = mail;
            Instruction_num = instruction_num;
        }
        public contact(){ }

        public void InsertContact()
        {
            DBServices dbs = new DBServices();
            dbs.InsertContact(this);
        }
        public List<contact> Read(int instruction_num)
        {
            DBServices dbs = new DBServices();
            List<contact> contactList = dbs.ReadContact(instruction_num);
            return contactList;
        }

        public List<contact> Read()
        {
            DBServices dbs = new DBServices();
            List<contact> contactList = dbs.ReadContact();
            return contactList;
        }
        public void UpdateContact()
        {
            DBServices dbs = new DBServices();
            dbs.UpdateContact(this);

        }
    }
}