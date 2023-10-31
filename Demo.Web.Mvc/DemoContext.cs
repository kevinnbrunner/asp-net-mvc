using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Demo.Config;
using Demo.Entities;


namespace Demo.Web.Mvc {

  public class DemoContext {

    public DemoController Controller { get; set; }

    public DemoContext () { }

    public DemoContext (DemoController controller) {
      this.Controller = controller;

      this.AdminBarModel = null;

    }

    public Account Account { get; set; } = new Account();
    public Account AdminAccount { get; set; } = new Account(); // set in SiteBaseController when necessary
    public Account ImpersonationAccount { get; set; } = new Account();// set in AdminBaseController when necessary

    public string PageTitle { get; set; } = "";


    public List<string> ContentJavaScripts { get; set; } = new List<string>();
    public List<string> ContentStyleSheets { get; set; } = new List<string>();
    public List<string> PrintStyleSheets { get; set; } = new List<string>();
    public List<string> ContentStyleSheetsOverride { get; set; } = new List<string>();
    public List<string> SeparateStyleSheets { get; set; } = new List<string>();
    public List<string> ScriptsInHead { get; set; } = new List<string>();
    public List<string> ScriptsAtBottom { get; set; } = new List<string>();

    //new js and css includes pull from any folder in /content
    //use these going forward.  Loads AFTER existing scripts and sheets
    public List<string> JavaScripts { get; set; } = new List<string>();
    public List<string> StyleSheets { get; set; } = new List<string>();
    public List<string> StyleLinks { get; set; } = new List<string>();
    public List<string> StyleSheetsLast { get; set; } = new List<string>();
    public List<string> ExternalStyleSheets { get; set; } = new List<string>();
    public List<string> ExternalJavaScripts { get; set; } = new List<string>();

    public string PageLayout { get; set; } = "";
    public string MetaKeywords { get; set; } = "";
    public string MetaDescription { get; set; } = "";
    public string MetaDatum { get; set; } = "";
    public int CartItemCount { get; set; } = 0;

    public dynamic AdminBarModel { get; set; }
    public string AdminBarView { get; set; } = "";

    public bool ViewAsAdmin { get; set; } = false;

    public int PageContentID { get; set; } = 0;
    public string VisitorID { get; set; } = "";

    public string Impersonating { get; set; } = "";
    public string ImpersonatingEmail { get; set; } = "";
    public string AdminAccountID { get; set; } = "";
    public bool IsImpersonating { get; set; } = false; // set in SiteBaseController when necessary

    public string ImpersonationAccountIpAddress { get; set; } = ""; // set in AdminBaseController when necessary
    public string UserAgent { get; set; } = "";
    public string BrowserName { get; set; } = "";
    public bool IsMobile { get; set; } = false;

    public string ReturnUrl { get; set; } = "";
    public string PathAndQuery { get; set; } = "";
    public string UrlPath { get; set; } = "";

    public string Domain { get; set; } = "";

    public string Token { get; set; } = "";

    public int BrowserWaitTime { get; set; } = 0;


    public bool IsAdmin {
      get {
        if (this.Account != null && this.Account.IsAdmin) {
          return true;
        }
        return false;
      }
    }

    public string TrackingID { get; set; } = "";

        public bool IsActiveOrFrozenSubscriber { get; set; } = false;
    public bool IsRegistered { get; set; } = false;
    public string IpAddress { get; set; } = "";

    public bool HasCreditCard { get; set; } = false;

    
  }


}
