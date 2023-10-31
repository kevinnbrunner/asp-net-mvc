using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;


namespace Demo.Web.Mvc {
  public class JCookies {
    private static int cookiesAgeYears = 5;
    private static int rememberMeMinutes_ = 1400 * 7;
    private static int dontRememberMeMinutes_ = 1400;
    private static string domain = "domain.com";


    public static void SetVistorCookie (Entities.Cookies.Cookied cookieData) {
      SetVisitorCookie(cookieData);
    }


    public static void SetAuthorizationCookie (Entities.Cookies.Cookied cookieData) {
      SetAuthCookie(cookieData);
    }


    private static void SetSubscriptionsCookie (Entities.Cookies.Cookied cookieData) {
      DateTime expireDate = DateTime.Now.AddMinutes(rememberMeMinutes_);

      if (!cookieData.RememberMe) {
        expireDate = DateTime.Now.AddMinutes(dontRememberMeMinutes_);
      }

      SetVistorCookie(cookieData);

      FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
        version: 1,
        name: cookieData.AccountId,
        issueDate: DateTime.Now,
        expiration: expireDate,
        isPersistent: true,
        userData: "",
        cookiePath: FormsAuthentication.FormsCookiePath
      );


      HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));

      authCookie.Secure = true;
      authCookie.Path = "/";
      authCookie.Expires = expireDate;

      if (HttpContext.Current.Request.Url.Host.ToLower().IndexOf(domain) > -1) {
        authCookie.Domain = "." + domain;
      }

      if (HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName] != null) {
        HttpContext.Current.Response.SetCookie(authCookie);
      } else {
        HttpContext.Current.Response.Cookies.Add(authCookie);
      }


      SetRegisteredCookie();

    }


    private static void SetVisitorCookie (Entities.Cookies.Cookied cookieData) {

      if (String.IsNullOrEmpty(cookieData.AccountId)) {
        cookieData.AccountId = "0";
      }

      HttpCookie vistorCookieNew = new HttpCookie("CookieName");

      vistorCookieNew.Secure = true;
      vistorCookieNew.Path = "/";
      vistorCookieNew.Expires = DateTime.Now.AddYears(cookiesAgeYears);
      vistorCookieNew["AccountId"] = cookieData.AccountId;

      if (HttpContext.Current.Request.Url.Host.ToLower().IndexOf("domain.com") > -1) {
        vistorCookieNew.Domain = ("." + "domain.com");
      }

      try {
        HttpContext.Current.Response.SetCookie(vistorCookieNew);
      }
      catch { }

      if (cookieData.RememberMe) {
        rememberMeMinutes_ = dontRememberMeMinutes_;
      }

    }


    private static void SetAuthCookie (Entities.Cookies.Cookied cookieData) {

      DateTime expireDate = DateTime.Now.AddMinutes(rememberMeMinutes_);

      if (!cookieData.RememberMe) {
        expireDate = DateTime.Now.AddMinutes(rememberMeMinutes_);
      }

      SetVistorCookie(cookieData);

      FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
        version: 1,
        name: cookieData.AccountId,
        issueDate: DateTime.Now,
        expiration: expireDate,
        isPersistent: true,
        userData: "",
        cookiePath: FormsAuthentication.FormsCookiePath
      );


      HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));

      authCookie.Secure = true;
      authCookie.Path = "/";
      authCookie.Expires = expireDate;

      if (HttpContext.Current.Request.Url.Host.ToLower().IndexOf(domain) > -1) {
        authCookie.Domain = "." + domain;
      }

      if (HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName] != null) {
        HttpContext.Current.Response.SetCookie(authCookie);
      } else {
        HttpContext.Current.Response.Cookies.Add(authCookie);
      }


      SetRegisteredCookie();

    }



    private static void SetRegisteredCookie () {

      HttpCookie regesteredCookie = new HttpCookie("siteRegister");

      regesteredCookie.Secure = true;
      regesteredCookie.Path = "/";
      regesteredCookie.Expires = DateTime.Now.AddYears(cookiesAgeYears);
      regesteredCookie["siteRegister"] = "true";

      if (HttpContext.Current.Request.Url.Host.ToLower().IndexOf(domain) > -1) {
        regesteredCookie.Domain = "." + domain;
      }

      if (HttpContext.Current.Request.Cookies["siteRegister"] != null) {
        HttpContext.Current.Response.SetCookie(regesteredCookie);
      } else {
        HttpContext.Current.Response.Cookies.Add(regesteredCookie);
      }

    }


    public static bool IsRememberMe () {
      bool rememberMe = false;
      HttpCookie rememberMeCookie = null;

      rememberMeCookie = HttpContext.Current.Request.Cookies["RememberMe"];

      if (rememberMeCookie != null) {
        int rMe = 0;
        if (int.TryParse(rememberMeCookie["RememberMe"], out rMe)) {
          if (rMe > dontRememberMeMinutes_) {
            rememberMe = true;
          }
        }
      }

      return rememberMe;
    }



    public static void SignOut () {

      HttpContext.Current.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
      FormsAuthentication.SignOut();
    }


    public static void LogOut () {

      HttpContext.Current.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
      FormsAuthentication.SignOut();
    }



    public static void SetAcceptPolicyCookie () {

      HttpCookie cookiePolicyCookie = new HttpCookie("AcceptCookiePolicyName");

      cookiePolicyCookie.Secure = true;
      cookiePolicyCookie.Value = "-";
      cookiePolicyCookie.Path = "/";
      cookiePolicyCookie.Expires = DateTime.Now.AddYears(cookiesAgeYears);

      if (HttpContext.Current.Request.Url.Host.ToLower().IndexOf(domain) > -1) {
        cookiePolicyCookie.Domain = "." + domain;
      }

      if (HttpContext.Current.Request.Cookies["AcceptCookiePolicyName"] != null) {
        HttpContext.Current.Response.SetCookie(cookiePolicyCookie);
      } else {
        HttpContext.Current.Response.Cookies.Add(cookiePolicyCookie);
      }

    }


    public static bool IsAcceptPolicyCookie () {
      bool val = true;
      HttpCookie acceptCookiePolicyCookie = HttpContext.Current.Request.Cookies["AcceptCookiePolicyName"];
      if (acceptCookiePolicyCookie == null) {
        val = false;
      }
      return val;
    }



    public static void SetConfirmCookie (string accountID) {

      HttpCookie confirmCookie = new HttpCookie("SetConfirmCookie");

      confirmCookie.Secure = true;
      confirmCookie.Value = accountID;
      confirmCookie.Path = "/";
      confirmCookie.Expires = DateTime.Now.AddYears(cookiesAgeYears);

      if (HttpContext.Current.Request.Url.Host.ToLower().IndexOf(domain) > -1) {
        confirmCookie.Domain = "." + domain;
      }

      if (HttpContext.Current.Request.Cookies["AcceptCookiePolicyName"] != null) {
        HttpContext.Current.Response.SetCookie(confirmCookie);
      } else {
        HttpContext.Current.Response.Cookies.Add(confirmCookie);
      }

    }





  }
}
