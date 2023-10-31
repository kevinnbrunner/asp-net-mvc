using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Demo.Entities;

using Dapper;


namespace Demo.DataAccess {

  public partial class AccessRepository : Repository, IAccessRepository {

    public Account GetAccount (int accountId) {

      string sql = @"/*sql*/ --GetAccount

                        SET NOCOUNT ON;

                        SELECT
                              AccountId
                            , Name
                            , Email
                          FROM
                            Accounts a
                          WHERE 1 = 1
                            AND AccountId   = @AccountId";

      Account account = null;

      int sqlType = 1;

      if (sqlType == 1) {
        try {
          account = this.SiteConnection.Query<Account>(sql, new { AccountId = accountId }).FirstOrDefault();
        }
        catch (Exception e) {
          string m = e.Message;
          // Log error
        }
      }

      if (sqlType != 1) {
        using (var conn = GetDbConnection(database: Database.Site)) {
          conn.Open();

          try {
            account = conn.Query<Account>(sql, new { AccountID = accountId }).FirstOrDefault();
          }
          catch (Exception e) {
            string m = e.Message;
            // Log error
          }

        }
      }

      return account;
    }




  }
}
