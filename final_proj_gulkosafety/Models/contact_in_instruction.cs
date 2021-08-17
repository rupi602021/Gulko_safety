using final_proj_gulkosafety.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_proj_gulkosafety.Models
{
    public class contact_in_instruction
    {
        string contact_id;
        int instruction_num;

        public contact_in_instruction(string contact_id, int instruction_num)
        {
            Contact_id = contact_id;
            Instruction_num = instruction_num;
        }

        public contact_in_instruction() {}

        public string Contact_id { get => contact_id; set => contact_id = value; }
        public int Instruction_num { get => instruction_num; set => instruction_num = value; }

        public void InsertContactInInstruction()
        {
            DBServices dbs = new DBServices();
            dbs.InsertContactInInstruction(this);
        }

        public void DeleteContactInInstruction(string contact_id, int instruction_num)
        {
            DBServices dbs = new DBServices();
            dbs.DeleteContactInInstruction(contact_id, instruction_num);

        }
    }
}