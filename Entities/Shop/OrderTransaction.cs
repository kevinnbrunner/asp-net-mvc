using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Demo.Entities.Shop {
  public class OrderTransaction {
    public OrderTransaction () {

    }

    public Order Order { get; set; }
    
    public PaymentTender PaymentTender { get; set; }

    public List<CartItems> CartItems { get; set; }

    public string TransactionResponse {  get; set; }


  }

}
