
using final_proj_gulkosafety.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_proj_gulkosafety.Models
{
    public class item
    {
        int item_num;
        string name;
        double price;

        public int Item_num { get => item_num; set => item_num = value; }
        public string Name { get => name; set => name = value; }
        public double Price { get => price; set => price = value; }

        public item(int item_num, string name, double price)
        {
            Item_num = item_num;
            Name = name;
            Price = price;
        }

        public item() { }

        public List<item> ReadItem()
        {
            DBServices dbs = new DBServices();
            return dbs.ReadItem();
        }
        public void InsertItem()
        {
            DBServices dbs = new DBServices();
            dbs.InsertItem(this);
        }
        public void UpdateItem()
        {
            DBServices dbs = new DBServices();
            dbs.UpdateItem(this);

        }
        public void DeleteItem(int Item_num)
        {
            DBServices dbs = new DBServices();
            dbs.DeleteItem(Item_num);

        }

    }
}
