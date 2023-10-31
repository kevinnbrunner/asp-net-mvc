[assembly: WebActivator.PreApplicationStartMethod(typeof(Boxx.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(Boxx.App_Start.NinjectWebCommon), "Stop")]

namespace Boxx.App_Start {
  using System;
  using System.Web;

  using Microsoft.Web.Infrastructure.DynamicModuleHelper;

  using Ninject;
  using Ninject.Web.Common;

  public static partial class NinjectWebCommon {
    private static readonly Bootstrapper bootstrapper = new Bootstrapper();

    /// <summary>
    /// Starts the application
    /// </summary>
    public static void Start () {
      DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
      DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
      bootstrapper.Initialize(CreateKernel);
    }

    /// <summary>
    /// Stops the application.
    /// </summary>
    public static void Stop () {
      bootstrapper.ShutDown();
    }

    /// <summary>
    /// Creates the kernel that will manage your application.
    /// </summary>
    /// <returns>The created kernel.</returns>
    private static IKernel CreateKernel () {
      StandardKernel ninjectKernel = new StandardKernel();
      ninjectKernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
      ninjectKernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

      RegisterRepositories(ninjectKernel);
      RegisterServices(ninjectKernel);



      return ninjectKernel;
    }

  }




}
