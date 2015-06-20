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
    [RequireHttps]
    //kontroler za rabota so kandidati koi zakzauvaat za vtor prakticen del
    public class VtorPrakticenDelController : Controller
    {
        private AmsmContext db = new AmsmContext();


        //metod koj gi prikazuva informaciite  za kandidatite  koi zakzale termin
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Index(int? page, string searchString)
        {

            //return View(db.CandidatesPP2.ToList());
            var candidates = from c in db.CandidatesPP2 select c;

            candidates = candidates.OrderByDescending(c => c.date);

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(candidates.ToPagedList(pageNumber, pageSize));

        }



        //metod koj gi prikazuva informaciite  za kandidatite  koi zakzale termin filtrirani spored dattum
        [HttpPost]
        [Authorize(Roles = "Admin")]

        public ActionResult Index(string searchString, int? page)
        {
            var candidates = from c in db.CandidatesPP2
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
            //return View(candidates.ToList());
           

            candidates = candidates.OrderByDescending(c => c.date);

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(candidates.ToPagedList(pageNumber, pageSize));


        }






       


         [Authorize(Roles = "Admin")]
        // GET: /VtorPrakticenDel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PracticalPart2 practicalpart2 = db.CandidatesPP2.Find(id);
            if (practicalpart2 == null)
            {
                return HttpNotFound();
            }
            return View(practicalpart2);
        }


         [Authorize(Roles = "Admin")]
        // GET: /VtorPrakticenDel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /VtorPrakticenDel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

         [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,EMBG,date,place,hour,passed,payed")] PracticalPart2 practicalpart2)
        {
            if (ModelState.IsValid)
            {
                db.CandidatesPP2.Add(practicalpart2);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(practicalpart2);
        }


         [Authorize(Roles = "User")]
        public ActionResult ZakaziTermin()
        {
            return View();
        }


        //metod za zakazuvanje na termin za vtor prakticen del
         [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ZakaziTermin([Bind(Include = "date,place,hour")] PracticalPart2 practicalpart2)
        {
            bool exsist = false;
            var mbr = (string)Session["mbr"];
            PracticalPart1 ppart = new PracticalPart1();

            // var candidate = db.CandidatesTP.Find(mbr);

            List<PracticalPart1> list = db.CandidatesPP1.ToList();
            foreach (PracticalPart1 pp in list)
            {
                if (pp.EMBG.Equals(mbr))
                {

                    ppart = pp;
                }

            }

            List<PracticalPart2> list2 = db.CandidatesPP2.ToList();
            foreach (PracticalPart2 pp2 in list2)
            {
                if (pp2.EMBG.Equals(mbr))
                {

                    exsist = true;
                }

            }



            //ako vekje zakazal testovi da nemoze pak da zakaze
            if (exsist)
            {
                return Redirect("https://localhost:44301/Error/Exsist");

            }


            //ako nema polozeno poligon nemoze da zakaze gradska
            if (!ppart.passed)
            {
                return Redirect("https://localhost:44301/Error/NoPrivilegies");

            }


            if (ModelState.IsValid)
            {
                //List<AmsmInfo> informacii = db.Informations.ToList();
                List<AmsmInfoPP2> informacii = db.AmsmInfoPP2.ToList();

                foreach (AmsmInfoPP2 ainfo in informacii)
                {
                    if (ainfo.date == practicalpart2.date && ainfo.place == practicalpart2.place && ainfo.hour == practicalpart2.hour)
                    //plus i za mesto i vreme proverka
                    {

                        if (ainfo.candidatesPP2 < 20)
                        {

                            ViewBag.Message = "Успешно закажавте термин";
                            // var mbr = (string)Session["mbr"];
                            practicalpart2.EMBG = mbr;
                            practicalpart2.passed = false;
                            practicalpart2.payed = false;
                            db.CandidatesPP2.Add(practicalpart2);

                            int num = ainfo.candidatesPP2;
                            ainfo.candidatesPP2 = num + 1;

                            db.SaveChanges();

                            TempData["PartName"] = "PracticalPart2";
                            TempData["Cena"] = "3200";
                            return Redirect("https://localhost:44301/Payment/Pay");
                        }
                        else
                        {
                            ViewBag.Message = "Местата за  тој термин се пополнети.Ве молиме изберете друг термин";
                            return View(practicalpart2);


                        }
                    }
                }

                ViewBag.Message = "Ве молиме изберете соодветно";
                return View(practicalpart2);

            }

            return View(practicalpart2);
        }



         [Authorize(Roles = "Admin")]
         // GET: /VtorPrakticenDel/Edit/5
         public ActionResult Edit(int? id)
         {
             if (id == null)
             {
                 return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             }
             PracticalPart2 practicalpart2 = db.CandidatesPP2.Find(id);
             if (practicalpart2 == null)
             {
                 return HttpNotFound();
             }
             return View(practicalpart2);
         }



       



        [Authorize(Roles = "Admin")]
        // POST: /VtorPrakticenDel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,EMBG,date,place,hour,passed,payed")] PracticalPart2 practicalpart2)
        {
            if (ModelState.IsValid)
            {
                db.Entry(practicalpart2).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(practicalpart2);
        }




        [Authorize(Roles = "Admin")]// GET: /VtorPrakticenDel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PracticalPart2 practicalpart2 = db.CandidatesPP2.Find(id);
            if (practicalpart2 == null)
            {
                return HttpNotFound();
            }
            return View(practicalpart2);
        }




        [Authorize(Roles = "Admin")]
        // POST: /VtorPrakticenDel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PracticalPart2 practicalpart2 = db.CandidatesPP2.Find(id);
            db.CandidatesPP2.Remove(practicalpart2);
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
