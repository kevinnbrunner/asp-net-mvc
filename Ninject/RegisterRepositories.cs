[assembly: WebActivator.PreApplicationStartMethod(typeof(Boxx.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(Boxx.App_Start.NinjectWebCommon), "Stop")]

namespace Boxx.App_Start {
  using Boxx.DataAccess.Elasticsearch;
  using Boxx.DataAccess.Elasticsearch.Contracts;
  //using Boxx.DataAccess.EsStore;
  using Boxx.DataAccess.HBase;
  using Boxx.DataAccess.HBaseRedis;
  using Boxx.DataAccess.Redis;
  using Boxx.DataAccess.Sql;

  using global::Ninject;
  using global::Ninject.Web.Common;

  public static partial class NinjectWebCommon {

    public static void RegisterRepositories(IKernel ninjectKernel) {

      ninjectKernel.Bind<IAdminRepository>().To<AdminRepository>().InRequestScope();
      ninjectKernel.Bind<ILoggingRepository>().To<LoggingRepository>().InRequestScope();
      ninjectKernel.Bind<IElmahRepository>().To<ElmahRepository>().InRequestScope();
      ninjectKernel.Bind<IConfirmCodeRepository>().To<ConfirmCodeRepository>().InRequestScope();
      ninjectKernel.Bind<IEmailTemplateRepository>().To<EmailTemplateRepository>().InRequestScope();

      ninjectKernel.Bind<IAccountsRepository>().To<AccountsRepository>().InRequestScope();
      ninjectKernel.Bind<IAccountsRedisRepository>().To<AccountsRedisRepository>().InRequestScope();
      ninjectKernel.Bind<IBillingManagerRepository>().To<BillingManagerRepository>().InRequestScope();

      ninjectKernel.Bind<ICartRepository>().To<CartRepository>().InRequestScope();
      ninjectKernel.Bind<IOrderRepository>().To<OrderRepository>().InRequestScope();
      ninjectKernel.Bind<IQuoteRepository>().To<QuoteRepository>().InRequestScope();
      ninjectKernel.Bind<IPaymentProcessRepository>().To<PaymentProcessRepository>().InRequestScope();
      ninjectKernel.Bind<IPayPalRepository>().To<PayPalRepository>().InRequestScope();

      ninjectKernel.Bind<IEsStoreRepository>().To<EsStoreRepository>().InRequestScope();
      ninjectKernel.Bind<IEsStoreSqlRepository>().To<EsStoreSqlRepository>().InRequestScope();

      ninjectKernel.Bind<IContentRepository>().To<ContentRepository>().InRequestScope();
      ninjectKernel.Bind<IContentRedisRepository>().To<ContentRedisRepository>().InRequestScope();
      ninjectKernel.Bind<IShadowSqlRepository>().To<ShadowSqlRepository>().InRequestScope();

      ninjectKernel.Bind<IErrorRepository>().To<ErrorRepository>().InRequestScope();
      ninjectKernel.Bind<IUpdateContentRepository>().To<UpdateContentRepository>().InRequestScope();

      ninjectKernel.Bind<IJuicerStoreRepository>().To<JuicerStoreRepository>().InRequestScope();
      ninjectKernel.Bind<ISearchRepository>().To<SearchRepository>().InRequestScope();
      ninjectKernel.Bind<IProductRepository>().To<ProductRepository>().InRequestScope();

      ninjectKernel.Bind<IKeywordRepository>().To<KeywordRepository>().InRequestScope();
      ninjectKernel.Bind<ITrackingRepository>().To<TrackingRepository>().InRequestScope();

      ninjectKernel.Bind<IDownloaderRepository>().To<DownloaderRepository>().InRequestScope();
      ninjectKernel.Bind<IJuicerWebRepository>().To<JuicerWebRepository>().InRequestScope();

      ninjectKernel.Bind<IResourceMonitoringRepository>().To<ResourceMonitoringRepository>().InRequestScope();

      ninjectKernel.Bind<IReferralRepository>().To<ReferralRepository>().InRequestScope();

      ninjectKernel.Bind<IHbaseStoreRepository>().To<HbaseStoreRepository>().InRequestScope();
      ninjectKernel.Bind<IRedisStoreRepository>().To<RedisStoreRepository>().InRequestScope();

      ninjectKernel.Bind<IShopRepository>().To<ShopRepository>().InRequestScope();

      ninjectKernel.Bind<IReportsRepository>().To<ReportsRepository>().InRequestScope();

      ninjectKernel.Bind<ICartRedisRepository>().To<CartRedisRepository>().InRequestScope();

      ninjectKernel.Bind<IRewardsRepository>().To<RewardsRepository>().InRequestScope();

      ninjectKernel.Bind<IKeapRepository>().To<KeapRepository>().InRequestScope();

      ninjectKernel.Bind<ISubscriptionsRepository>().To<SubscriptionsRepository>().InRequestScope();



      //ninjectKernel.Bind<IGroupAccountRepository>().To<GroupAccountRepository>().InRequestScope();
      //ninjectKernel.Bind<ISecurityRepository>().To<SecurityRepository>().InRequestScope();
      //
      //ninjectKernel.Bind<IBusyBoxxRepository>().To<BusyBoxxRepository>().InRequestScope();




      // for threaded app

    }



  }




}
