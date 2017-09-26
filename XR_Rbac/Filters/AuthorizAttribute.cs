using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XR_Rbac.Models;

namespace XR_Rbac.Filters
{
    public enum AuthorizationType {None,Identity, Authorization }

    public class AuthorizAttribute : FilterAttribute, IAuthorizationFilter
    {
        public AuthorizationType AuthorizationType { get; set; } = AuthorizationType.Authorization;

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (AuthorizationType == AuthorizationType.None) return;

            if (filterContext.HttpContext.Session["User"] == null)
            {
                ToLogin(filterContext);
            }
            if(AuthorizationType==AuthorizationType.Identity)return;
            var controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var role = filterContext.HttpContext.Session["Role"] as Role;
            if (role==null)
            {
                ToLogin(filterContext);
                return;
            }
            var module = role.Modules.FirstOrDefault(m => m.Controller == controller);
            if (module==null)
            {
                ToLogin(filterContext);
            }
        }
        public void ToLogin(AuthorizationContext filterContext)
        {
            var url = new UrlHelper(filterContext.RequestContext);
            filterContext.Result= new RedirectResult(url.Action("Index", "Login"));
        }
    }
}