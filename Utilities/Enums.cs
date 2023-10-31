using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;


namespace Demo.Utilities {
  public static class Enums {

    public static T ToEnum<T>(this string enumString) {
      //      EnumName e = "enumAsString".ToEnum<EnumName>();
      return (T)Enum.Parse(typeof(T), enumString);
    }


    public static string ToDescription(this Enum value) {
      return GetDescription(value);
    }


    public static string GetDescription(this Enum value) {
      Type type = value.GetType();
      System.Reflection.FieldInfo fi = type.GetField(value.ToString());
      if (fi != null) {
        DescriptionAttribute[] descriptions = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
        return descriptions.Length > 0 ? descriptions[0].Description : value.ToString();
      } else {
        return "";
      }
    }


    // alternative to the above... dont know which is better see
    // http://stackoverflow.com/questions/1799370/getting-attributes-of-enums-value#9276348
    //public static string GetDescription2(this Enum enumValue) {
    //  var attribute = enumValue.GetAttributeOfType<DescriptionAttribute>();

    //  return attribute == null ? String.Empty : attribute.Description;
    //}


    //private static T GetAttributeOfType<T>(this Enum enumVal) where T : System.Attribute {
    //  Type type = enumVal.GetType();
    //  System.Reflection.MemberInfo[] memInfo = type.GetMember(enumVal.ToString());
    //  Object[] attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
    //  return (T)attributes[0];
    //}


    public static int[] ToIntArray<T>(T[] value) {
      int[] result = new int[value.Length];
      for(int i = 0; i < value.Length; i++) {
        result[i] = Convert.ToInt32(value[i]);
      }
      return result;
    }


    public static T[] FromIntArray<T>(int[] value) {
      T[] result = new T[value.Length];
      for(int i = 0; i < value.Length; i++) {
        result[i] = (T)Enum.ToObject(typeof(T), value[i]);
      }
      return result;
    }


    internal static T Parse<T>(string value, T defaultValue) {
      if(Enum.IsDefined(typeof(T), value)) {
        return (T)Enum.Parse(typeof(T), value);
      }

      int num;
      if(int.TryParse(value, out num)) {
        if(Enum.IsDefined(typeof(T), num)) {
          return (T)Enum.ToObject(typeof(T), num);
        }
      }

      return defaultValue;
    }



  }



}
