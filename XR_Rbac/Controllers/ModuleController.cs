using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XR_Rbac.Models;

namespace XR_Rbac.Controllers
{
    public class ModuleController : Controller
    {
        Rbac rb = new Rbac();
        // GET: Module
        public ActionResult Index()
        {
                //Module module = rb.Modules.FirstOrDefault(m => m.Name == Name && m.Controller==Controller);
                return View(rb.Modules);

        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Save(Module module)
        {
            if (!ModelState.IsValid)
            {
                return Json(new {code = 400});
            }
            rb.Modules.AddOrUpdate(module);
            rb.SaveChanges();
            return Json(new { code = 200 });
        }
        public ActionResult Edit(int id)
        {
            var mod = rb.Modules.FirstOrDefault(m => m.ID == id);
            if (mod == null) return Content("编辑项未找到");

            return View(mod);
        }
        public ActionResult delete(int id)
        {
            Module mod=new Module();
            //var module = rb.Modules.FirstOrDefault(m => m.ID == id);
            mod.ID = id;
            rb.Modules.Attach(mod);
            rb.Modules.Remove(mod);
            rb.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}