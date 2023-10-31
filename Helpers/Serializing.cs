using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

using Demo.Entities;
using Demo.Utilities;

using fastJSON;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace Demo.Helpers {
  public static class Serializing {

    public static string ToFastJson (this object anObject, bool beautify = false) {
      return Serialize(anObject, SerializationType.fastJSON, beautify);
    }

    //public static string ToFastJSON (this object anObject) {
    //  return Serialize(anObject, SerializationType.fastJSON);
    //}

    public static string ToFastJsonWithTypes (this object anObject, bool beautify = false) {
      return Serialize(anObject, SerializationType.fastJSONWithTypes, beautify);
    }


    public static string SerializeResponse (Object obj, bool returnProtocolBuffers, bool isGtLtToBeReplaced = true, bool isAmpToBeReplaced = true) {
      string responseString = "";

      if ((obj == null) || (obj is string) || obj.ToString().StartsWith("<html>")) {
        if (obj == null) {
          obj = "null";
        }
        return obj.ToString();
      } else {

        if (returnProtocolBuffers) {
          responseString = Helpers.ProtocolBuffers.SerializeToString(obj);
        } else {
          responseString = JSON.ToJSON(obj, new fastJSON.JSONParameters() { UseExtensions = false, UsingGlobalTypes = false, UseUTCDateTime = false, EnableAnonymousTypes = true, SerializeNullValues = true });
        }

        responseString = responseString.Replace("&amp;", "&");

        if (isGtLtToBeReplaced) {
          //replace &amp; with &
          if (!isAmpToBeReplaced) {
            responseString = WebUtility.HtmlEncode(responseString).Replace("&lt;", "<").Replace("&gt;", ">").Replace("&quot;", "\"").Replace("&#39;", "'").Replace("&amp;", "&");
          } else {
            responseString = WebUtility.HtmlEncode(responseString).Replace("&lt;", "<").Replace("&gt;", ">").Replace("&quot;", "\"").Replace("&#39;", "'");
          }
        }
      }

      return responseString;
    }


    public static string Serialize (Object obj, SerializationType serializationType, bool beautify = false) {
      string responseString = "";

      if ((obj == null) || (obj is string) || obj.ToString().StartsWith("<html>")) {
        if (obj == null) {
          obj = "null";
        }
        if (serializationType == SerializationType.Base64FastJson) {
          return obj.ToString().Base64StringEncode();
        }
        return obj.ToString();
      } else {

        switch (serializationType) {
          case SerializationType.fastJSON:
            try {
              responseString = JSON.ToJSON(obj, new fastJSON.JSONParameters() { UseExtensions = false, UsingGlobalTypes = false, UseUTCDateTime = false, EnableAnonymousTypes = true, SerializeNullValues = true });
              if (beautify) {
                responseString = JSON.Beautify(responseString);
              }
            }
            catch (Exception e ){
              try {
                //Exception e = obj as Exception;
                responseString = e.Message;
              }
              catch {
                responseString = "An uncaught error!";
              }
            }
            break;

          case SerializationType.fastJSONWithTypes:
            responseString = JSON.ToJSON(obj, new JSONParameters() { UseExtensions = true, UsingGlobalTypes = true });
            if (beautify) {
              responseString = JSON.Beautify(responseString);
            }
            break;

          case SerializationType.NewtonJSON:
            responseString = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            break;

          case SerializationType.ProtocolBuffers:
            responseString = ProtoBufToString(obj);
            break;

          case SerializationType.XML:

            Type objType = obj.GetType();
            XmlSerializer xs = new XmlSerializer(obj.GetType());
            MemoryStream dataObjectStream = new MemoryStream();
            xs.Serialize(dataObjectStream, obj);
            responseString = Encoding.ASCII.GetString(dataObjectStream.ToArray());
            break;

          case SerializationType.BinaryDotNet:
            if (!obj.GetType().IsSerializable) {
              responseString = "";
            }

            using (MemoryStream stream = new MemoryStream()) {
              new BinaryFormatter().Serialize(stream, obj);
              responseString = Convert.ToBase64String(stream.ToArray());
            }
            break;

          case SerializationType.Base64FastJson:
            try {
              string responseStringPre = JSON.ToJSON(obj, new fastJSON.JSONParameters() { UseExtensions = false, UsingGlobalTypes = false, UseUTCDateTime = false, EnableAnonymousTypes = true, SerializeNullValues = true });
              byte[] rawBytes = Encoding.UTF8.GetBytes(responseStringPre);
              responseString = Convert.ToBase64String(rawBytes);
            }
            catch {
              try {
                Exception e = obj as Exception;
                responseString = e.Message;
              }
              catch {
                responseString = "An uncaught error!";
              }
            }
            break;

          default:
            throw new Exception();

        }

        return responseString;
      }
    }


    public static MemoryStream ToMemoryStream (string serialzedData) {
      return new MemoryStream(Convert.FromBase64String(serialzedData));
    }


    public static string ProtoBufToString (object dataObject) {
      // is object decorated with the attributes like in "Person" below?
      string serialzedData = "";
      MemoryStream dataObjectStream = new MemoryStream();
      ProtoBuf.Serializer.Serialize(dataObjectStream, dataObject);
      serialzedData = Convert.ToBase64String(dataObjectStream.ToArray());

      return serialzedData;
    }


    public static void ProtoBufWriteToRawFile (object dataObject, string fileName) {

      MemoryStream ms = new MemoryStream();
      ProtoBuf.Serializer.Serialize(ms, dataObject);
      FileInfo fi = new FileInfo(fileName);
      if (!fi.Directory.Exists) {
        Directory.CreateDirectory(fi.Directory.FullName);
      }

      using (FileStream file = new FileStream(fileName, FileMode.Create, FileAccess.Write)) {
        ms.WriteTo(file);
      }

    }


    public static T DeserializeProtoBuf<T> (string data) {
      T typeToReturn;
      typeToReturn = ProtoBuf.Serializer.Deserialize<T>(new MemoryStream(Convert.FromBase64String(data)));

      return typeToReturn;
    }


    public static T DeserializeProtoBufFile<T> (string filePath) {

      StreamReader streamReader = new StreamReader(filePath, Encoding.Unicode);
      string data = streamReader.ReadToEnd();
      streamReader.Close();

      T typeToReturn;
      typeToReturn = ProtoBuf.Serializer.Deserialize<T>(new MemoryStream(Convert.FromBase64String(data)));

      return typeToReturn;
    }


    public static object DeserializeObjectDotNet (string str) {
      byte[] bytes = Convert.FromBase64String(str);

      using (MemoryStream stream = new MemoryStream(bytes)) {
        return new BinaryFormatter().Deserialize(stream);
      }
    }


    public static T DeserialzeWithFastJson<T> (string serialzedData) where T : new() {
      return Deserialize<T>(serialzedData, SerializationType.fastJSON);
    }


    public static T Deserialize<T> (string serialzedData, SerializationType serializationType) where T : new() {
      T dataObject;

      switch (serializationType) {

        case SerializationType.fastJSONWithTypes:
        case SerializationType.fastJSON:
          JSON.Parameters.UseExtensions = true;
          JSON.Parameters.UsingGlobalTypes = true;
          dataObject = JSON.ToObject<T>(serialzedData);
          break;

        case SerializationType.NewtonJSON:
          dataObject = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(serialzedData);
          break;

        case SerializationType.ProtocolBuffers:
          dataObject = ProtoBuf.Serializer.Deserialize<T>(Helpers.ProtocolBuffers.ToMemoryStream(serialzedData));
          break;

        case SerializationType.Base64FastJson:
          JSON.Parameters.UseExtensions = true;
          JSON.Parameters.UsingGlobalTypes = true;

          byte[] base64EncodedBytes = Convert.FromBase64String(serialzedData);
          serialzedData = Encoding.UTF8.GetString(base64EncodedBytes);

          dataObject = JSON.ToObject<T>(serialzedData);
          break;

        default:
          //throw new Exception("not implemented");

          if (string.IsNullOrEmpty(serialzedData)) {
            return new T();
          }
          serialzedData = serialzedData.Replace("soapenv:", "");
          serialzedData = serialzedData.Replace("ns6:", "");
          serialzedData = serialzedData.Replace("ns2:", "");
          serialzedData = serialzedData.Replace(":soapenv", "");
          serialzedData = serialzedData.Replace(":ns6", "");
          serialzedData = serialzedData.Replace(":ns2", "");
          try {
            using (var stringReader = new StringReader(serialzedData)) {
              var serializer = new XmlSerializer(typeof(T));
              return (T)serializer.Deserialize(stringReader);
            }
          }
          catch {
            return new T();
          }

      }

      return dataObject;

    }



  }
}
