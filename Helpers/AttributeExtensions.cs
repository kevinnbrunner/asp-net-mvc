using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

using Demo.Entities;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AdminOnlyAttribute : AuthorizeAttribute {
  public AdminOnlyAttribute() {
    Roles = AdminRole.Admin.ToString();
  }
}



[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class AuthorizeRoleAttribute : AuthorizeAttribute {
  public AuthorizeRoleAttribute(params AdminRole[] AdminRole) {
    List<AdminRole> Roles = AdminRole.ToList();
  }

  protected override bool AuthorizeCore(HttpContextBase httpContext) {
    if(httpContext == null) {
      throw new ArgumentNullException("httpContext");
    }
    IPrincipal user = httpContext.User;
    if(!user.Identity.IsAuthenticated) {
      return false;
    }
    if((Roles.Length > 0) && !Roles.Select(p => p.ToString()).Any<string>(new Func<string, bool>(user.IsInRole))) {
      return false;
    }
    return true;
  }
}


// based on http://stackoverflow.com/questions/1160105/asp-net-mvc-disable-browser-cache
// [NoCache] as an attribute of a controller
public class NoCacheAttribute : ActionFilterAttribute {
  public override void OnResultExecuting(ResultExecutingContext filterContext) {
    filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
    filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
    filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
    filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
    filterContext.HttpContext.Response.Cache.SetNoStore();

    base.OnResultExecuting(filterContext);
  }
}


public class JRequireHttpsAttribute : ActionFilterAttribute {
  public override void OnResultExecuting(ResultExecutingContext filterContext) {
    filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
    filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
    filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
    filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
    filterContext.HttpContext.Response.Cache.SetNoStore();

    base.OnResultExecuting(filterContext);
  }
}


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
public class AutoRedirectToLogin : ActionFilterAttribute {
  //[AutoRedirectToLogin]
  public override void OnResultExecuted(ResultExecutedContext filterContext) {
    string url = System.Web.Security.FormsAuthentication.LoginUrl;
    int durationInSeconds = ((filterContext.HttpContext.Session.Timeout * 60) + 10); // Extra 10 seconds

    string headerValue = string.Concat(durationInSeconds, ";Url=", url);

    filterContext.HttpContext.Response.AppendHeader("Refresh", headerValue);

    base.OnResultExecuted(filterContext);
  }
}


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
public class AutoRefreshAttribute : ActionFilterAttribute {
  //[AutoRefresh(DurationInSeconds = 10)]
  //[AutoRefresh(ActionName = "Login", ControllerName = "Login", AreaName = "Members", DurationInSeconds = 300)]
  public const int DefaultDurationInSeconds = 300; // 5 Minutes

  public AutoRefreshAttribute() {
    DurationInSeconds = DefaultDurationInSeconds;
  }

  public int DurationInSeconds { get; set; }
  public string RouteName { get; set; }
  public string ControllerName { get; set; }
  public string ActionName { get; set; }
  public string AreaName { get; set; }

  public override void OnResultExecuted(ResultExecutedContext filterContext) {
    string url = BuildUrl(filterContext);
    string headerValue = string.Concat(DurationInSeconds, ";Url=", url);

    filterContext.HttpContext.Response.AppendHeader("Refresh", headerValue);

    base.OnResultExecuted(filterContext);
  }

  private string BuildUrl(ControllerContext filterContext) {
    UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);
    string url;

    if(!string.IsNullOrEmpty(RouteName)) {
      url = urlHelper.RouteUrl(RouteName);
    } else if(!string.IsNullOrEmpty(AreaName) && !string.IsNullOrEmpty(ControllerName) && !string.IsNullOrEmpty(ActionName)) {
      url = urlHelper.Action(ActionName, ControllerName, new { Area = AreaName });
    } else if(!string.IsNullOrEmpty(ControllerName) && !string.IsNullOrEmpty(ActionName)) {
      url = urlHelper.Action(ActionName, ControllerName);
    } else if(!string.IsNullOrEmpty(ActionName)) {
      url = urlHelper.Action(ActionName);
    } else {
      url = filterContext.HttpContext.Request.RawUrl;
    }

    return url;
  }
}


// public class AllowCorsAttribute : ActionFilterAttribute {
//   private string _url { get; set; } = "";
//   public AllowCorsAttribute (string url) {
//     _url = url;
//   }
//
//   public override void OnResultExecuting (ResultExecutingContext filterContext) {
//     if (!String.IsNullOrEmpty(_url)) {
//       filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", _url);
//     }
//     filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Headers", "djauth,authorization,content-type,*");
//     filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Credentials", "true");
//
//     base.OnResultExecuting(filterContext);
//   }
//
// }


