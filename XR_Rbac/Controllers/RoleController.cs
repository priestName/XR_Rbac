using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XR_Rbac.Models;

namespace XR_Rbac.Controllers
{
    public class RoleController : Controller
    {
        Rbac rb = new Rbac();
        // GET: Role
        public ActionResult Index()
        {
            return View(rb.Roles);
        }
        public ActionResult Edit(int id)
        {
            var role = rb.Roles.FirstOrDefault(m => m.ID == id);
            if (role == null) return Content("编辑项未找到");
            return View(role);
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Save(Role role)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { code = 400 });
            }
            rb.Roles.AddOrUpdate(role);
            rb.SaveChanges();
            return Json(new { code = 200 });
        }
        public ActionResult delete(int id)
        {
            Role role = new Role();
            //var module = rb.Modules.FirstOrDefault(m => m.ID == id);
            role.ID = id;
            rb.Roles.Attach(role);
            rb.Roles.Remove(role);
            rb.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}