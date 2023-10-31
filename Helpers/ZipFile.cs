using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

using Ionic.Zip;


namespace Demo.Helpers {

  public static class Zip {

    public static string CreateZippedFile (string sourcePath, string fileNameToZip, string zippedFileName) {
      string zipFilePath = "";
      DirectoryInfo sourceDir = new DirectoryInfo(sourcePath);
      try {
        string zippedFile = "";
        if (sourceDir.Exists) {
          zippedFile = Path.Combine(sourcePath, zippedFileName);
          if (File.Exists(zippedFile)) {
            File.Delete(zippedFile);
          }
          using (ZipFile zip = new ZipFile()) {
            zip.ZipErrorAction = ZipErrorAction.Throw;
            zip.AddItem(Path.Combine(sourcePath, fileNameToZip), "");
            zip.Save(zippedFile);
          }
          zipFilePath = zippedFile;
        }
      }
      catch (Exception ex) {
        Console.WriteLine("Error In Ziping Process " + ex.Message.ToString());
        return "";
      }
      return zipFilePath;
    }





  }
}
