using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            string user_uid = Memes.GetUID();

            if (user_uid != null)
            {

                Memes.GetMemes("", user_uid, "All");

                return View(Memes);
            }
            else
            {
                Response.Redirect("/Login.aspx");
                return View(Memes);
            }
        }

        [Route("Profile/{uid}")]
        public new ActionResult Profile(string uid)
        {
            var Memes = new MemeModel();
            Memes.GetUserDetails(uid);
            string user_uid = Memes.GetUID();

            if (Memes.GetUID() == null)
            {
                Response.Redirect("/Login.aspx");
                return View(Memes);
            }
            else
            {
                Memes.GetUserDetails(uid);
                Memes.GetMemes(uid, user_uid, "Profile");

                return View(Memes);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            //ViewBag.Something = "Microsoft bekar";
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

            return View(Memes);
        }

        public ActionResult LikedMemes()
        {
            var Memes = new MemeModel();

            Memes.GetMemes("", Memes.GetUID(), "LikedMemes");

            return View(Memes);
        }

        public string Like(string val, string job)
        {
            var Memes = new MemeModel();
            string uid = Memes.GetUID();
            string likedString = Memes.LikeMeme(job, val, uid);
            Debug.WriteLine("Like called " + val);
            return likedString;
        }



        public ActionResult Logout()
        {
            ViewBag.Message = "Your logout";

            var Memes = new MemeModel();

            HttpCookie cookie = new HttpCookie("meme_cookie");
            cookie.Value = null;
            cookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(cookie);

            Response.Redirect("/Login.aspx");
            return View(Memes);
        }


    }
}