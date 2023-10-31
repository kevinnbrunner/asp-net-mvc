using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

using Demo.Config;
using Demo.DataAccess;
using Demo.Entities;
using Demo.Entities.Shop;
using Demo.Services.Contracts;
using Demo.Utilities;

using Ninject;


namespace Demo.Services {

  [PartCreationPolicy(CreationPolicy.NonShared)]
  public class OrderService : IOrderService {

    [Inject]
    private IAccessRepository AccessRepository { get; set; }

    [ImportingConstructor]
    public OrderService (
      [Import] IAccessRepository accessRepository
    ) {
      this.AccessRepository = accessRepository;
    }


    public void ProcessOrder (OrderTransaction orderTransaction) {

      // save pending Order

      // save CartItems

      // process PaymentTender
      //  create payment transaction
      //  attempt transaction
      //  save payment result
      //  apply payment 

      // finalize Order

      // email receipt

      // set response

    }





  }
}
