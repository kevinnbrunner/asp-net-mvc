using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Demo.Entities;

namespace Demo.DataAccess {

  public interface IAccessRepository {

    Account GetAccount (int accountId);

  }


}
