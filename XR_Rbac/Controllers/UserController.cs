using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XR_Rbac.Models;

namespace XR_Rbac.Controllers
{
    public class UserController : Controller
    {
        Rbac rb = new Rbac();
        // GET: User
        public ActionResult Index()
        {
            return View(rb.Users);
        }
        public ActionResult Edit(int id)
        {
            var user = rb.Users.FirstOrDefault(m => m.ID == id);
            if (user == null) return Content("编辑项未找到");
            return View(user);
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Save(User user)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { code = 400 });
            }
            rb.Users.AddOrUpdate(user);
            rb.SaveChanges();
            return Json(new { code = 200 });
        }
        public ActionResult delete(int id)
        {
            User user = new User();
            //var module = rb.Modules.FirstOrDefault(m => m.ID == id);
            user.ID = id;
            rb.Users.Attach(user);
            rb.Users.Remove(user);
            rb.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}