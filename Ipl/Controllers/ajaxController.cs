using Ipl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ipl.Controllers
{
    public class ajaxController : Controller
    {
        private incedoEntities db = new incedoEntities();
        // GET: ajax
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AjaxMethod()
        {
            List<SelectListItem> cricketers_ipl = new List<SelectListItem>();
              
            for (int i = 0; i < 10; i++)
            {
                cricketers_ipl.Add(new SelectListItem
                {
                    Value =db.Cricketer_IPL_Statstics. ToList()[i].IPL_Id.ToString(),
                    Text =db.Cricketer_IPL_Statstics.ToList()[i].Team_Name
                });
            }

            return Json(cricketers_ipl);
        }
    }

}
