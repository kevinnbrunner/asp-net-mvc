using System;
using System.Web.Mvc;
using System.Web.Routing;

using Demo.Services;
using Demo.Services.Contracts;

using Ninject;


namespace Demo.App_Start {
  public class NinjectControllerFactory : DefaultControllerFactory {
    private IKernel ninjectKernel;

    public NinjectControllerFactory() {
      ninjectKernel = new StandardKernel();
      AddBindings();
    }

    protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType) {
      return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
    }

    private void AddBindings() {
      ninjectKernel.Bind<IAccessService>().To<AccessService>();
    }
  }

}