using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using XR_Rbac.Models;
using XR_Rbac.ViewModel;

namespace XR_Rbac.Controllers
{
    public class RoleModuleController : Controller
    {
        Rbac rb = new Rbac();
        // GET: RoleModule
        public ActionResult Index()
        {
            var rolemodule = rb.Roles.Include(r => r.Modules);
            return View(rolemodule);
        }
        public ActionResult Edit(RoleModuleViewModel rolemodule)
        {
            rolemodule.roleNume= rb.Roles.FirstOrDefault(r => r.ID == rolemodule.roleid).Name;
            rolemodule.moduleNume = rb.Modules.FirstOrDefault(r => r.ID == rolemodule.moduleid).Name;

            ViewBag.Moduleoptions = from r in rb.Modules select new SelectListItem { Text = r.Name, Value = r.ID.ToString() };
            return View(rolemodule);
        }
        public ActionResult Create()
        {
            ViewBag.Roleoptions = from r in rb.Roles select new SelectListItem { Text = r.Name, Value = r.ID.ToString() };
            ViewBag.Moduleoptions = from r in rb.Modules select new SelectListItem { Text = r.Name, Value = r.ID.ToString() };
            return View();
        }
        public ActionResult Insert(RoleModuleViewModel rolemodule)
        {
            var role = rb.Roles.FirstOrDefault(r => r.ID == rolemodule.roleid);
            var module = new Module {ID = rolemodule.moduleid};
            rb.Modules.Attach(module);
            role.Modules.Add(module);
            if (rb.SaveChanges()==0)
            {
                return Json(new { code = 400 });
            }
            return Json(new {code=200});
        }

        public ActionResult delete(RoleModuleViewModel rolemodule)
        {
            var role = rb.Roles.FirstOrDefault(r => r.ID == rolemodule.roleid);
            var module=new Module {ID =rolemodule.moduleid};
            rb.Modules.Attach(module);
            role.Modules.Remove(module);
            if (rb.SaveChanges()==0)
            {
                return Json(new { code =400 });
            }
            return Json(new { code = 200 });
        }

        public ActionResult Update(RoleModuleViewModel rolemodule)
        {
            if (rolemodule.moduleid == rolemodule.Upmoduleid)
            {
                return Json(new { code = 400 });
            }
            var role = rb.Roles.FirstOrDefault(r => r.ID == rolemodule.roleid);

            var module = new Module { ID = rolemodule.moduleid};
            rb.Modules.Attach(module);

            var upmoduleid= new Module { ID = rolemodule.Upmoduleid};
            rb.Modules.Attach(upmoduleid);
            role.Modules.Remove(module);
            role.Modules.Add(upmoduleid);
            if (rb.SaveChanges()==0)
            {
                return Json(new { code = 400 });
            }
            return Json(new {code = 200});
        }
    }
}