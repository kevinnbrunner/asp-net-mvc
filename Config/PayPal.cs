using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using PayPal.Api;


namespace Demo.Config {
  public static class PayPalConfiguration {
    private readonly static string ClientId;
    private readonly static string ClientSecret;


    static PayPalConfiguration () {
      Dictionary<string, string> config = GetConfig();
      ClientId = Config.PaypalConfig.PaypalClientId;
      ClientSecret = Config.PaypalConfig.PaypalClientSecret;

    }


    private static Dictionary<string, string> GetConfig () {
      return PayPal.Api.ConfigManager.Instance.GetProperties();
    }


    public static PayPal.Api.APIContext GetAPIContext (bool getLive, string requrestId) {

      PayPal.Api.APIContext apiContext = null;
      try {
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        apiContext = new PayPal.Api.APIContext(GetAccessToken(getLive), requrestId);
        Dictionary<string, string> configMap = new Dictionary<string, string>();
        if (getLive) {
          configMap.Add("mode", "live");
        } else {
          configMap.Add("connectionTimeout", "360000");
          configMap.Add("mode", "sandbox");
        }
        apiContext.Config = configMap;
      }
      catch (Exception e) {
        string m = e.Message;
      }

      return apiContext;
    }


    private static string GetAccessToken (bool getLive) {
      OAuthTokenCredential credToken = null;
      Dictionary<string, string> payPalConfig = new Dictionary<string, string>();
      if (getLive) {
        payPalConfig.Add("mode", "live");
        credToken = new OAuthTokenCredential(ClientId, ClientSecret, payPalConfig);
      } else {
        payPalConfig.Add("connectionTimeout", "360000");
        payPalConfig.Add("mode", "sandbox");
        credToken = new OAuthTokenCredential(ClientId, ClientSecret, payPalConfig);
      }
      string accessToken = credToken.GetAccessToken();

      return accessToken;
    }



  }


}
