using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace Demo.Web.Mvc {
  //supported uri scheme values
  public enum UriScheme {
    Http,
    Https
  }


  public class RequireSSL : ActionFilterAttribute {
    private UriScheme TargetUriScheme { get; set; }
    public int? HttpPort { get; set; }
    public int? HttpsPort { get; set; }


    public override void OnActionExecuting(ActionExecutingContext filterContext) {
      HttpRequestBase reqst = filterContext.HttpContext.Request;
      HttpResponseBase respns = filterContext.HttpContext.Response;

      UriBuilder uriBuilder = null;
      bool isSecure = false;
      string url = "";

      if (reqst.IsSecureConnection) {
        isSecure = true;
      }

      string http_x_forwarded_port = filterContext.HttpContext.Request.ServerVariables["HTTP_X_FORWARDED_PORT"] + "";
      if(String.IsNullOrWhiteSpace(http_x_forwarded_port)) {
        http_x_forwarded_port = "empty";
      }

      if(http_x_forwarded_port == "443") {
        isSecure = true;
      }

      if(filterContext.HttpContext.Request.Url.Port == 44300 || filterContext.HttpContext.Request.Url.Port == 44301) {
        isSecure = true;
      }

      if(!isSecure && !reqst.IsLocal) {

        TargetUriScheme = UriScheme.Https;

        if(reqst.IsLocal) {
          uriBuilder = new UriBuilder(reqst.Url);
        } else {
          uriBuilder = new UriBuilder(reqst.Url) { Scheme = Uri.UriSchemeHttps, Port = 443 };
        }

        url = uriBuilder.Uri.ToString();
        respns.Redirect(url);

        //filterContext.Result = new RedirectResult(url);
      }

      base.OnActionExecuting(filterContext);
    }
  }


  public class RequireHttpsAttribute : System.Web.Mvc.RequireHttpsAttribute {

    public override void OnAuthorization(AuthorizationContext filterContext) {

      if(filterContext == null) {
        throw new ArgumentNullException("filterContext");
      }

      if(filterContext.HttpContext.Request.IsSecureConnection || filterContext.HttpContext.Request.ServerVariables["HTTP_X_FORWARDED_PORT"] == "443") {
        return;
      }


      string host = filterContext.HttpContext.Request.Url.Host.ToString().ToLower();

      if(host.Contains("localhost")) {
        return;
      }

      HandleNonHttpsRequest(filterContext);

    }
  }


}
