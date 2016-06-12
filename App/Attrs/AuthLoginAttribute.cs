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
    public class AuthLoginAttribute : FilterAttribute, IAuthenticationFilter
    {
        public AuthLoginAttribute()
        {
            this.Order = 1;
        }

        public void OnAuthentication(AuthenticationContext filterContext)
        {
            //这个方法是在Action执行之前调用
            if (filterContext.HttpContext.Session != null)
            {
                var user = filterContext.HttpContext.Session[Const.SessionKey.LoginUser];
                if (user == null)
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