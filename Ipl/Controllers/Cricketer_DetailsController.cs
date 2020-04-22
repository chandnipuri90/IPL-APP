using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ipl.Models;
using PagedList;
using PagedList.Mvc;

namespace Ipl.Controllers
{
    public class Cricketer_DetailsController : Controller
    {
        private incedoEntities db = new incedoEntities();

        // GET: Cricketer_Details
        //public ActionResult Index(int? page)
        //{
        //    var cricketer_Details = db.Cricketer_Details;
        //    return View(cricketer_Details.ToList().ToPagedList(page ?? 1, 3));
        //}





        public ActionResult Index(string SearchBy, string search, int? page)
        {

            if (SearchBy == "Team")
            {
                var model = db.Cricketer_Details.Where(a => a.Team == search || search == null).ToList().ToPagedList(page ?? 1, 3);
                return View(model);
            }
            else if (SearchBy == "ODI_Runs")
            {
                var model = db.Cricketer_Details.Where(a => a.ODI_Runs.ToString() == search || search == null).ToList().ToPagedList(page ?? 1, 3);
                return View(model);
            }
            else if (SearchBy == "Test_Runs")
            {
                var model = db.Cricketer_Details.Where(a => a.Test_Runs.ToString() == search || search == null).ToList().ToPagedList(page ?? 1, 3);
                return View(model);
            }
            else if (SearchBy == "Wickets")
            {
                var model = db.Cricketer_Details.Where(a => a.Wickets.ToString() == search || search == null).ToList().ToPagedList(page ?? 1, 3);
                return View(model);
            }
            else
            {
                var model = db.Cricketer_Details.Where(a => a.Team.StartsWith(search) || search == null).ToList().ToPagedList(page ?? 1, 3);
                return View(model);
            }

        }

        // GET: Cricketer_Details/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cricketer_Details cricketer_Details = await db.Cricketer_Details.FindAsync(id);
            if (cricketer_Details == null)
            {
                return HttpNotFound();
            }
            return View(cricketer_Details);
        }

        // GET: Cricketer_Details/Create
        public ActionResult Create()
        {
            ViewBag.Cricketer_ID = new SelectList(db.Cricketers, "ID", "Name");
            return View();
        }

        // POST: Cricketer_Details/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Details_ID,Cricketer_ID,Team,ODI_Runs,Test_Runs,Wickets")] Cricketer_Details cricketer_Details)
        {
            if (ModelState.IsValid)
            {
                db.Cricketer_Details.Add(cricketer_Details);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Cricketer_ID = new SelectList(db.Cricketers, "ID", "Name", cricketer_Details.Cricketer_ID);
            return View(cricketer_Details);
        }

        // GET: Cricketer_Details/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cricketer_Details cricketer_Details = await db.Cricketer_Details.FindAsync(id);
            if (cricketer_Details == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cricketer_ID = new SelectList(db.Cricketers, "ID", "Name", cricketer_Details.Cricketer_ID);
            return View(cricketer_Details);
        }

        // POST: Cricketer_Details/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Details_ID,Cricketer_ID,Team,ODI_Runs,Test_Runs,Wickets")] Cricketer_Details cricketer_Details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cricketer_Details).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Cricketer_ID = new SelectList(db.Cricketers, "ID", "Name", cricketer_Details.Cricketer_ID);
            return View(cricketer_Details);
        }

        // GET: Cricketer_Details/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cricketer_Details cricketer_Details = await db.Cricketer_Details.FindAsync(id);
            if (cricketer_Details == null)
            {
                return HttpNotFound();
            }
            return View(cricketer_Details);
        }

        // POST: Cricketer_Details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Cricketer_Details cricketer_Details = await db.Cricketer_Details.FindAsync(id);
            db.Cricketer_Details.Remove(cricketer_Details);
            await db.SaveChangesAsync();
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
