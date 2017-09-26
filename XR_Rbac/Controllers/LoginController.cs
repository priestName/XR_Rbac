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
    public class LoginController : Controller
    {
        Rbac rb = new Rbac();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User logUser)
        {
            if(!ModelState.IsValid)
            {
                return Json(new { code = 400 });
            }

            var user = rb.Users.FirstOrDefault(u=>u.UserName==logUser.UserName && u.Password==logUser.Password);
            if (user==null)
            {
                return Json(new { code = 404 });
            }
            //当前用户
            Session["User"] = user;
            var RoleModule = user.Roles.ToDictionary(r => r.ID, r => r.Modules);
            //角色模块
            Session["RoleModule"] = RoleModule;
            var Roles = user.Roles.ToList();
            //角色
            Session["Roles"] = Roles;
            Session["Role"] = Roles[0];
            return Json(new { code = 200 });
        }
    }
}