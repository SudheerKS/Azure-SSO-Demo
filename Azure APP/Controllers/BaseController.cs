using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Azure_APP.Controllers
{
    public class BaseController : Controller
    {
        //public User LoginUser;

      //  private readonly IUserService _userService;

        //public BaseController(IUserService userService)
        //{
        //    _userService = userService;
        //}

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var loginRroute = new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } };
            base.OnActionExecuting(context);

            var isAuthenticated = HttpContext?.User?.Identity.IsAuthenticated;

            if (isAuthenticated != null && isAuthenticated == true)
            {
                //var loginId = User.FindFirst("LoginId")?.Value;

                var loginId = HttpContext.User.Identity.Name;

                if (string.IsNullOrEmpty(loginId)) context.Result = new RedirectToRouteResult(loginRroute);

                //LoginUser = _userService.GetUserByLoginId(loginId).Result;

                //if (LoginUser == null || LoginUser.IsActive == false)
                //{
                    HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    context.Result = new RedirectToRouteResult(loginRroute);
                //}
                //else
                //{
                //    LoginUser.LastLogin = DateTime.Now;
                //    LoginUser.IsLogedIn = true;
                //    _userService.UpdateLastLogin(LoginUser);
                //}
            }
            else
            {
                context.Result = new RedirectToRouteResult(loginRroute);
            }

            //var lang = Request.Cookies.FirstOrDefault(x => x.Key == "lang");
            //if (lang.Equals(default(KeyValuePair<string, string>)))
            //{
            //    Response.Cookies.Append("lang", "it-it");
            //}
            //else
            //{
            //    Response.Cookies.Append("lang", lang.Value);
            //}
        }
    }
}
