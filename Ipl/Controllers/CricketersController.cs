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
    public class CricketersController : Controller
    {
        private incedoEntities db = new incedoEntities();



        // GET: Cricketers
        [OutputCache(Duration = 10)]
        public ActionResult Index(string SearchBy, string search, int? page, string sort )
        {

           

            var cricketer = db.Cricketers;

            
            
            if (SearchBy == "Name")
            {
              var  model = cricketer.Where(a => a.Name == search || search == null ).ToList().ToPagedList(page??1,3);
                return View(model);
            }
            else if (SearchBy == "ODI")
            {
               var model = cricketer.Where(a => a.ODI.ToString() == search || search == null).ToList().ToPagedList(page ?? 1, 3); ;
                return View(model);
            }
            else if (SearchBy == "Test")
            {
                var model = cricketer.Where(a => a.Test.ToString() == search || search == null).ToList().ToPagedList(page ?? 1, 3); ;
                return View(model);
            }
            else
            {
                var model = db.Cricketers.Where(a => a.Name.StartsWith(search) || search == null).ToList().ToPagedList(page ?? 1, 3); ;
                return View(model);
            }




            ///view bag uses in index view //by using "T" only show time not date if u remove T then show date and time as well

            //var cricketer = db.Cricketers.AsQueryable();
            //cricketer = cricketer.Where(c => c.Name.Contains(SearchBy));

            //switch (sort)
            //{
            //    case "Name":
            //        cricketer = cricketer.OrderByDescending(s => s.Name);
            //        break;
            //    case "ODI":
            //        cricketer = cricketer.OrderByDescending(s => s.ODI);
            //        break;
            //    case "Test":
            //        cricketer = cricketer.OrderByDescending(s => s.Test);
            //        break;
            //    default:
            //        cricketer = cricketer.OrderBy(s => s.Name);
            //        break;
            //}
            //return View(cricketer.ToList().ToPagedList(page ?? 1, 3));
        }


        // GET: Cricketers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cricketer cricketer = await db.Cricketers.FindAsync(id);
            if (cricketer == null)
            {
                return HttpNotFound();
            }
            return View(cricketer);
        }

        // GET: Cricketers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cricketers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,ODI,Test")] Cricketer cricketer)
        {
            if (ModelState.IsValid)
            {
                db.Cricketers.Add(cricketer);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(cricketer);
        }

        // GET: Cricketers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cricketer cricketer = await db.Cricketers.FindAsync(id);
            if (cricketer == null)
            {
                return HttpNotFound();
            }
            return View(cricketer);
        }

        // POST: Cricketers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,ODI,Test")] Cricketer cricketer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cricketer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cricketer);
        }

        // GET: Cricketers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cricketer cricketer = await db.Cricketers.FindAsync(id);
            if (cricketer == null)
            {
                return HttpNotFound();
            }
            return View(cricketer);
        }

        // POST: Cricketers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Cricketer cricketer = await db.Cricketers.FindAsync(id);
            db.Cricketers.Remove(cricketer);
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
