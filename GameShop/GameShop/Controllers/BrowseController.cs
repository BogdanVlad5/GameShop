using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameShop.Models;

namespace GameShop.Controllers
{
    public class BrowseController : Controller
    {
        private GameDBContext db = new GameDBContext();
        //
        // GET: /ShoppingCart/

        public ActionResult Index()
        {
            ViewBag.listProducts = db.Games.ToList();
            return View();
        }

        public ActionResult ExportJson()
        {
            ViewBag.listProducts = db.Games.ToList();
            Iexporter f = ExporterFactory.GetFactory(ExporterFactory.ExporterType.Json);
            f.createFile(db.Games.ToList());
            return View("Index");
        }

        public ActionResult ExportCSV()
        {
            ViewBag.listProducts = db.Games.ToList();
            Iexporter f = ExporterFactory.GetFactory(ExporterFactory.ExporterType.CSV);
            f.createFile(db.Games.ToList());
            return View("Index");
        }

    }
}
