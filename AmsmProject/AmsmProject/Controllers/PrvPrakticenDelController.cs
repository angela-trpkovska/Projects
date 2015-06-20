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

namespace AmsmProject.Controllers
{
   // [RequireHttps]

    //kontroler koj sluzi za rabota so kandidati koj zakazuvaat za prv prakticen del
    public class PrvPrakticenDelController : Controller
    {
        private AmsmContext db = new AmsmContext();


        //prikazi gi kandidatie od baza 
        // GET: /PrvPrakticenDel/
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Index(int? page, string searchString)
        {
           var candidates = from c in db.CandidatesPP1 select c;

            candidates = candidates.OrderByDescending(c => c.date);

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(candidates.ToPagedList(pageNumber, pageSize));

           // return View(candidates.ToList());

        }

        //prikazi gi kandidatie od baza filtrirani spored datum na zakzuvanje
        [HttpPost]
        [Authorize(Roles = "Admin")]
        // GET: /TeoretskiDel/
        public ActionResult Index(string searchString, int? page)
        {

           var candidates = from c in db.CandidatesPP1
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
            candidates = candidates.OrderByDescending(c => c.date);

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(candidates.ToPagedList(pageNumber, pageSize));



        }


        [Authorize(Roles = "Admin")]
        // GET: /PrvPrakticenDel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PracticalPart1 practicalpart1 = db.CandidatesPP1.Find(id);
            if (practicalpart1 == null)
            {
                return HttpNotFound();
            }
            return View(practicalpart1);
        }

         [Authorize(Roles = "Admin")]
        // GET: /PrvPrakticenDel/Create
        public ActionResult Create()
        {
            return View();
        }


         [Authorize(Roles = "Admin")]
        // POST: /PrvPrakticenDel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,EMBG,date,passed,payed")] PracticalPart1 practicalpart1)
        {
            if (ModelState.IsValid)
            {
                db.CandidatesPP1.Add(practicalpart1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(practicalpart1);
        }

        [Authorize(Roles = "User")]
        public ActionResult ZakaziTermin()
        {
            return View();
        }


        //metod za korisnikot da zakaze termin za 1 prakticen del
        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ZakaziTermin([Bind(Include = "date")] PracticalPart1 practicalpart1)
        {
            bool exsist = false;
            var mbr = (string)Session["mbr"];
            TheoryPart tpart = new TheoryPart();

            // var candidate = db.CandidatesTP.Find(mbr);

            List<TheoryPart> list = db.CandidatesTP.ToList();
            foreach (TheoryPart tp in list)
            {
                if (tp.EMBG.Equals(mbr))
                {

                    tpart = tp;
                    
                }

            }



            // var candidate = db.CandidatesTP.Find(mbr);

            List<PracticalPart1> list2 = db.CandidatesPP1.ToList();
            foreach (PracticalPart1 pp1 in list2)
            {
                if (pp1.EMBG.Equals(mbr))
                {

                    exsist = true;
                }

            }



            //ako vekje zakazal testovi da nemoze pak da zakaze
            if (exsist)
            {
                return Redirect("https://localhost:44301/Error/Exsist");

            }




            //ako nema polozeno testovi nemoze da zakaze poligon
            if (!tpart.passed)
            {
                return Redirect("https://localhost:44301/Error/NoPrivilegies");

            }



            if (ModelState.IsValid)
            {
                List<AmsmInfo> informacii = db.Informations.ToList();
                foreach (AmsmInfo ainfo in informacii)
                {
                    if (ainfo.date == practicalpart1.date)
                    {

                        if (ainfo.candidatesTP < 30)
                        {

                            ViewBag.Message = "Успешно закажавте термин";
                            //var mbr = (string)Session["mbr"];
                            practicalpart1.EMBG = mbr;
                            practicalpart1.passed = false;
                            practicalpart1.payed = false;
                            db.CandidatesPP1.Add(practicalpart1);
                            int num = ainfo.candidatesPP1;
                            ainfo.candidatesPP1 = num + 1;




                            db.SaveChanges();

                            TempData["PartName"] = "PracticalPart1";
                            TempData["Cena"] = "3200";
                            return Redirect("https://localhost:44301/Payment/Pay");

                        }
                        else
                        {
                            ViewBag.Message = "Местата за  тој термин се пополнети.Ве молиме изберете друг термин";
                            return View(practicalpart1);


                        }

                    }



                }

                ViewBag.Message = "Ве молиме изберете соодветно";
                return View(practicalpart1);
            }



            return View(practicalpart1);
        }

        [Authorize(Roles = "Admin")]

        // GET: /PrvPrakticenDel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PracticalPart1 practicalpart1 = db.CandidatesPP1.Find(id);
            if (practicalpart1 == null)
            {
                return HttpNotFound();
            }
            return View(practicalpart1);
        }

        // POST: /PrvPrakticenDel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,EMBG,date,passed,payed")] PracticalPart1 practicalpart1)
        {
            if (ModelState.IsValid)
            {
                db.Entry(practicalpart1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(practicalpart1);
        }


        [Authorize(Roles = "Admin")]
        // GET: /PrvPrakticenDel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PracticalPart1 practicalpart1 = db.CandidatesPP1.Find(id);
            if (practicalpart1 == null)
            {
                return HttpNotFound();
            }
            return View(practicalpart1);
        }


        [Authorize(Roles = "Admin")]
        // POST: /PrvPrakticenDel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PracticalPart1 practicalpart1 = db.CandidatesPP1.Find(id);
            db.CandidatesPP1.Remove(practicalpart1);
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
