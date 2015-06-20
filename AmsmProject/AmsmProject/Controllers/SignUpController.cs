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
using System.Data.Entity.Validation;
using PagedList;

namespace AmsmProject.Controllers
{
    [RequireHttps]
    //kontroler za rabota so potencijalnite kandidati za vozacka dozvola
    public class SignUpController : Controller
    {
        private AmsmContext db = new AmsmContext();



        // GET: /SignUp/
        public ActionResult Index()
        {
            return View();
        }


        // Post: /SignUp/

        [HttpPost]
        public ActionResult Index(Candidate c)
        {

            List<Candidate> candidates = db.Candidates.ToList();

            foreach (Candidate cand in candidates)
            {

                if (cand.EMBG == c.EMBG)
                {
                    Session["mbr"] = c.EMBG;

                    return Redirect("https://localhost:44301/Login/Create");
                }
            }
            return View(c);

        }


        //metod za prikazuvanje na lista na kandidati od baza
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult List(int? page, string searchString)
        {

            //return View(db.Candidates.ToList());

            var candidates = from c in db.Candidates select c;

            candidates = candidates.OrderByDescending(c => c.firstName);


            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(candidates.ToPagedList(pageNumber, pageSize));

           

        
        }




        //metod za prikazuvanje na lista na kandidati od baza filtrirani spored maticen borj
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string searchString, int? page)
        {
            var candidates = from cand in db.Candidates
                             select cand;

            if (!String.IsNullOrEmpty(searchString))
            {
              candidates = candidates.Where(cand => cand.EMBG.Equals(searchString));
                                     
            }
            candidates = candidates.OrderByDescending(c => c.firstName);

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(candidates.ToPagedList(pageNumber, pageSize));

           // return View(candidates.ToList());
        }


        // GET: /SignUp/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate candidate = db.Candidates.Find(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

        // GET: /SignUp/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /SignUp/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="EMBG,firstName,parentName,lastName,category,drivingSchool,instructor")] Candidate candidate)
        {
            if (ModelState.IsValid)
            {
                db.Candidates.Add(candidate);
                db.SaveChanges();
                //return RedirectToAction("Index");
            }

            return View(candidate);
        }

        // GET: /SignUp/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate candidate = db.Candidates.Find(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

        // POST: /SignUp/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="EMBG,firstName,parentName,lastName,category,drivingSchool,instructor")] Candidate candidate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(candidate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(candidate);
        }

        // GET: /SignUp/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate candidate = db.Candidates.Find(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

        // POST: /SignUp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Candidate candidate = db.Candidates.Find(id);
            db.Candidates.Remove(candidate);
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
