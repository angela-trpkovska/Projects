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

    //kontroler za rabota so datumite za polaganje na prv prakticen del i teoretski del
    public class DatesController : Controller
    {
        private AmsmContext db = new AmsmContext();


    //gi pirkazuva podatocite od bazata vo tabela
         [HttpGet]
        [Authorize(Roles = "Admin")]
        // GET: /AmsmDates/
        public ActionResult Index(int? page, string searchString)
        {
           var candidates = from c in db.Informations select c;

            candidates = candidates.OrderByDescending(c => c.date);


            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(candidates.ToPagedList(pageNumber, pageSize));

            //return View(candidates.ToList());
        }


         //gi prikazuva podatocite od bazata filtrirani spored datum dokolku e vnesen

        [HttpPost]
        [Authorize(Roles = "Admin")]
        // GET: /TeoretskiDel/
         public ActionResult Index(string searchString, int? page)
        {
           var candidates = from c in db.Informations
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



        //Vnesuvanje na datum 

        [Authorize(Roles = "Admin")]
        // GET: /AmsmDates/Insert
        public ActionResult Insert()
        {
            return View();
        }



          [Authorize(Roles = "Admin")]
        // POST: /AmsmDates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Insert([Bind(Include = "date,hour")] AmsmInfo amsminfo)
        {
            if (ModelState.IsValid)
            {
                amsminfo.candidatesTP = 0;
                amsminfo.candidatesPP1 = 0;
               


                db.Informations.Add(amsminfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(amsminfo);
         
           

             }
        




          [Authorize(Roles = "Admin")]
        // GET: /AmsmDates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmsmInfo amsminfo = db.Informations.Find(id);
            if (amsminfo == null)
            {
                return HttpNotFound();
            }
            return View(amsminfo);
        }


          [Authorize(Roles = "Admin")]
        // GET: /AmsmDates/Create
    public ActionResult Create()
        {
            return View();
        }



          [Authorize(Roles = "Admin")]
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,date,hour,candidatesTP,candidatesPP1,candidatesPP2")] AmsmInfo amsminfo)
        {
            if (ModelState.IsValid)
            {
                db.Informations.Add(amsminfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(amsminfo);
        }
    

        // GET: /AmsmDates/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmsmInfo amsminfo = db.Informations.Find(id);
            if (amsminfo == null)
            {
                return HttpNotFound();
            }
            return View(amsminfo);
        }

        [Authorize(Roles = "Admin")]
        // POST: /AmsmDates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,date,hour,candidatesTP,candidatesPP1,candidatesPP2")] AmsmInfo amsminfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(amsminfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(amsminfo);
        }

        [Authorize(Roles = "Admin")]
        // GET: /AmsmDates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmsmInfo amsminfo = db.Informations.Find(id);
            if (amsminfo == null)
            {
                return HttpNotFound();
            }
            return View(amsminfo);
        }

        [Authorize(Roles = "Admin")]
        // POST: /AmsmDates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AmsmInfo amsminfo = db.Informations.Find(id);
            db.Informations.Remove(amsminfo);
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
