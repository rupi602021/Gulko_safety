using final_proj_gulkosafety.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_proj_gulkosafety.Models
{
    public class order
    {
        int order_num;
        string invoice_num;
        DateTime date;
        double total_price;
        string contact_id;
        int delete_status;

        public int Order_num { get => order_num; set => order_num = value; }
        public string Invoice_num { get => invoice_num; set => invoice_num = value; }
        public DateTime Date { get => date; set => date = value; }
        public double Total_price { get => total_price; set => total_price = value; }
        public string Contact_id { get => contact_id; set => contact_id = value; }
        public int Delete_status { get => delete_status; set => delete_status = value; }

        public order(int order_num, string bill_num, DateTime date, double total_price, string contact_id, int delete_status)
        {
            Order_num = order_num;
            Invoice_num = invoice_num;
            Date = date;
            Total_price = total_price;
            Contact_id = contact_id;
            Delete_status = delete_status;
        }

        public order(){}

        public List<order> ReadOrder()
        {
            DBServices dbs = new DBServices();
            return dbs.ReadOrder();
        }
        public void DeleteOrder(int order_num, int delete_status)
        {
            DBServices dbs = new DBServices();
            dbs.DeleteOrder(order_num, delete_status);

        }
        public void Insert()
        {
            DBServices dbs = new DBServices();
            dbs.InsertOrder(this);
        }

        public void UpdateOrder()
        {
            DBServices dbs = new DBServices();
            dbs.UpdateOrder(this);

        }
    }

}