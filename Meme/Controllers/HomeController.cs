using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Meme.Models;
using Startup.Models;
namespace Meme.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var Memes = new MemeModel();
            var Start = new StartupModel();
            Start.CreateTables();

            string uid = Memes.GetUID();

            if (uid != null)
            {

                Memes.GetMemes();

                return View(Memes);
            }
            else
            {
                Response.Redirect("/Login.aspx");
                return View(Memes);
            }
        }

        public new ActionResult Profile(string uid)
        {
            var Memes = new MemeModel();
            if (Memes.GetUID() == null)
            {
                Response.Redirect("/Login.aspx");
                return View(Memes);
            }
            else
            {
                Memes.GetUserDetails(uid);
                Memes.GetMemes(uid);

                return View(Memes);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            ViewBag.Something = "Microsoft bekar";
            var Memes = new MemeModel();


            return View(Memes);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            var Memes = new MemeModel();


            return View(Memes);
        }

        public ActionResult Upload()
        {
            ViewBag.Message = "Your upload page";

            var Memes = new MemeModel();

            return RedirectToAction("../Upload.aspx");
        }

    }
}