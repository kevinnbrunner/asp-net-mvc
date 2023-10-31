using System;
using System.Web.Mvc;
using System.Web.Routing;

using Boxx.Services;
using Boxx.Services.Contracts;

using Ninject;


namespace Boxx.App_Start {
  public class NinjectControllerFactory : DefaultControllerFactory {
    private IKernel ninjectKernel;

    public NinjectControllerFactory () {
      ninjectKernel = new StandardKernel();
      AddBindings();
    }

    protected override IController GetControllerInstance (RequestContext requestContext, Type controllerType) {
      return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
    }

    private void AddBindings () {
      ninjectKernel.Bind<IMembersService>().To<MembersService>();
    }
  }

}