using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Demo.Config;
using Demo.Entities;
using Demo.Helpers;
using Demo.Services.Contracts;
using Demo.Utilities;
using Demo.Web.Mvc;

using Ninject;


namespace Demo.Controllers {
  public class SiteBaseController : DemoController {

    [Inject]
    public IAccessService AccessService { get; set; }


  }
}