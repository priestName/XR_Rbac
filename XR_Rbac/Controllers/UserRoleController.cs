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
    public class UserRoleController : Controller
    {
        Rbac rb = new Rbac();
        // GET: UserRole
        public ActionResult Index()
        {
            var userrole = rb.Users.Include(r => r.Roles);
            return View(userrole);
        }
        public ActionResult Edit(UserRoleViewModel userrole)
        {
            userrole.roleNume = rb.Roles.FirstOrDefault(r => r.ID == userrole.roleid).Name;
            userrole.userNume = rb.Users.FirstOrDefault(r => r.ID == userrole.userid).UserName;

            ViewBag.Roleoptions = from r in rb.Roles select new SelectListItem { Text = r.Name, Value = r.ID.ToString() };
            return View(userrole);
        }
        public ActionResult Create()
        {
            ViewBag.Roleoptions = from r in rb.Roles select new SelectListItem { Text = r.Name, Value = r.ID.ToString() };
            ViewBag.Useroptions = from r in rb.Users select new SelectListItem { Text = r.UserName, Value = r.ID.ToString() };
            return View();
        }
        public ActionResult Insert(UserRoleViewModel userrole)
        {
            var user = rb.Users.FirstOrDefault(r => r.ID == userrole.userid);
            var role = new Role { ID = userrole.roleid };
            rb.Roles.Attach(role);
            user.Roles.Add(role);
            if (rb.SaveChanges() == 0)
            {
                return Json(new { code = 400 });
            }
            return Json(new { code = 200 });
        }

        public ActionResult delete(UserRoleViewModel userrole)
        {
            var user = rb.Users.FirstOrDefault(r => r.ID == userrole.userid);
            var role = new Role { ID = userrole.roleid };
            rb.Roles.Attach(role);
            user.Roles.Remove(role);
            if (rb.SaveChanges() == 0)
            {
                return Json(new { code = 400 });
            }
            return Json(new { code = 200 });
        }

        public ActionResult Update(UserRoleViewModel userrole)
        {
            if (userrole.roleid == userrole.Uproleid)
            {
                return Json(new { code = 400 });
            }
            var user = rb.Users.FirstOrDefault(r => r.ID == userrole.userid);

            var role = new Role { ID = userrole.roleid };
            rb.Roles.Attach(role);

            var uproleid = new Role { ID = userrole.Uproleid };
            rb.Roles.Attach(uproleid);
            user.Roles.Remove(role);
            user.Roles.Add(uproleid);
            if (rb.SaveChanges() == 0)
            {
                return Json(new { code = 400 });
            }
            return Json(new { code = 200 });
        }
    }
}