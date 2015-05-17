using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameShop.Models;

namespace GameShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private GameDBContext db = new GameDBContext();

        public ActionResult Index()
        {
            List<Item> cart = (List<Item>)Session["cart"];
            if (cart == null) return View();
            else return View("Cart");
        }

        private int existsP(int id)
        {
            List<Item> cart = (List<Item>) Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Pr.ID == id) return i;
            return -1;
        }

        public ActionResult Delete(int id)
        {
            int index = existsP(id);
            List<Item> cart = (List<Item>)Session["cart"];
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return View("Cart");
        }


        public ActionResult OrderNow(int id)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item(db.Games.Find(id), 1));
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>) Session["cart"];
                int index = existsP(id);
                if (index == -1)
                    cart.Add(new Item(db.Games.Find(id), 1));
                else cart[index].Quantity++;
                Session["cart"] = cart;
            }

            return View("Cart");
        }

    }
}
