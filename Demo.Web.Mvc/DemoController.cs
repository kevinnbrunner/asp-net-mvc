using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

using Demo.Config;
using Demo.Entities;
using Demo.Helpers;
using Demo.Utilities;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace Demo.Web.Mvc {

  public class DemoController : DemoJsonController {

    public string RawUrl = "";
    public string AbsoluteUri = "";
    public string IPAddress = "";
    public string AnonymousID = "";
    public string VisitorID = "";
    public string BrowserID = "";
    public string CheckMyGuid = "";
    public bool RememberMe = false;
    public bool IsRedisAccountLookUp = true;
    public string Country = "US";
    public bool IsUnAuthAdmin = false;

    public HttpCookie IsSubscriberCookie;
    public HttpCookie vistorCookie;
    public HttpCookie dataCookie;
    public HttpCookie registeredCookie;

    public DemoContext DemoContext { get; protected set; }
    public string ViewBasePath { get; set; } = "";
    public string ControllerName { get; set; } = "";
    public string AreaName { get; set; } = "";
    public string AreaPath { get; set; } = "";
    public string ActionName { get; set; } = "";
    public string PathAndQuery { get; set; } = "";
    public string FullUrl { get; set; } = "";
    public string UrlPath { get; set; } = "";
    public string Domain { get; set; } = "";
    public string[] QueryString { get; set; } = new string[] { };



    public DemoController () {
      DemoContext = new DemoContext(this) {
      };
    }


    public string ControllerPath {
      get { return this.ControllerName + "/"; }
    }


    public string AreaControllerPath {
      get {
        return this.AreaPath + this.ControllerPath;
      }
    }


    protected override void Initialize (RequestContext requestContext) {
      //always on TOP!
      base.Initialize(requestContext);

      if (Request != null && Request.RawUrl != null) {
        this.RawUrl = Request.RawUrl.ToString().ToLower();
        this.AbsoluteUri = Request.Url.AbsoluteUri.ToLower();
      } else {
        this.RawUrl = "null";
      }

      if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null) {
        this.IPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
      } else if (Request.UserHostAddress != null && Request.UserHostAddress.Length != 0) {
        this.IPAddress = Request.ServerVariables["REMOTE_ADDR"];
      } else {
        this.IPAddress = ":::0";
      }

      string[] ips = IPAddress.Split(new char[] { ',', ' ', '-' }, StringSplitOptions.RemoveEmptyEntries);
      if (ips.Length > 1) {
        this.IPAddress = ips[0].Trim();
      }

      this.VisitorID = this.VisitorID ?? "";
      this.BrowserID = this.BrowserID ?? "";
      this.CheckMyGuid = this.CheckMyGuid ?? "";
      this.ControllerName = this.ControllerName ?? "";
      this.AreaPath = this.AreaPath ?? "";
      this.ActionName = this.ActionName ?? "";
      this.ViewBasePath = this.ViewBasePath ?? "";

      this.RawUrl = Request.RawUrl ?? "";
      this.RawUrl = Request.RawUrl.ToLower();

      this.PathAndQuery = Request.Url.PathAndQuery ?? "";
      this.PathAndQuery = this.PathAndQuery.ToLower();

      this.FullUrl = Request.Url.AbsoluteUri ?? "";
      this.FullUrl = this.FullUrl.ToLower();

      this.UrlPath = Request.Url.AbsolutePath ?? "";
      this.UrlPath = this.UrlPath.ToLower();

      this.Domain = Request.Url.Authority ?? "";
      this.Domain = this.Domain.ToLower();

      string queryString = Request.Url.Query ?? "";

      if (queryString.Length == 0) {
        this.QueryString = new string[] { };
      } else {
        this.QueryString = queryString.Substring(1).Split(new char[] { '&' });
      }

      this.DemoContext.PageTitle = "";
      this.DemoContext.ReturnUrl = HttpUtility.HtmlDecode(((Request.QueryString["returnurl"]) ?? ""));
      this.DemoContext.PathAndQuery = this.PathAndQuery;
      this.DemoContext.UrlPath = this.UrlPath;
      this.DemoContext.Domain = this.Domain;
      this.DemoContext.IpAddress = this.IPAddress.SetAndTrim();

    }


    protected override void OnActionExecuted (ActionExecutedContext ctx) {
      base.OnActionExecuted(ctx);

    }


    protected override void OnActionExecuting (ActionExecutingContext ctx) {
      base.OnActionExecuting(ctx);

    }




    public void GetBestGuessBrowserName () {
      string browserName = "";

      this.DemoContext.UserAgent = Request.ServerVariables.Get("HTTP_USER_AGENT").SetAndTrim();

      string ua = this.DemoContext.UserAgent.SetAndTrim().ToLower();
      // this gets the most common browsers, not all... edge, ie, firefox, safari, opera, chrome make up 98%
      browserName = "chrome";
      if (ua.Contains("opera")) {
        browserName = "opera";
      } else if (ua.Contains("brave")) {
        browserName = "brave";
      } else if (ua.Contains("samsung")) {
        browserName = "samsung";
      } else if (ua.Contains("vivaldi")) {
        browserName = "vivaldi";
      } else if (ua.Contains("yandex")) {
        browserName = "yandex";
      } else if (ua.Contains("edge") || ua.Contains("edg/")) {
        browserName = "edge";
      } else if (ua.Contains("trident") || ua.Contains("msie")) {
        browserName = "ie";
      } else if (ua.Contains("seamonkey")) {
        browserName = "firefox";
      } else if (ua.Contains("firefox")) {
        browserName = "firefox";
      } else if (ua.Contains("chrome")) {
        browserName = "chrome";
      } else if (ua.Contains("safari")) {
        // http://www.useragentstring.com/
        // must come after "chrome" as chrome says it is safari too...
        browserName = "safari";
      } else {
        browserName = "chrome";
      }

      this.DemoContext.BrowserName = browserName;
    }


  }


  public class ImageResult : ActionResult {
    public ImageResult () { }

    public ImageResult (MemoryStream imageStream, string contentType) {
      this.ImageStream = imageStream ?? throw new ArgumentNullException("imageStream");
      this.ContentType = contentType ?? throw new ArgumentNullException("contentType");
    }


    public ImageResult (MemoryStream imageStream, ImageContentTypes contentType = ImageContentTypes.Jpg) {
      this.ImageStream = imageStream ?? throw new ArgumentNullException("imageStream");
      this.ContentType = ContentMimeType.MimeType(contentType.ToString().ToLower());
    }

    public MemoryStream ImageStream { get; private set; }
    public string ContentType { get; private set; }


    public override void ExecuteResult (ControllerContext context) {
      if (context == null) {
        throw new ArgumentNullException("context");
      }

      HttpResponseBase response = context.HttpContext.Response;

      try {
        response.ContentType = this.ContentType;
        response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
        response.Cache.SetValidUntilExpires(false);
        response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
        response.Cache.SetCacheability(HttpCacheability.NoCache);
        response.Cache.SetNoStore();

        byte[] buffer = new byte[4096];
        while (true) {
          int read = this.ImageStream.Read(buffer, 0, buffer.Length);
          if (read == 0) {
            break;
          }

          response.OutputStream.Write(buffer, 0, read);
        }

        response.End();
      }
      catch { }
    }
  }


  public enum ImageContentTypes {
    Png,
    Jpg,
    Gif
  }


  public static class ControllerExtensions {
    public static ImageResult Image (this Controller controller, MemoryStream imageStream, string contentType) {
      return new ImageResult(imageStream, contentType);
    }

    public static ImageResult Image (this Controller controller, byte[] imageBytes, string contentType) {
      return new ImageResult(new MemoryStream(imageBytes), contentType);
    }

    public static ImageResult Image (this Controller controller, MemoryStream imageStream, ImageContentTypes contentType = ImageContentTypes.Jpg) {
      return new ImageResult(imageStream, contentType);
    }

    public static ImageResult Image (this Controller controller, byte[] imageBytes, ImageContentTypes contentType = ImageContentTypes.Jpg) {
      return new ImageResult(new MemoryStream(imageBytes), contentType);
    }
  }


  public abstract class DemoJsonController : System.Web.Mvc.Controller {
    protected override void Initialize (RequestContext requestContext) {
      base.Initialize(requestContext);
    }


    protected override void OnActionExecuted (ActionExecutedContext ctx) {
      base.OnActionExecuted(ctx);
    }


    protected override void OnActionExecuting (ActionExecutingContext ctx) {
      base.OnActionExecuting(ctx);
    }


    private string IdentityName = null;

    public string IdentityID {
      get {

        if (this.HttpContext != null && this.HttpContext.User != null && this.HttpContext.User.Identity != null && string.IsNullOrEmpty(IdentityName)) {
          IdentityName = this.HttpContext.User.Identity.Name;
        }

        return IdentityName;

      }
      set { IdentityName = value; }
    }


    public string ToJson<T> (IEnumerable<T> collection) {
      //, Newtonsoft.Json.Formatting formatType = Newtonsoft.Json.Formatting.None, JsonDateTimeFormat jdtf = JsonDateTimeFormat.MMMddyyyy
      string dtformat = ToDTFormat(JsonDateTimeFormat.MMMddyyyy);
      string json = null;

      if (collection != null) {
        IsoDateTimeConverter isoDTC = new IsoDateTimeConverter() { DateTimeFormat = dtformat };
        json = JsonConvert.SerializeObject(collection, Formatting.None, isoDTC);
      }

      return json;
    }


    public string ToJson (object objToSerialize, Newtonsoft.Json.Formatting formatType = Newtonsoft.Json.Formatting.None, JsonDateTimeFormat jdtf = JsonDateTimeFormat.MMMddyyyy) {

      string dtformat = ToDTFormat(jdtf);
      string json = null;

      if (objToSerialize != null) {
        IsoDateTimeConverter isoDTC = new IsoDateTimeConverter() { DateTimeFormat = dtformat };
        json = JsonConvert.SerializeObject(objToSerialize, formatType, isoDTC);
      }

      return json;
    }


    public string ToJsonWithSettings (object objToSerialize, ReferenceLoopHandling refLoop = ReferenceLoopHandling.Error, Newtonsoft.Json.Formatting formatType = Newtonsoft.Json.Formatting.None, JsonDateTimeFormat jdtf = JsonDateTimeFormat.MMMddyyyy) {
      string dtformat = ToDTFormat(jdtf);
      string json = null;

      if (objToSerialize != null) {
        JsonSerializerSettings settings = new JsonSerializerSettings() { ReferenceLoopHandling = refLoop, DateFormatString = dtformat };
        json = JsonConvert.SerializeObject(objToSerialize, formatType, settings);
      }
      return json;
    }


    public T FromJson<T> (string jsonData) {
      return JsonConvert.DeserializeObject<T>(jsonData);
    }


    protected string RenderPartialViewToString () {
      return RenderPartialViewToString(null, null);
    }


    protected string RenderPartialViewToString (string viewName) {
      return RenderPartialViewToString(viewName, null);
    }


    protected string RenderPartialViewToString (object model) {
      return RenderPartialViewToString(null, model);
    }


    protected string RenderPartialViewToString (string viewName, object model) {
      if (string.IsNullOrEmpty(viewName)) {
        viewName = ControllerContext.RouteData.GetRequiredString("action");
      }

      ViewData.Model = model;

      using (StringWriter sw = new StringWriter()) {
        ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
        ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
        viewResult.View.Render(viewContext, sw);

        return sw.GetStringBuilder().ToString();
      }
    }


    public static string ToDTFormat (JsonDateTimeFormat jdtf) {
      string dtformat = "";
      switch (jdtf.ToString()) {
        case "MMMddyy":
          dtformat = "MMM dd, yy";
          break;
        case "MMMddyyyy":
          dtformat = "MMM dd, yyyy";
          break;
        case "yyyymmdd":
          dtformat = "MMM dd yyyy HH:mm:ss";
          break;
        default:
          dtformat = "MMM dd, yyyy";
          break;
      }
      return dtformat;
    }


    public enum JsonDateTimeFormat {
      MMMddyy,
      MMddyy,
      MMddyyyy,
      yymmdd,
      yyyymmdd,
      MMMddyyyy

    }


    public class FastJsonResult : JsonResult {

      public override void ExecuteResult (ControllerContext context) {
        if (context == null) {
          throw new ArgumentNullException("context");
        }

        if (this.JsonRequestBehavior == JsonRequestBehavior.DenyGet && String.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase)) {
          throw new InvalidOperationException("JSON GET is not allowed");
        }

        HttpResponseBase response = context.HttpContext.Response;
        response.ContentType = String.IsNullOrEmpty(this.ContentType) ? "application/json" : this.ContentType;

        if (this.ContentEncoding != null) {
          response.ContentEncoding = this.ContentEncoding;
        }

        if (this.Data == null) {
          return;
        }

        string jsonString = Helpers.Serializing.Serialize(this.Data, SerializationType.fastJSON);

        response.Write(jsonString);
      }
    }


    public class FastJsonpResult : JsonResult {


      public override void ExecuteResult (ControllerContext context) {
        if (context == null) {
          throw new ArgumentNullException("context");
        }

        HttpResponseBase response = context.HttpContext.Response;
        response.ContentType = String.IsNullOrEmpty(this.ContentType) ? "application/x-javascript" : this.ContentType;
        this.JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet;

        if (this.ContentEncoding != null) {
          response.ContentEncoding = this.ContentEncoding;
        }

        if (this.Data == null) {
          throw new Exception("Data must be provided in the request!");
        }

        string callBack = context.HttpContext.Request.QueryString["callback"];

        if (string.IsNullOrWhiteSpace(callBack)) {
          throw new Exception("Callback function name must be provided in the request!");
        }

        string jsonString = String.Format("{0}({1});", callBack, Helpers.Serializing.Serialize(this.Data, SerializationType.fastJSON));

        response.Write(jsonString);
      }
    }



  }
}
