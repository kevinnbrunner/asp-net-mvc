using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Demo.Entities.Shop {
  
  public class PaymentTender {
    
    public PaymentTender () {

    }

    public CreditCard CreditCard { get; set; }

    public int PayPalEmail { get; set; }



  }
}
