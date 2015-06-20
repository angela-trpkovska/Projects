using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AmsmProject.Models;
using AmsmProject.DAL;
using PagedList;
using PagedList;

namespace AmsmProject.Controllers
{
    [RequireHttps]
   //kontroler koj sluzi za rabota so kandidati koj zakazuvaat za teoretski del
    public class TeoretskiDelController : Controller
    {
        private AmsmContext db = new AmsmContext();

        //metod koj gi prikazuva kandidatite za teoretski del
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Index(int? page, string searchString)
        {


            var candidates = from c in db.CandidatesTP select c;

            candidates = candidates.OrderByDescending(c => c.date);

          

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(candidates.ToPagedList(pageNumber, pageSize));


           
        
        }

        //prikazi gi kandidatite za teoretski del filtrirani po datum 
        [HttpPost]
        [Authorize(Roles = "Admin")]
        // GET: /TeoretskiDel/
        public ActionResult Index(string searchString, int? page)
        {
          
            var candidates = from c in db.CandidatesTP
                           select c;

            if (!String.IsNullOrEmpty(searchString))
            {

                string day = searchString.Substring(0, 2);
                string month = searchString.Substring(3, 2);
                if (day.StartsWith("0"))
                {
                    day = day.Substring(1, 1);
                }

                if (month.StartsWith("0"))
                {
                    month = month.Substring(1, 1);
                }

                

                string year = searchString.Substring(6, 4);



                candidates = candidates.Where(c => c.date.Day.ToString().Equals(day) && c.date.Month.ToString().Equals(month) && c.date.Year.ToString().Equals(year));
                    
                   
            }
           // return View(candidates.ToList());

            

            candidates = candidates.OrderByDescending(c => c.date);

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(candidates.ToPagedList(pageNumber, pageSize));


          

           
        }

         [Authorize(Roles = "Admin")]
        // GET: /TeoretskiDel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TheoryPart theorypart = db.CandidatesTP.Find(id);
            if (theorypart == null)
            {
                return HttpNotFound();
            }
            return View(theorypart);
        }


        [Authorize(Roles = "Admin")]
        // GET: /TeoretskiDel/Create
        public ActionResult Create()
        {
            return View();
        }


        [Authorize(Roles = "Admin")]
        // POST: /TeoretskiDel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,EMBG,date,hour,passed,payed")] TheoryPart theorypart)
        {
            if (ModelState.IsValid)
            {
                db.CandidatesTP.Add(theorypart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(theorypart);
        }


        [Authorize(Roles = "User")]
        public ActionResult ZakaziTermin()
        {
            return View();
        }


        //metod za zakuzuvanje na termin za teoretski del
        [Authorize(Roles = "User")]
        [HttpPost]
        public ActionResult ZakaziTermin([Bind(Include = "date,hour")] TheoryPart theorypart)
        {
            bool exsist = false;
            var mbr = (string)Session["mbr"];

            // var candidate = db.CandidatesTP.Find(mbr);

            List<TheoryPart> list = db.CandidatesTP.ToList();
            foreach (TheoryPart tp in list)
            {
                if (tp.EMBG.Equals(mbr))
                {

                    exsist = true;
                }

            }



            //ako vekje zakazal testovi da nemoze pak da zakaze
            if (exsist)
            {
                return Redirect("https://localhost:44301/Error/Exsist");

            }


            if (ModelState.IsValid)
            {
                List<AmsmInfo> informacii = db.Informations.ToList();


                foreach (AmsmInfo ainfo in informacii)
                {
                    if (ainfo.date == theorypart.date && ainfo.hour == theorypart.hour)
                    {
                        if (ainfo.candidatesTP < 30)
                        {

                            ViewBag.Message = "Успешно закажавте термин";
                            // Session["TheoryPID"] = theorypart.ID;
                            //var mbr = (string)Session["mbr"];
                            theorypart.EMBG = mbr;
                            theorypart.passed = false;
                            theorypart.payed = false;
                            db.CandidatesTP.Add(theorypart);

                            int num = ainfo.candidatesTP;
                            ainfo.candidatesTP = num + 1;




                            db.SaveChanges();

                            TempData["PartName"] = "TeoretskiDel";
                            TempData["Cena"] = "2600";
                            return Redirect("https://localhost:44301/Payment/Pay");


                        }

                        else
                        {
                            ViewBag.Message = "Местата за  тој термин се пополнети.Ве молиме изберете друг термин";
                            return View(theorypart);


                        }

                    }



                }

                ViewBag.Message = "Ве молиме изберете соодветно";
                return View(theorypart);
            }



            return View(theorypart);




        }



        [Authorize(Roles = "Admin")]
        // GET: /TeoretskiDel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TheoryPart theorypart = db.CandidatesTP.Find(id);
            if (theorypart == null)
            {
                return HttpNotFound();
            }
            return View(theorypart);
        }



        [Authorize(Roles = "Admin")]
        // POST: /TeoretskiDel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,EMBG,date,hour,passed,payed")] TheoryPart theorypart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(theorypart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(theorypart);
        }



        [Authorize(Roles = "Admin")]
        // GET: /TeoretskiDel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TheoryPart theorypart = db.CandidatesTP.Find(id);
            if (theorypart == null)
            {
                return HttpNotFound();
            }
            return View(theorypart);
        }



        [Authorize(Roles = "Admin")]
        // POST: /TeoretskiDel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TheoryPart theorypart = db.CandidatesTP.Find(id);
            db.CandidatesTP.Remove(theorypart);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
