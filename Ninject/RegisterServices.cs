[assembly: WebActivator.PreApplicationStartMethod(typeof(Boxx.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(Boxx.App_Start.NinjectWebCommon), "Stop")]

namespace Boxx.App_Start {
  using Boxx.Services;
  using Boxx.Services.Contracts;
  using Boxx.Services.Store;
  using Boxx.Services.Store.Elasticsearch;

  using global::Ninject;
  using global::Ninject.Web.Common;

  public static partial class NinjectWebCommon {


    /// <summary>
    /// Load your modules or register your services here!
    /// </summary>
    /// <param name="ninjectKernel">The kernel.</param>
    public static void RegisterServices(IKernel ninjectKernel) {

      ninjectKernel.Bind<IMembersService>().To<MembersService>().InRequestScope();
      ninjectKernel.Bind<IAccountsService>().To<AccountsService>().InRequestScope();
      ninjectKernel.Bind<IGuestService>().To<GuestService>().InRequestScope();

      ninjectKernel.Bind<IEmailTemplateService>().To<EmailTemplateService>().InRequestScope();
      ninjectKernel.Bind<IEmailSubscriptionsService>().To<EmailSubscriptionsService>().InRequestScope();
      ninjectKernel.Bind<IBillingManagerService>().To<BillingManagerService>().InRequestScope();

      ninjectKernel.Bind<IAdminService>().To<AdminService>().InRequestScope();
      ninjectKernel.Bind<IAccessService>().To<AccessService>().InRequestScope();
      ninjectKernel.Bind<ILoggingService>().To<LoggingService>().InRequestScope();
      ninjectKernel.Bind<IConfirmCodeService>().To<ConfirmCodeService>().InRequestScope();

      ninjectKernel.Bind<IOrderService>().To<OrderService>().InRequestScope();
      ninjectKernel.Bind<IShopService>().To<ShopService>().InRequestScope();
      ninjectKernel.Bind<IPaymentProcessService>().To<PaypalPayflowProService>().InRequestScope();
      ninjectKernel.Bind<IPaymentProcessExtraService>().To<PaymentProcessExtraService>().InRequestScope();

      ninjectKernel.Bind<IContentService>().To<ContentService>().InRequestScope();

      ninjectKernel.Bind<IErrorService>().To<ErrorService>().InRequestScope();
      ninjectKernel.Bind<IUpdateContentService>().To<UpdateContentService>().InRequestScope();
      ninjectKernel.Bind<IShadowSqlService>().To<ShadowSqlService>().InRequestScope();

      ninjectKernel.Bind<IJuicerStoreService>().To<JuicerStoreService>().InRequestScope();

      ninjectKernel.Bind<IKeywordService>().To<KeywordService>().InRequestScope();
      ninjectKernel.Bind<IEsStoreService>().To<EsStoreService>().InRequestScope();

      ninjectKernel.Bind<IDownloaderService>().To<DownloaderService>().InRequestScope();

      ninjectKernel.Bind<IProductService>().To<ProductService>().InRequestScope();
      ninjectKernel.Bind<ITrackingService>().To<TrackingService>().InRequestScope();

      ninjectKernel.Bind<IReferralService>().To<ReferralService>().InRequestScope();
      ninjectKernel.Bind<IQuoteService>().To<QuoteService>().InRequestScope();
      ninjectKernel.Bind<ICartService>().To<CartService>().InRequestScope();

      ninjectKernel.Bind<IKeapService>().To<KeapService>().InRequestScope();
      ninjectKernel.Bind<IPayPalService>().To<PayPalService>().InRequestScope();
      ninjectKernel.Bind<IJuicerWebService >().To<JuicerWebService >().InRequestScope();

      ninjectKernel.Bind<IReportsService>().To<ReportsService>().InRequestScope();

      ninjectKernel.Bind<IAdminMembersService>().To<AdminMembersService>().InRequestScope();
      ninjectKernel.Bind<IElmahService>().To<ElmahService>().InRequestScope();

      ninjectKernel.Bind<IRedisService>().To<RedisService>().InRequestScope();

      ninjectKernel.Bind<IRewardsService>().To<RewardsService>().InRequestScope();
      
      ninjectKernel.Bind<IStoreManagementService>().To<StoreManagementService>().InRequestScope();

      ninjectKernel.Bind<ISubscriptionsService>().To<SubscriptionsService>().InRequestScope();

      ninjectKernel.Bind<IStartUpService>().To<StartUpService>().InRequestScope();

    }
  }

}
