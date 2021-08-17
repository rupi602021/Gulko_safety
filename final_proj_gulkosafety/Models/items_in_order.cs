using final_proj_gulkosafety.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_proj_gulkosafety.Models
{
    public class items_in_order
    {
        int order_num;
        int item_num;
        int quantity;
        string name1;
        double price;

        public int Order_num { get => order_num; set => order_num = value; }
        public int Item_num { get => item_num; set => item_num = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public string Name1 { get => name1; set => name1 = value; }
        public double Price { get => price; set => price = value; }

        public items_in_order(int order_num, int item_num, int quantity, string name1, double price)
        {
            Order_num = order_num;
            Item_num = item_num;
            Quantity = quantity;
            Name1 = name1;
            Price = price;
        }

        public items_in_order() { }

        public void InsertItemInOrder()
        {
            DBServices dbs = new DBServices();
            dbs.InsertItemInOrder(this);
        }
        public List<items_in_order> Read(int order_num)
        {
            DBServices dbs = new DBServices();
            List<items_in_order> itemInOrdertList = dbs.ReadItemsInOrder(order_num);
            return itemInOrdertList;
        }
        public void UpdateItemInOrder()
        {
            DBServices dbs = new DBServices();
            dbs.UpdateItemInOrder(this);

        }
        public void DeleteItemInOrder(int item_num, int order_num)
        {
            DBServices dbs = new DBServices();
            dbs.DeleteItemInOrder(item_num, order_num);

        }

    }
}
