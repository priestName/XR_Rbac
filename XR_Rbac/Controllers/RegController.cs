using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XR_Rbac.Models;
using XR_Rbac.Filters;

namespace XR_Rbac.Controllers
{
    [Authoriz(AuthorizationType = AuthorizationType.None)]
    public class RegController : Controller
    {
        Rbac rb = new Rbac();
        // GET: Reg
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddUser(User adduser)
        {
            if (!ModelState.IsValid)
            {
                return Json(new {code = 400});
            }
            try
            {
                var role = rb.Roles.FirstOrDefault(r => r.ID == 3);
                adduser.Roles.Add(role);
                rb.Users.Add(adduser);
                rb.SaveChanges();
            }
            catch (Exception)
            {
                return Json(new { code = 404 });
            }
            
            return Json(new { code = 200 });
        }
    }
}