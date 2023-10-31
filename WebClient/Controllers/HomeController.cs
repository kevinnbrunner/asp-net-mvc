using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Demo.Web.Mvc;


namespace Demo.Controllers {
  public class HomeController : SiteBaseController {
    public ActionResult Index () {

      Entities.Account account = this.AccessService.GetAccount(5);
      return View();
    }

    public ActionResult About () {
      this.DemoContext.PageTitle = "Your application description page.";

      return View();
    }

    public ActionResult Contact () {
      this.DemoContext.PageTitle = "Your contact page.";

      return View();
    }
  }
}