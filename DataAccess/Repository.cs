using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Linq;

using Nest;


namespace Demo.DataAccess {
  public partial class Repository : IDisposable {


    public Repository () {
    }


    [ThreadStatic]
    private static IDbConnection siteConnection = null;

    [Export("SiteConnection")]
    public IDbConnection SiteConnection {
      get {

        if (siteConnection == null) {
          DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
          siteConnection = factory.CreateConnection();
          siteConnection.ConnectionString = Config.DataBaseConfig.SiteConnectionString;
          siteConnection.Open();

        }
        return siteConnection;

      }
    }
    
    [ThreadStatic]
    private static IDbConnection siteMembersConnection = null;

    [Export("SiteMembersConnection")]
    public IDbConnection SiteMembersConnection {
      get {

        if (siteMembersConnection == null) {
          DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
          siteMembersConnection = factory.CreateConnection();
          siteMembersConnection.ConnectionString = Config.DataBaseConfig.SiteMembersConnectionString;
          siteMembersConnection.Open();
        }

        return siteMembersConnection;

      }
    }



    public IDbConnection GetDbConnection () {
      return GetDbConnection(Database.Site);
    }


    public IDbConnection GetDbConnection(Database database) {

      string connectionString = String.Empty;

      switch (database) {
        case Database.Site:
          connectionString = Config.DataBaseConfig.SiteConnectionString;
          break;
        case Database.Members:
          connectionString = Config.DataBaseConfig.SiteMembersConnectionString;
          break;
      }

      DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
      DbConnection dbConnection = factory.CreateConnection();

      dbConnection.ConnectionString = connectionString;

      return dbConnection;
    }



    public void Dispose () {

      if (siteConnection != null) {
        siteConnection.Dispose();
        siteConnection = null;
      }

      if (siteMembersConnection != null) {
        siteMembersConnection.Dispose();
        siteMembersConnection = null;
      }


    }


    private static ElasticClient elasticSearchClient = null;

    protected ElasticClient ElasticSearchClient {
      get {

        if (elasticSearchClient == null) {
          var setting = new ConnectionSettings(new Uri(Demo.Config.SiteConfig.ElasticSearchServerHttp));
          //setting.DisableDirectStreaming(true);
          setting.DefaultIndex("Demo.Entities.ElasticSearch.SearchQueryQnA.IndicesName");
          elasticSearchClient = new ElasticClient(setting);
        }
        return elasticSearchClient;

      }
    }




  }


  public enum Database {
    Site,
    Members,

  }




}

