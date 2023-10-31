using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Demo.Entities.Shop {

  public class Order {

    public int OrderId { get; set; }

    public int AccountId { get; set; }

    public DateTime OrderDate { get; set; }

    public string OrderStatus { get; set; }

    public int Subtotal { get; set; }

    public string TaxName { get; set; }
    
    public decimal TaxRate { get; set; }

    public int Tax { get; set; }

    public int ShippingCharge { get; set; }
    
    public string ShippingMethods { get; set; }
    
    public int Discount { get; set; }
    
    public string DiscountName { get; set; }

    public int GrandTotal { get; set; }

    public int Balance { get; set; }

    public List<OrderItem> Items { get; set;}

    public List<Payment> Payments { get; set;}


  }

}
