using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;


namespace Demo.Web.Mvc.Extensions {

  public static class ExtensionsHelper {

    public static DemoContext GetDemoContext(this HtmlHelper html) {

      DemoController controller = html.ViewContext.Controller as DemoController;

      if(controller != null) {
        return controller.DemoContext;
      }

      return null;

    }


    public static DemoController GetController(this HtmlHelper html) {

      DemoController controller = html.ViewContext.Controller as DemoController;

      return controller;
    }


  }
}
