using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Demo.Helpers {
  public static class ProtocolBuffers {

    public static MemoryStream ToMemoryStream (string serialzedData) {
      byte[] byteOfDataString = Convert.FromBase64String(serialzedData);
      MemoryStream streamOfByteOfDataString = new MemoryStream(byteOfDataString);

      return streamOfByteOfDataString;
    }


    public static string SerializeToString (object dataObject) {
      // is object decorated with the attributes like in "Person" below?
      string serialzedData = "";
      MemoryStream dataObjectStream = new MemoryStream();
      ProtoBuf.Serializer.Serialize(dataObjectStream, dataObject);
      serialzedData = Convert.ToBase64String(dataObjectStream.ToArray());

      return serialzedData;
    }



    public static void WriteToRawFile (object dataObject, string fileName) {

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


    public static T DeserializeProto<T> (string data) {
      T typeToReturn;
      typeToReturn = ProtoBuf.Serializer.Deserialize<T>(new MemoryStream(Convert.FromBase64String(data)));

      return typeToReturn;
    }


    public static T DeserializeProtoFile<T> (string filePath) {

      StreamReader streamReader = new StreamReader(filePath, Encoding.Unicode);
      string data = streamReader.ReadToEnd();
      streamReader.Close();

      T typeToReturn;
      typeToReturn = ProtoBuf.Serializer.Deserialize<T>(new MemoryStream(Convert.FromBase64String(data)));

      return typeToReturn;
    }





  }
}
