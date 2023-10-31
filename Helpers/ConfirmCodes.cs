using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Demo.Utilities;

namespace Demo.Helpers {
  public class ConfirmCodes {

    public static bool IsValidConfirmCodeForm (string confirmCode) {
      bool isValid = false;

      if (!confirmCode.IsEmpty()) {
        string[] epochSplit = confirmCode.Split(new char[] { '_', ';', ':', '|', '-', ' ' }, StringSplitOptions.RemoveEmptyEntries);
        if (epochSplit.Length > 1) {

          //string guid = String.Join("-", confirmCode.Split('-').Take(5));

          if (String.Join("-", confirmCode.Split('-').Take(5)).IsGuid()) {
            isValid = true;
          }
        }
      }

      return isValid;
    }




  }

}
