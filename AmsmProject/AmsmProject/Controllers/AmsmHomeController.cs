using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AmsmProject.Controllers
{
   // [RequireHttps]
    //kontroler za prikazuvanje na HomePage,AdminPage,Avtoskoli 
    public class AmsmHomeController : Controller
    {

        [AllowAnonymous]
        //
        // GET: /AmsmHome/
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "User,Admin")]
        public ActionResult HomePage()
        {
            return View();
        }

         [Authorize(Roles = "Admin")]
        public ActionResult AdminPage()
        {
            return View();
        }

        [AllowAnonymous]
         public ActionResult Avtoshkoli()
         {
             return View();
         }
	}
}