using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Demo.Entities.Shop {

  public  class Payment {
    
    public int PaymentId { get; set; } 

    public int OrderId { get; set; }
    
    public DateTime PaymentDate { get; set; }
    
    public int PaymentAmount { get; set; }
    
    public string PaymentMethod { get; set; }

    public string PaymentStatus { get; set; }


  }

}
