using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Demo.Entities.Shop {

  public class CartItems {
    public CartItems() { }
    
    public int cartId { get; set; }
    
    public int ProductId { get; set; }
    
    public string ProductName { get; set; }
    
    public int Quantity { get; set; }
    
    public int Price { get; set; }
    
    public int Discount {  get; set; }


  }

}
