using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Demo.Entities;
using Demo.Entities.Shop;


namespace Demo.Services.Contracts {

  public interface IOrderService {

    void ProcessOrder (OrderTransaction orderTransaction);

  }

}
