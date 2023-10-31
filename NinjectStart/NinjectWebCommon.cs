[assembly: WebActivator.PreApplicationStartMethod(typeof(Demo.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(Demo.App_Start.NinjectWebCommon), "Stop")]

namespace Demo.App_Start {
  using System;
  using System.Web;

  using Microsoft.Web.Infrastructure.DynamicModuleHelper;

  using Demo.DataAccess;
  using Demo.Services;
  using Demo.Services.Contracts;

  using Ninject;
  using Ninject.Web.Common;

  public static class NinjectWebCommon {
    private static readonly Bootstrapper bootstrapper = new Bootstrapper();

    /// <summary>
    /// Starts the application
    /// </summary>
    public static void Start() {
      DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
      DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
      bootstrapper.Initialize(CreateKernel);
    }

    /// <summary>
    /// Stops the application.
    /// </summary>
    public static void Stop() {
      bootstrapper.ShutDown();
    }

    /// <summary>
    /// Creates the kernel that will manage your application.
    /// </summary>
    /// <returns>The created kernel.</returns>
    private static IKernel CreateKernel() {
      StandardKernel ninjectKernel = new StandardKernel();
      ninjectKernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
      ninjectKernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

      RegisterServices(ninjectKernel);

      return ninjectKernel;
    }

    /// <summary>
    /// Load your modules or register your services here!
    /// </summary>
    /// <param name="ninjectKernel">The kernel.</param>
    private static void RegisterServices(IKernel ninjectKernel) {

      //repository
      ninjectKernel.Bind<IAccessRepository>().To<AccessRepository>().InRequestScope();

      //services
      ninjectKernel.Bind<IAccessService>().To<AccessService>().InRequestScope();

    }



  }




}
