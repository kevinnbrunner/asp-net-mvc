using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Demo.Entities.Shop {

  public class CreditCard {

    public int CreditCardId { get; set; }

    public int AccountId { get; set; }
    
    public string NameOnName { get; set; }
    
    public string CardFirst6 { get; set; }
    
    public string CardLast4 { get; set; }

    public DateTime CvvCheckDate { get; set; }

    public DateTime LastUsed {  get; set; }
    
    public string Token { get; set; }
    
    public string TokenDate { get; set; }
    
    public DateTime ExpireDate { get; set; }


  }


}
