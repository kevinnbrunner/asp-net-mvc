using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.Entities {
  
  public class Account {
  
    public Account () {
    }

    public int AccountId { get; set; }
    
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public bool IsAdmin { get; set; }


  }
}
