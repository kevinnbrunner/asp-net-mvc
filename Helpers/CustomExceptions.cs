using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
  
using Demo.Entities;
using Demo.Utilities;


namespace Demo.Helpers {

  public class _Exceptions : Exception {

    public string ErrorMessage {
      get {
        return base.Message.ToString();
      }
    }

    public _Exceptions(string errorMessage) : base(errorMessage) { }

    public _Exceptions(string errorMessage, Exception innerEx) : base(errorMessage, innerEx) { }
  }

  public class RunTimeException : _Exceptions {
    public RunTimeException(string errorMessage) : base(errorMessage) { }
    public RunTimeException(string errorMessage, Exception innerEx) : base(errorMessage, innerEx) { }
  }
}

