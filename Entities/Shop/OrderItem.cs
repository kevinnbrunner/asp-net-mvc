using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Demo.Entities.Shop {

  public class OrderItem {
    
    public int ItemId {  get; set; }
    
    public int OrderId {  get; set; } 
    
    public int ProductId { get; set; }
    
    public string ProductName { get; set; }
    
    public int Quantity {  get; set; }
    
    public int Price {  get; set; } // in pennies
    
    public int LineTotal { get; set; }


  }

}
