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

    //kontroler za rabota so datumite za polaganje na vtor prakticen del
    public class Dates2Controller : Controller
    {
        private AmsmContext db = new AmsmContext();


        //gi pirkazuva podatocite od bazata vo tabela
        [HttpGet]
        [Authorize(Roles = "Admin")]
        // GET: /AmsmDates2/
        public ActionResult Index(int? page, string searchString)
        {
            
            
            var candidates = from c in db.AmsmInfoPP2 select c;

            candidates = candidates.OrderByDescending(c => c.date);

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(candidates.ToPagedList(pageNumber, pageSize));

           // return View(candidates.ToList());
        }


        //gi prikazuva podatocite od bazata filtrirani spored datum dokolku e vnesen

         [HttpPost]
         [Authorize(Roles = "Admin")]
         // GET: /TeoretskiDel/
        public ActionResult Index(string searchString, int? page)
         {
         
           var candidates = from c in db.AmsmInfoPP2
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
        // GET: /AmsmDates2/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmsmInfoPP2 amsminfopp2 = db.AmsmInfoPP2.Find(id);
            if (amsminfopp2 == null)
            {
                return HttpNotFound();
            }
            return View(amsminfopp2);
        }

         // Strana za vnesuvanje na datum za vtor prakticen del

         [Authorize(Roles = "Admin")]
        // GET: /AmsmDates2/Create
        public ActionResult Create()
        {
            return View();
        }



        //Vnesuvanje na datum za vtor prakticen del
         [Authorize(Roles = "Admin")]
        // POST: /AmsmDates2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="date,hour,place")] AmsmInfoPP2 amsminfopp2)
        {
            if (ModelState.IsValid)
            {
                amsminfopp2.candidatesPP2 = 0;
                db.AmsmInfoPP2.Add(amsminfopp2);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(amsminfopp2);
        }



         [Authorize(Roles = "Admin")]
        // GET: /AmsmDates2/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmsmInfoPP2 amsminfopp2 = db.AmsmInfoPP2.Find(id);
            if (amsminfopp2 == null)
            {
                return HttpNotFound();
            }
            return View(amsminfopp2);
        }



         [Authorize(Roles = "Admin")]
        // POST: /AmsmDates2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,date,hour,place,candidatesPP2")] AmsmInfoPP2 amsminfopp2)
        {
            if (ModelState.IsValid)
            {
                db.Entry(amsminfopp2).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(amsminfopp2);
        }



         [Authorize(Roles = "Admin")]
        // GET: /AmsmDates2/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmsmInfoPP2 amsminfopp2 = db.AmsmInfoPP2.Find(id);
            if (amsminfopp2 == null)
            {
                return HttpNotFound();
            }
            return View(amsminfopp2);
        }


         [Authorize(Roles = "Admin")]
        // POST: /AmsmDates2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AmsmInfoPP2 amsminfopp2 = db.AmsmInfoPP2.Find(id);
            db.AmsmInfoPP2.Remove(amsminfopp2);
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
