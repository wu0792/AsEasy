using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using AsEasy.Common;
using AsEasy.Controllers;
using AsEasy.Ext;

namespace AsEasy.Attrs
{
    public class AuthPermissionAttribute : FilterAttribute, IAuthenticationFilter
    {
        public AuthPermissionAttribute(List<Permission> requiredPermissions)
        {
            this.Order = 2;
            this.RequiredPermissions = requiredPermissions ?? new List<Permission>();
        }

        public AuthPermissionAttribute(Permission requiredPermission)
            : this(new List<Permission>() { requiredPermission })
        {

        }

        public List<Permission> RequiredPermissions { get; set; }

        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var baseController = filterContext.Controller as BaseController;
            if (baseController != null)
            {
                if (baseController.IsLogin)
                {
                    var exUser = baseController.ExUser;
                    var ownedPermissions = exUser.OwnedPermissions ?? new List<Permission>();
                    if (this.RequiredPermissions.Any(t => !ownedPermissions.Contains(t)))
                    {
                        filterContext.RedirectToLogin();
                    }
                }
                else
                {
                    filterContext.RedirectToLogin();
                }
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            //这个方法是在Action执行之后调用
        }
    }
}