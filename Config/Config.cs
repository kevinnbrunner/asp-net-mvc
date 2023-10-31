using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;


namespace Demo.Config {

  public partial class SiteContacts {


    public readonly static string KevinEmail = "kevin@domain.com";
  }


  public enum TracingForYou {
    Kevin,
  }


  public enum HBaseTableEnvironment {
    eProduction = 1,
    eTest,
    eInvalid
  }





  public class PaypalConfig : ReadFromConfigFile {
    public readonly static string PaypalHost = ReadFromConfigFile._PaypalHost;
    public readonly static string PaypalUser = ReadFromConfigFile._PaypalUser;
    public readonly static string PaypalVendor = ReadFromConfigFile._PaypalVendor;
    public readonly static string PaypalPartner = ReadFromConfigFile._PaypalPartner;
    public readonly static string PaypalPassword = ReadFromConfigFile._PaypalPassword;

    public readonly static string PayFlowProPalHost = ReadFromConfigFile._PaypalHost;
    public readonly static string PayFlowProPalUser = ReadFromConfigFile._PaypalUser;
    public readonly static string PayFlowProPalVendor = ReadFromConfigFile._PaypalVendor;
    public readonly static string PayFlowProPalPartner = ReadFromConfigFile._PaypalPartner;
    public readonly static string PayFlowProPalPassword = ReadFromConfigFile._PaypalPassword;

    public readonly static string PaypalClientId = ReadFromConfigFile._PaypalClientId;
    public readonly static string PaypalClientSecret = ReadFromConfigFile._PaypalClientSecret;

  }



  public class AppConfig : ReadFromConfigFile {

    public readonly static int PasswordVersion = ReadFromConfigFile._PasswordVersion;

    public readonly static string MailServer = ReadFromConfigFile._MailServer;
    public readonly static int MailServerPort = ReadFromConfigFile._MailServerPort;
    public readonly static string MailSender = ReadFromConfigFile._MailSender;
    public readonly static string MailSenderPassword = ReadFromConfigFile._MailSenderPassword;
    public readonly static bool EnableErrorLogEmail = ReadFromConfigFile._EnableErrorLogEmail;
    public readonly static bool ReadSearchResultsFromFile = ReadFromConfigFile._ReadSearchResultsFromFile;

    public readonly static string DefaultContentID = "261426";

    public readonly static string[] SupportedExtensions = new string[] { ".mp4", ".avi", ".mpeg", ".mov", ".mp3", ".wma", ".ogg", ".pjdf" };

    public readonly static string ElasticSearchServerHttp = ReadFromConfigFile._ElasticSearchServerHttp;

  }


  
  public class DataBaseConfig : ReadFromConfigFile {
    public readonly static string SiteConnectionString = _SiteConnectionString;
    public readonly static string SiteMembersConnectionString = _SiteMembersConnectionString;

    public readonly static string HBaseIPAddress = _HBaseIPAddress;
    public readonly static int HBasePortNo = _HBasePortNo;
    public readonly static int ImpalaPortNo = _ImpalaPortNo;

    public readonly static string[] HBaseThriftNodes = _HBaseThiftNodes;

    public readonly static string RedisIP = _RedisIP;

    public readonly static string RedisPort = _RedisPort;

    public readonly static int HBaseTimeoutPeriod = 300;


  }


  public class ReadFromConfigFile {

    static ReadFromConfigFile () { }

    protected static string[] _HBaseThiftNodes {
      get {
        return Array.ConvertAll(ConfigurationManager.AppSettings["HBaseThiftNodes"].Split(','), p => p.Trim());
      }
    }

    protected static int _PasswordVersion = 3;

    protected static int _DownloadTimeOutCount { 
      get {
        int downloadTimeOutCount = 5;
        if(!int.TryParse(ConfigurationManager.AppSettings["DownloadTimeOutCount"], out downloadTimeOutCount)) {
          downloadTimeOutCount = 5;
        }
        return downloadTimeOutCount;
      } 
    }
    protected static string _ElasticSearchServerHttp { get { return ConfigurationManager.AppSettings["ElasticSearchServerHttp"]; } }

    protected static string _SiteConnectionString { get { return ConfigurationManager.ConnectionStrings["SiteConnection"].ConnectionString; } }
    protected static string _SiteMembersConnectionString { get { return ConfigurationManager.ConnectionStrings["MembersConnection"].ConnectionString; } }

    protected static string _HBaseIPAddress { get { return ConfigurationManager.AppSettings["HBaseIPAddress"]; } }
    protected static int _HBasePortNo { get { return Convert.ToInt32(ConfigurationManager.AppSettings["HBasePortNo"]); } }
    protected static int _ImpalaPortNo { get { return Convert.ToInt32(ConfigurationManager.AppSettings["ImpalaPortNo"]); } }

    protected static string _MailServer { get { return ConfigurationManager.AppSettings["MailServer"]; } }
    protected static int _MailServerPort { get { return Convert.ToInt32(ConfigurationManager.AppSettings["MailServerPort"]); } }
    protected static string _MailSender { get { return ConfigurationManager.AppSettings["MailSender"]; } }
    protected static string _MailSenderPassword { get { return ConfigurationManager.AppSettings["MailSenderPassword"]; } }

    protected static bool _EnableErrorLogEmail { get { return Convert.ToBoolean(ConfigurationManager.AppSettings["EnableErrorLogEmail"]); } }

    protected static bool _ReadSearchResultsFromFile { get { return ConfigurationManager.AppSettings["ReadSearchResultsFromFile"].ToLower() == "true" ? true : false; } }

    protected static string _CDN0 { get { return ConfigurationManager.AppSettings["CDN0"]; } }
    protected static string _CDN1 { get { return ConfigurationManager.AppSettings["CDN1"]; } }
    protected static string _CDN2 { get { return ConfigurationManager.AppSettings["CDN2"]; } }
    protected static string _CDN3 { get { return ConfigurationManager.AppSettings["CDN3"]; } }
    protected static string _CDN4 { get { return ConfigurationManager.AppSettings["CDN4"]; } }

    protected static string _LocalPublic { get { return ConfigurationManager.AppSettings["LocalPublic"]; } }
    protected static string _LocalSecure { get { return ConfigurationManager.AppSettings["LocalSecure"]; } }

    protected static string _RedisIP { get { return ConfigurationManager.AppSettings["RedisIP"]; } }
    protected static string _RedisPort  { get { return ConfigurationManager.AppSettings["RedisPort"]; } }

    protected static string _HBaseEnivronment { get { return ConfigurationManager.AppSettings["HBaseEnivronment"]; } }

    protected static string _PaypalClientId { get { return ConfigurationManager.AppSettings["PaypalClientId"] ?? ""; ; } }
    protected static string _PaypalClientSecret { get { return ConfigurationManager.AppSettings["PaypalClientSecret"] ?? ""; ; } }

    protected static string _PaypalHost { get { return "payflowpro.paypal.com"; } }
    protected static string _PaypalUser { get { return ConfigurationManager.AppSettings["PaypalUser"]; } }
    protected static string _PaypalVendor { get { return ConfigurationManager.AppSettings["PaypalVendor"]; } }
    protected static string _PaypalPartner { get { return ConfigurationManager.AppSettings["PaypalPartner"]; } }
    protected static string _PaypalPassword { get { return ConfigurationManager.AppSettings["PaypalPassword"] ?? ""; } }



  }




  public static class HbaseConfig {
    public static readonly List<string> AllProfileTags = new List<string>() { "AppAccess", "WebOnly", "designation_All", "designation_EarlyAccess", "designation_FoundingMember", "designation_Guest", "designation_Partner", "designation_PilotMember", "designation_Standard", "designation_StandardGracePeriod", "memberlevel_All", "memberlevel_Basic", "memberlevel_Diamond", "memberlevel_FoundingMember", "memberlevel_Gold", "memberlevel_Guest", "memberlevel_Platinum", "memberlevel_Pro", "memberlevel_Standard" };
  }



}
