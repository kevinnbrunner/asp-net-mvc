using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Demo.Entities;


namespace Demo.Services.Contracts {

  public interface IAccessService {

    Account GetAccount(int accountId);

  }

}
