using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AmsmProject.Controllers
{
    //kotnroler za prikazuvanje na soodvetni poraki dokolku nastane greska
    public class ErrorController : Controller
    {
        //
        // GET: /Error/
        public ActionResult Index()
        {
            return View();
        }

        //prikazuvanje na stranata za zabranet pristap
        [AllowAnonymous]
        public ActionResult AccessDenied()
        {
            return View();
        }

        //prikazuvanje na stranata za postoenje na toj korisnik
        public ActionResult Exsist()
        {
            return View();
        }


        //  prikazuvanje na stranata deka nema privilegii
        public ActionResult NoPrivilegies()
        {
            return View();
        }
	}
}