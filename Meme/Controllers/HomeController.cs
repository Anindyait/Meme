using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Meme.Models;
namespace Meme.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var Memes = new MemeModel();
            ViewBag.IndexMsg = "Hello!";

            Memes.GetMemes();
        
            return View(Memes);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            ViewBag.Something = "Microsoft bekar";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}