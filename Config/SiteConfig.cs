using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Config {
  public partial class SiteConfig : ReadFromConfigFile {

        public readonly static string BasicLayout = @"~/Views/Shared/_BasicLayout.cshtml";
    public readonly static string AdminLayout = @"~/Views/Shared/_LayoutAdmin.cshtml";
    public readonly static string AngularJsAdminLayout = @"~/Views/Shared/_Layout_AngularJsAdmin.cshtml";
    public readonly static string HeaderLessLayout = @"~/Views/Shared/_HeaderLessLayout.cshtml";
    public readonly static string SimpleHeaderLayout = @"~/Views/Shared/_SimpleHeaderLayout.cshtml";
    public readonly static string AngularJsLayout = @"~/Views/Shared/_AngularJsLayout.cshtml";
    public readonly static string AnonymousLayout = @"~/Views/Shared/_LayoutAnonymous.cshtml";

    public readonly static string EmailGrayDots = CDN1 + @"interface/grayDots.gif";
    public readonly static string EmailHeaderImage = CDN1 + @"interface/emailHeader.png";

    public static string GetCDN (int? cdnNumber) {
      int cdnNumber_ = 1; if (cdnNumber != null) { cdnNumber_ = (int)cdnNumber; }
      //return _GetCDN(cdnNumber_);
      string cdn = "";
      switch (cdnNumber_) {
        case -1:
          cdn = "";
          break;
        case 1:
          cdn = _CDN1;
          break;
        case 2:
          cdn = _CDN2;
          break;
        case 3:
          cdn = _CDN3;
          break;
        case 4:
          cdn = _CDN4;
          break;
        default:
          cdn = _CDN0;
          break;
      }
      return cdn;

    }
    public readonly static string CDN0 = ReadFromConfigFile._CDN0;
    public readonly static string CloudFrontPublic = ReadFromConfigFile._CDN1;
    public readonly static string CDN1 = ReadFromConfigFile._CDN1;
    public readonly static string CDN2 = ReadFromConfigFile._CDN2;
    public readonly static string CDN3 = ReadFromConfigFile._CDN3;
    public readonly static string CDN4 = ReadFromConfigFile._CDN4;


    public static string SiteType {
      get {
        string siteType = "live";

        string domain = "";

        if (domain.IndexOf("test.") > 0) {
          siteType = "test";
        } else if (domain.IndexOf("dev.") > 0) {
          siteType = "dev";
        }

        return siteType;
      }
    }

    public static HBaseTableEnvironment HBaseEnivronment {
      get {
        if (ReadFromConfigFile._HBaseEnivronment == "Production") {
          return HBaseTableEnvironment.eProduction;
        } else if (ReadFromConfigFile._HBaseEnivronment == "Test") {
          return HBaseTableEnvironment.eTest;
        } else {
          return HBaseTableEnvironment.eInvalid;
        }
      }
    }

        public readonly static int PasswordVersion = ReadFromConfigFile._PasswordVersion;

    public readonly static string MailServer = ReadFromConfigFile._MailServer;
    public readonly static int MailServerPort = ReadFromConfigFile._MailServerPort;
    public readonly static string MailSender = ReadFromConfigFile._MailSender;
    public readonly static string MailSenderPassword = ReadFromConfigFile._MailSenderPassword;
    public readonly static bool EnableErrorLogEmail = ReadFromConfigFile._EnableErrorLogEmail;
    public readonly static bool ReadSearchResultsFromFile = ReadFromConfigFile._ReadSearchResultsFromFile;

    public readonly static string LocalPublic = ReadFromConfigFile._LocalPublic;
    public readonly static string LocalSecure = ReadFromConfigFile._LocalSecure;


    public readonly static string[] SupportedExtensions = new string[] { ".mp4", ".avi", ".mpeg", ".mov", ".mp3", ".wma", ".ogg", ".pjdf" };

    public readonly static string ElasticSearchServerHttp = ReadFromConfigFile._ElasticSearchServerHttp;

    public readonly static string ProfilePicSuggestionID = "1417496855";

  }


}
