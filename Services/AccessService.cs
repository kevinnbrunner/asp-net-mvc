using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

using Demo.Config;
using Demo.DataAccess;
using Demo.Entities;
using Demo.Services.Contracts;
using Demo.Utilities;

using Ninject;


namespace Demo.Services {

  [PartCreationPolicy(CreationPolicy.NonShared)]
  public class AccessService : IAccessService {

    [Inject]
    private IAccessRepository AccessRepository { get; set; }

    [ImportingConstructor]
    public AccessService(
      [Import] IAccessRepository accessRepository
    ) {
      this.AccessRepository = accessRepository;
    }


    public Account GetAccount (int accountId) { 
      return AccessRepository.GetAccount(accountId);
    }


  }
}
