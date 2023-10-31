using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Demo.Utilities;


namespace Demo.Entities.Cookies {

  public class Cookied {

    public Cookied (int accountId, int anonymousID, bool rememberMe) {
      this.AccountId = accountId.ToString();
      this.RememberMe = rememberMe;
      if (anonymousID < 1) {
        this.AnonymousId = AccountId.ToString(); ;
      } else {
        this.AnonymousId = anonymousID.ToString(); ;
      }
    }

    public Cookied (Account account, string anonymousID, bool rememberMe) {
      if (account == null) { // || account.AccountID_ == 0) {
        return;
      }
      this.AccountId = account.AccountId.ToString(); ;
      this.RememberMe = rememberMe;
      this.AnonymousId = anonymousID;
    }


    public string AnonymousId { get; set; } = "";

    public string AccountId { get; set; } = "";

    public bool RememberMe { get; set; } = true;

  }
}
