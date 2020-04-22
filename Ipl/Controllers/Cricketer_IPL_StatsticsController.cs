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
    public class Cricketer_IPL_StatsticsController : Controller
    {
        private incedoEntities db = new incedoEntities();

        // GET: Cricketer_IPL_Statstics
        public ActionResult Index(string SearchBy, string Search, int?page)

        {
            var ipl = db.Cricketer_IPL_Statstics.ToList();
            SelectList list = new SelectList(ipl, "IPL_Id" , "Team_Name");
                
            ViewBag.ipl = list;


            if (SearchBy == "Team_Name")
            {
                var model = db.Cricketer_IPL_Statstics.Where(a => a.Team_Name.ToString() == Search || Search == null).ToList().ToPagedList(page ?? 1, 3);
                return View(model);
            }
            else
            {
                var model = db.Cricketer_IPL_Statstics.Where(a => a.Team_Name.StartsWith(Search) || Search == null).ToList().ToPagedList(page ?? 1, 3);
                return View(model);
            }

        }

        // GET: Cricketer_IPL_Statstics/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cricketer_IPL_Statstics cricketer_IPL_Statstics = await db.Cricketer_IPL_Statstics.FindAsync(id);
            if (cricketer_IPL_Statstics == null)
            {
                return HttpNotFound();
            }
            return View(cricketer_IPL_Statstics);
        }

        // GET: Cricketer_IPL_Statstics/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.Cricketers, "ID", "Name");
            return View();
        }

        // POST: Cricketer_IPL_Statstics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IPL_Id,Team_Name,ID")] Cricketer_IPL_Statstics cricketer_IPL_Statstics)
        {
            if (ModelState.IsValid)
            {
                db.Cricketer_IPL_Statstics.Add(cricketer_IPL_Statstics);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.Cricketers, "ID", "Name", cricketer_IPL_Statstics.ID);
            return View(cricketer_IPL_Statstics);
        }

        // GET: Cricketer_IPL_Statstics/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cricketer_IPL_Statstics cricketer_IPL_Statstics = await db.Cricketer_IPL_Statstics.FindAsync(id);
            if (cricketer_IPL_Statstics == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.Cricketers, "ID", "Name", cricketer_IPL_Statstics.ID);
            return View(cricketer_IPL_Statstics);
        }

        // POST: Cricketer_IPL_Statstics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IPL_Id,Team_Name,ID")] Cricketer_IPL_Statstics cricketer_IPL_Statstics)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cricketer_IPL_Statstics).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.Cricketers, "ID", "Name", cricketer_IPL_Statstics.ID);
            return View(cricketer_IPL_Statstics);
        }

        // GET: Cricketer_IPL_Statstics/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cricketer_IPL_Statstics cricketer_IPL_Statstics = await db.Cricketer_IPL_Statstics.FindAsync(id);
            if (cricketer_IPL_Statstics == null)
            {
                return HttpNotFound();
            }
            return View(cricketer_IPL_Statstics);
        }

        // POST: Cricketer_IPL_Statstics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Cricketer_IPL_Statstics cricketer_IPL_Statstics = await db.Cricketer_IPL_Statstics.FindAsync(id);
            db.Cricketer_IPL_Statstics.Remove(cricketer_IPL_Statstics);
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
