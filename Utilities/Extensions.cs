using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace Demo.Utilities {

  public enum DateTimeFormat {
    Date,
    Time,
    TimeMS,
    DateTime,
    DateTimeMS,
  }


  public static partial class StringExtensions {

    public static string SetAndTrimDotDotDot (this string text, int length) {
      if (text.Length >= length) {
        return text.Substring(0, length - 3) + "...";
      } else {
        return text.PadRight(length).Substring(0, length).Trim();
      }
    }


    public static string ToDbQuote (this object anObject) {
      if (anObject == null) {
        return "''";
      }

      return "'" + anObject.ToString() + "'";
    }


    public static string SetAndTrim (this string text, int maxLength = 0) {
      text = text ?? "";
      text = Regex.Replace(text, @"\s+", " ");
      if (maxLength > 0) {
        text = text.PadRight(maxLength).Substring(0, maxLength);
      }
      return text.Trim();
    }

    public static string ToStringLongDateTime (this DateTime now) {
      return String.Format("{0:MM/dd/yyyy HH:mm:ss:fff}", now);
    }


    public static DateTime ToHourFloor (this DateTime now) {
      return new DateTime(now.Year, now.Month, now.Day).AddHours(now.Hour);
    }


    public static bool IsEmpty (this string value) {
      return String.IsNullOrWhiteSpace(value);
    }


    public static bool IsInteger (string s, bool positiveOnly = false) {
      Regex regularExpression = new Regex("^-[0-9]+$|^[0-9]+$");
      if (positiveOnly) {
        regularExpression = new Regex("^[0-9]+$");
      }
      return regularExpression.Match(s).Success;
    }


    public static bool IsGuid (this string value) {
      bool isValid = true;

      if (value != null && (value.Length == 36 || value.Length == 38)) {
        Guid g = new Guid();
        if (!Guid.TryParse(value, out g)) {
          isValid = false;
        }
      } else {
        isValid = false;
      }

      return isValid;
    }


    public static bool IsGuidEpochValid (this string value) {
      if (value.IsEmpty()) {
        return false;
      } else {
        string[] epochSplit = value.Split(new char[] { '_', ';', ':', '|', '-', ' ' }, StringSplitOptions.RemoveEmptyEntries);
        if (epochSplit.Length > 1) {

          string guid = String.Join("-", value.Split('-').Take(5));

          if (!(String.Join("-", value.Split('-').Take(5)).IsGuid())) {
            return false;
          }

          long checkEpoch = 0;
          if (long.TryParse(epochSplit[epochSplit.Length - 1], out checkEpoch)) {
            if (checkEpoch < ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000)) {
              return false;
            }
          }
        }
      }
      return true;
    }


    public static bool ValidateEmail (this string emailAddress) {
      Regex e2 = new Regex(@"^((([a-zA-Z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~])+(\.([a-zA-Z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~])+)*))@((([a-zA-Z]|\d)|(([a-zA-Z]|\d)([a-zA-Z]|\d|-|\.|_|~)*([a-zA-Z]|\d)))\.)+(([a-zA-Z])|(([a-zA-Z])([a-zA-Z]|\d|-|\.|_|~)*([a-zA-Z])))\.?$");
      return e2.IsMatch(emailAddress);
    }



    public static IEnumerable<TSource> DistinctBy<TSource, TKey> (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) {
      HashSet<TKey> seenKeys = new HashSet<TKey>();
      foreach (TSource element in source) {
        if (seenKeys.Add(keySelector(element))) {
          yield return element;
        }
      }

    }


    public static string ToProper (this string inputWord, bool firstWordOnly = false) {

      string properWord = "";

      if (!String.IsNullOrWhiteSpace(inputWord)) {
        List<string> words = inputWord.Split(new char[] { ' ' }).ToList();
        int wordsCount = words.Count;
        if (firstWordOnly) {
          wordsCount = 1;
        }
        for (int i = 0; i < wordsCount; i++) {
          words[i] = words[i].Substring(0, 1).ToUpper() + words[i].Substring(1, words[i].Length - 1).ToLower();
        }
        properWord = String.Join(" ", words);
      }

      return properWord;
    }



    /// <summary>
    /// Encodes to Base64
    /// Test Coverage: Included
    /// </summary>
    /// <param name="val"></param>
    /// <returns>Base 64 Encoded string</returns>
    public static string Base64StringEncode (this string val) {
      byte[] toEncodeAsBytes = ASCIIEncoding.ASCII.GetBytes(val);
      string returnValue = Convert.ToBase64String(toEncodeAsBytes);
      return returnValue;
    }

    /// <summary>
    /// Decodes a Base64 encoded string
    /// Test Coverage: Included
    /// </summary>
    /// <param name="val"></param>
    /// <returns>Base 64 decoded string</returns>
    public static string Base64StringDecode (this string val) {
      string returnValue = "";
      Regex r = new Regex("^([A-Za-z0-9+/]{4})*([A-Za-z0-9+/]{3}=|[A-Za-z0-9+/]{2}==)?$");

      if (!val.IsEmpty() && r.IsMatch(val)) {
        try {
          byte[] encodedDataAsBytes = Convert.FromBase64String(val);
          returnValue = ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
        }
        catch {
        }
      }

      return returnValue;
    }


    public static string ConvertNumberToWord (long numberVal) {
      string[] powers = new string[] { "thousand ", "million ", "billion " };

      string[] ones = new string[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };

      string[] tens = new string[] { "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

      string wordValue = "";

      if (numberVal == 0) { return "zero"; }
      if (numberVal < 0) {
        wordValue = "negative ";
        numberVal = -numberVal;
      }

      long[] partStack = new long[] { 0, 0, 0, 0 };
      int partNdx = 0;

      while (numberVal > 0) {
        partStack[partNdx++] = numberVal % 1000;
        numberVal /= 1000;
      }

      for (int i = 3; i >= 0; i--) {
        long part = partStack[i];

        if (part >= 100) {
          wordValue += ones[part / 100 - 1] + " hundred ";
          part %= 100;
        }

        if (part >= 20) {
          if ((part % 10) != 0) {
            wordValue += tens[part / 10 - 2] + " " + ones[part % 10 - 1] + " ";
          } else {
            wordValue += tens[part / 10 - 2] + " ";
          }
        } else if (part > 0) {
          wordValue += ones[part - 1] + " ";
        }

        if (part != 0 && i > 0) {
          wordValue += powers[i - 1];
        }
      }

      return wordValue.Trim();
    }


    /// <summary>
    /// Convert to Ordinal number
    /// Test Coverage: Included
    /// </summary>
    /// <param name="val"></param>
    /// <returns>String representation of the Ordinal number</returns>
    public static string ToOrdinal (this int val) {
      if (val <= 0) { throw new ArgumentException("Cardinal must be positive."); }

      int lastTwoDigits = val % 100;
      int lastDigit = lastTwoDigits % 10;
      string suffix;
      switch (lastDigit) {
        case 1:
          suffix = "st";
          break;

        case 2:
          suffix = "nd";
          break;

        case 3:
          suffix = "rd";
          break;

        default:
          suffix = "th";
          break;
      }

      if (11 <= lastTwoDigits && lastTwoDigits <= 13) {
        suffix = "th";
      }

      return string.Format("{0}{1}", val, suffix);
    }


    public static byte[] GetUTF8ByteArray (this string content) {
      return Encoding.UTF8.GetBytes(content);
    }


    public static string FormatPrice (this decimal price, int pos = -1) {
      string s = "";
      switch (pos) {
        case 0:
          s = "$" + (String.Format("{0:#}", price));
          break;
        case 3:
          s = "$" + (String.Format("{0:#.00}", price)).PadLeft(6, ' ');
          break;
        case 4:
          s = "$" + (String.Format("{0:#.00}", price)).PadLeft(7, ' ');
          break;
        case 5:
          s = "$" + (String.Format("{0:#.00}", price)).PadLeft(8, ' ');
          break;
        case 6:
          s = "$" + (String.Format("{0:#.00}", price)).PadLeft(9, ' ');
          break;
        default:
          s = String.Format("{0:c}", price);
          break;
      }
      return s;
    }





  }

}
