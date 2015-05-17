using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameShop.Models;

namespace GameShop.Controllers
{
    public class Item
    {
        private Game pr = new Game();
        public int Quantity { get;  set; }

        public Item()
        {
        }

        public Item(Game product, int quantity)
        {
            this.pr = product;
            this.Quantity = quantity;
        }

        public Game Pr
        {
            get { return pr; }
            set { pr = value; }
        }
    }
}