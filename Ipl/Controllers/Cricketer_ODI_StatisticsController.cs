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
using PagedList;//add namespace for paging
using PagedList.Mvc;//add namespace for paging

namespace Ipl.Controllers
{
    public class Cricketer_ODI_StatisticsController : Controller
    {
        private incedoEntities db = new incedoEntities();

        // GET: Cricketer_ODI_Statistics
        public ActionResult Index(string Search, int? page)
        {

            ViewBag.Cricketer = new SelectList(db.Cricketer_ODI_Statistics, "ODI_ID", "Name");

            
            
                var model = db.Cricketer_ODI_Statistics.Where(a => a.Name == Search || Search == null).ToList().ToPagedList(page ?? 1, 3);
                return View(model);
            
            //if (SearchBy == "Name")
            //{
            //    var model = db.Cricketer_ODI_Statistics.Where(a => a.Name == Search || Search == null).ToList().ToPagedList(page ?? 1, 3);
            //    return View(model);
            //}
            //else
            //{
            //    var model = db.Cricketer_ODI_Statistics.Where(a => a.Name.StartsWith(Search) || Search == null).ToList().ToPagedList(page ?? 1, 3);
            //    return View(model);
            //}
        }



        // GET: Cricketer_ODI_Statistics/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cricketer_ODI_Statistics cricketer_ODI_Statistics = await db.Cricketer_ODI_Statistics.FindAsync(id);
            if (cricketer_ODI_Statistics == null)
            {
                return HttpNotFound();
            }
            return View(cricketer_ODI_Statistics);
        }

        // GET: Cricketer_ODI_Statistics/Create
        public ActionResult Create()
        {
            ViewBag.Cricketer_ID = new SelectList(db.Cricketers, "ID", "Name");
            return View();
        }

        // POST: Cricketer_ODI_Statistics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ODI_ID,Cricketer_ID,Name,Half_Century,Century")] Cricketer_ODI_Statistics cricketer_ODI_Statistics)
        {
            if (ModelState.IsValid)
            {
                db.Cricketer_ODI_Statistics.Add(cricketer_ODI_Statistics);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Cricketer_ID = new SelectList(db.Cricketers, "ID", "Name", cricketer_ODI_Statistics.Cricketer_ID);
            return View(cricketer_ODI_Statistics);
        }

        // GET: Cricketer_ODI_Statistics/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cricketer_ODI_Statistics cricketer_ODI_Statistics = await db.Cricketer_ODI_Statistics.FindAsync(id);
            if (cricketer_ODI_Statistics == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cricketer_ID = new SelectList(db.Cricketers, "ID", "Name", cricketer_ODI_Statistics.Cricketer_ID);
            return View(cricketer_ODI_Statistics);
        }

        // POST: Cricketer_ODI_Statistics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ODI_ID,Cricketer_ID,Name,Half_Century,Century")] Cricketer_ODI_Statistics cricketer_ODI_Statistics)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cricketer_ODI_Statistics).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Cricketer_ID = new SelectList(db.Cricketers, "ID", "Name", cricketer_ODI_Statistics.Cricketer_ID);
            return View(cricketer_ODI_Statistics);
        }

        // GET: Cricketer_ODI_Statistics/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cricketer_ODI_Statistics cricketer_ODI_Statistics = await db.Cricketer_ODI_Statistics.FindAsync(id);
            if (cricketer_ODI_Statistics == null)
            {
                return HttpNotFound();
            }
            return View(cricketer_ODI_Statistics);
        }

        // POST: Cricketer_ODI_Statistics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Cricketer_ODI_Statistics cricketer_ODI_Statistics = await db.Cricketer_ODI_Statistics.FindAsync(id);
            db.Cricketer_ODI_Statistics.Remove(cricketer_ODI_Statistics);
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
