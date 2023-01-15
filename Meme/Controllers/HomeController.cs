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
            HttpCookie cookie = Request.Cookies["meme_cookie"];
            var Memes = new MemeModel();


            if (cookie != null)
            {
                ViewBag.IndexMsg = "Hello!";

                Memes.GetMemes();

                return View(Memes);
            }
            else
            {
                Response.Redirect("/Login.aspx");
                return View(Memes);
            }
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

        public ActionResult Upload()
        {
            ViewBag.Message = "Your upload page";

           
            return View();
        }
    }
}