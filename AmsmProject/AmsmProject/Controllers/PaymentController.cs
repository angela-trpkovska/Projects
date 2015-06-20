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

namespace AmsmProject.Controllers
{

    [RequireHttps]
    //kotnroler koj sluzi da rabota so bazata  koja sodrzi informacii za kreditnite karitcki
    public class PaymentController : Controller
    {
        private AmsmContext db = new AmsmContext();


        [Authorize(Roles = "Admin")]
        // GET: /Payment/
        public ActionResult Index()
        {
            return View(db.CreditCards.ToList());
        }


        [Authorize(Roles = "Admin")]
        // GET: /Payment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCardInfo creditcardinfo = db.CreditCards.Find(id);
            if (creditcardinfo == null)
            {
                return HttpNotFound();
            }
            return View(creditcardinfo);
        }


          [Authorize(Roles = "User")]
        // GET: /Payment/Create
        public ActionResult Pay()
        {
            return View();
        }



        [Authorize(Roles = "User")]
        // POST: /Payment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Pay([Bind(Include = "firstName,lastName,cardNumber,expiresDate,code")] CreditCardInfo creditcardinfo)
        {
            if (ModelState.IsValid)
            {
                var mbr = (string)Session["mbr"];
                creditcardinfo.EMBG = mbr;
                db.CreditCards.Add(creditcardinfo);

                var text = (string)TempData["PartName"];

                if (text.Equals("TeoretskiDel"))
                { 
                    //TheoryPart tp=db.CandidatesTP.Find()
                    List<TheoryPart> kandidati = db.CandidatesTP.ToList();


                   // var ID = (int)Session["TheoryPID"];
                    //int id = Convert.ToInt32(ID);

                   // TheoryPart tpart = db.CandidatesTP.Find(id);
                    //tpart.payed = true;

                 
                  foreach (TheoryPart tp in kandidati)
                    {
                        if (tp.EMBG.Equals(mbr))
                        {
                            tp.payed = true;
                            db.SaveChanges();
                            ViewBag.Message = "Плаќањето е успешно";
                            return View("Successful");
                        }
                    }
             }

                if (text.Equals("PracticalPart1"))
                {
                    //TheoryPart tp=db.CandidatesTP.Find()
                    List<PracticalPart1> kandidati = db.CandidatesPP1.ToList();
                     foreach (PracticalPart1 pp1 in kandidati)
                    {
                        if (pp1.EMBG.Equals(mbr))
                        {
                            pp1.payed = true;
                            db.SaveChanges();
                            ViewBag.Message = "Плаќањето е успешно";
                            return View("Successful");
                        }

                    }
                   }


                if (text.Equals("PracticalPart2"))
                {
                    //TheoryPart tp=db.CandidatesTP.Find()
                    List<PracticalPart2> candidates = db.CandidatesPP2.ToList();
                     foreach (PracticalPart2 pp2 in candidates)
                    {
                        if (pp2.EMBG.Equals(mbr))
                        {
                            pp2.payed = true;
                            db.SaveChanges();
                            ViewBag.Message = "Плаќањето е успешно";
                            return View("Successful");
                        }

                    }
                   }
            
            }

        
           
            return View(creditcardinfo);
        }



        [Authorize(Roles = "Admin")]
        // GET: /Payment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCardInfo creditcardinfo = db.CreditCards.Find(id);
            if (creditcardinfo == null)
            {
                return HttpNotFound();
            }
            return View(creditcardinfo);
        }

        // POST: /Payment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize(Roles = "Admin")]
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,EMBG,firstName,lastName,cardNumber,expiresDate,code")] CreditCardInfo creditcardinfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(creditcardinfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(creditcardinfo);
        }




        [Authorize(Roles = "Admin")]
        // GET: /Payment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCardInfo creditcardinfo = db.CreditCards.Find(id);
            if (creditcardinfo == null)
            {
                return HttpNotFound();
            }
            return View(creditcardinfo);
        }



        [Authorize(Roles = "Admin")]
        // POST: /Payment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CreditCardInfo creditcardinfo = db.CreditCards.Find(id);
            db.CreditCards.Remove(creditcardinfo);
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
