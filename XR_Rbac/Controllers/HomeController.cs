using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XR_Rbac.Models;
using XR_Rbac.Filters;

namespace XR_Rbac.Controllers
{
    [Authoriz(AuthorizationType = AuthorizationType.Identity)]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Header()
        {
            var user = Session["User"] as User;
            var roles = Session["Roles"] as List<Role>;
            var role = Session["Role"] as Role;
            List<SelectListItem> roleList =new List<SelectListItem>();
            foreach (var item in roles)
            {
                roleList.Add(new SelectListItem {Text = item.Name,Value = item.ID.ToString(),Selected = item.ID==role.ID});
            }
            ViewBag.roles = roleList;
            return PartialView(user);
        }
        public ActionResult Nav(int roleid=0)
        {
            var roleModule = Session["RoleModule"] as Dictionary<int, ICollection<Module>>;
            var role = Session["Role"] as Role;
            var roles = Session["Roles"] as List<Role>;

            List<Module> module;
            if (roleModule.ContainsKey(roleid))
            {
                module = roleModule[roleid].ToList();
                Session["role"] = roles.FirstOrDefault(r => r.ID == roleid);
            }
            else
            {
                module = roleModule[role.ID].ToList();
            }
            return PartialView(module);
        }

        public ActionResult LoginOut()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}