using System;
using System.Collections.Generic;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;


namespace Demo.Helpers {
  public class Imaging {
    public static List<Base64Image> Base64ImagesFromFolderList (List<string> folders) {
      List<Base64Image> Base64Images = new List<Base64Image>();
      if (folders == null || folders.Count == 0) {
        folders.Add(@"Content\Images\Common\AsBase64");
        folders.Add(@"Content\Images\Default\AsBase64");
        folders.Add(@"Content\Images\Bright\AsBase64");
      }
      //string[] folders = { @"Content\Images\Common\AsBase64", @"Content\Images\Default\AsBase64", @"Content\Images\Bright\AsBase64" };

      foreach (string folder in folders) {
        List<Base64Image> theseBase64Images = new List<Base64Image>();
        StringBuilder imageCss = new StringBuilder();
        imageCss.Append("<html><head>\r\n");
        imageCss.Append("<style type='text/css'>\r\n");
        imageCss.Append("/**************************/\r\n");
        imageCss.Append("/***** Base64 Images ******/\r\n");
        imageCss.Append("/**************************/\r\n");
        imageCss.Append(".b64 li { display: inline-block; } ");

        string thisFolder = AppDomain.CurrentDomain.BaseDirectory + folder;

        DirectoryInfo di = new DirectoryInfo(thisFolder);
        FileInfo[] fileList = di.GetFiles();
        int stringCount = fileList.Length;


        for (int i = 0; i < stringCount; i++) {
          FileInfo f = fileList[i];

          try {
            string imageN = f.Name.Replace(f.Extension, "");
            string imageD = "data:image/" + f.Extension.ToLower().Substring(1) + ";base64," + Base64ImageCalc(f.FullName);

            System.Drawing.Image thisImage = System.Drawing.Image.FromFile(f.FullName);
            int imageH = thisImage.Height;
            int imageW = thisImage.Width;

            theseBase64Images.Add(new Base64Image(folder, f.Name, imageN, imageD, imageH, imageW));
            Base64Images.Add(new Base64Image(folder, f.Name, imageN, imageD, imageH, imageW));

            imageCss.Append(".");
            imageCss.Append(imageN);
            imageCss.Append(" {");

            if (imageH > 1 && imageW == 1) {
              imageCss.Append("background-repeat: repeat-x; ");
              imageCss.Append("height: ");
              imageCss.Append(imageH);
              imageCss.Append("px; ");
              imageCss.Append("width: ");
              imageCss.Append(imageW * 100);
              imageCss.Append("px; ");
            }

            if (imageH == 1 && imageW > 1) {
              imageCss.Append("background-repeat: repeat-y; ");
              imageCss.Append("height: ");
              imageCss.Append(imageH * 100);
              imageCss.Append("px; ");
              imageCss.Append("width: ");
              imageCss.Append(imageW);
              imageCss.Append("px; ");
            }

            if (imageH > 1 && imageW > 1) {
              imageCss.Append("background-repeat: repeat; ");
              imageCss.Append("height: ");
              imageCss.Append(imageH * 100);
              imageCss.Append("px; ");
              imageCss.Append("width: ");
              imageCss.Append(imageW * 100);
              imageCss.Append("px; ");
            }

            imageCss.Append("background: url(");
            imageCss.Append(imageD);
            imageCss.Append("); ");

            imageCss.Append(" ");
            imageCss.Append("}");
            imageCss.Append("\r\n");

          }
          catch { } //finally { }
        }

        imageCss.Append("div { margin-bottom: 5px; } \r\n");
        imageCss.Append("</style>\r\n");
        imageCss.Append("</head><body>\r\n");

        foreach (Base64Image img in theseBase64Images) {
          imageCss.Append("<div><ul class='b64' ><li");
          imageCss.Append(" class='");
          imageCss.Append(img.ImageName);
          imageCss.Append("' ></li>");
          imageCss.Append("<li style='margin-left: 10px;' >");
          imageCss.Append(img.FileName);
          imageCss.Append("&nbsp;=&gt;&nbsp;");
          imageCss.Append(img.ImageName);
          imageCss.Append("<li>");
          imageCss.Append("</ul></div>\r\n");
        }

        imageCss.Append("</body></html>");
        try {
          System.IO.File.WriteAllText(thisFolder + @"\ImageFile.html", imageCss.ToString());
        }
        catch { }
      }



      return Base64Images;

    }


    public static string Base64ImageCalc (string filePath) {
      if (String.IsNullOrEmpty(filePath)) {
        filePath = @"F:\Interface\addtocartTrans.png";
      }

      FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
      byte[] data = new byte[(int)fileStream.Length];
      fileStream.Read(data, 0, data.Length);

      return Convert.ToBase64String(data);
    }


    public class Base64Image {
      public Base64Image () { }
      public Base64Image (string folderName, string fileName, string imageName, string imageData, int imageHeight, int imageWidth) {
        FolderName = folderName;
        FileName = fileName;
        ImageName = imageName;
        ImageData = imageData;
        ImageHeight = imageHeight;
        ImageWidth = imageWidth;
      }

      public string FolderName { get; set; }
      public string FileName { get; set; }
      public string ImageName { get; set; }
      public string ImageData { get; set; }
      public int ImageHeight { get; set; }
      public int ImageWidth { get; set; }
    }


    public static byte[] CropImage (string path, int width, int height, int x, int y, bool scaleToFit) {


      using (Image OriginalImage = Image.FromFile(path)) {

        using (Bitmap bmp = new Bitmap(width, height)) {
          bmp.SetResolution(OriginalImage.HorizontalResolution, OriginalImage.VerticalResolution);

          using (Graphics graphic = Graphics.FromImage(bmp)) {



            graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            graphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphic.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

            if (OriginalImage.Width < 161 && OriginalImage.Height < 191) {


              scaleToFit = true;

              //Get the midpoint of container
              //var imagePositionLeft = 0;
              //var imagePositionTop = 0;

              //var ratioX = width - OriginalImage.Width;
              //var ratioY = height - OriginalImage.Height;


              //imagePositionLeft = (width - OriginalImage.Width) / 2;

              //imagePositionTop = (height - OriginalImage.Height) / 2;



              //Rectangle destination = new Rectangle(imagePositionLeft, imagePositionTop, OriginalImage.Width, OriginalImage.Height);
              //Rectangle source = new Rectangle(0, 0, OriginalImage.Width, OriginalImage.Height);


              //graphic.DrawImage(OriginalImage, destination, source, GraphicsUnit.Pixel);

              //using(Brush brush = new SolidBrush(Color.White)) {
              //graphic.FillRegion(brush, new Region(source));
              //}

            }

            if (scaleToFit) {
              var imagePositionLeft = 0;
              var imagePositionTop = 0;

              var ratioX = (double)width / OriginalImage.Width;
              var ratioY = (double)height / OriginalImage.Height;
              var ratio = Math.Min(ratioX, ratioY);

              var newWidth = (int)(OriginalImage.Width * ratio);
              var newHeight = (int)(OriginalImage.Height * ratio);


              if (newWidth < newHeight) {
                imagePositionLeft = (width - newWidth) / 2;
              } else {
                imagePositionTop = (height - newHeight) / 2;
              }
              graphic.DrawImage(OriginalImage, imagePositionLeft, imagePositionTop, newWidth, newHeight);

            } else {
              graphic.DrawImage(OriginalImage, new Rectangle(0, 0, width, height), x, y, width, height, GraphicsUnit.Pixel);
            }


            MemoryStream ms = new MemoryStream();

            bmp.Save(ms, OriginalImage.RawFormat);
            return ms.GetBuffer();
          }
        }
      }
    }


    public static void DeleteAvatarSource (string path, string accountID) {
      var Tempfiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Select(f => new FileInfo(f)).ToList();
      foreach (var item in Tempfiles) {
        if (item.Name.StartsWith(accountID + "_")) {
          System.IO.File.Delete(item.FullName);
        }
      }
    }


    private static readonly Color[] RandomColor = { Color.Red, Color.Green, Color.Blue, Color.Black, Color.Purple, Color.Orange };
    private static readonly string[] RandomFontFamily = { "arial", "arial black", "comic sans ms", "courier new", "microsoft sans serif", "tahoma", "times new roman", "trebuchet ms", "verdana" };
    private Random _rand;


    private string GetRandomFontFamily () {
      _rand = new Random();
      return RandomFontFamily[_rand.Next(0, RandomFontFamily.Length)];
    }


    public enum FontWarpFactor {
      None,
      Low,
      Medium,
      High,
      Extreme
    }


    public enum BackgroundNoiseLevel {
      None,
      Low,
      Medium,
      High,
      Extreme
    }


    public enum LineNoiseLevel {
      None,
      Low,
      Medium,
      High,
      Extreme
    }


    private Color GetRandomColor () {
      return RandomColor[_rand.Next(0, RandomColor.Length)];
    }


    private GraphicsPath TextPath (string s, Font f, Rectangle r) {
      StringFormat sf = new StringFormat();
      sf.Alignment = StringAlignment.Near;
      sf.LineAlignment = StringAlignment.Near;
      GraphicsPath gp = new GraphicsPath();
      gp.AddString(s, f.FontFamily, (int)f.Style, f.Size, r, sf);
      return gp;
    }


    private void WarpText (GraphicsPath textPath, Rectangle rect, int _width, int _height, FontWarpFactor FontWarp) {
      float WarpDivisor;
      float RangeModifier;

      switch (FontWarp) {
        case FontWarpFactor.None:
          goto default;
        case FontWarpFactor.Low:
          WarpDivisor = 6F;
          RangeModifier = 1F;
          break;
        case FontWarpFactor.Medium:
          WarpDivisor = 5F;
          RangeModifier = 1.3F;
          break;
        case FontWarpFactor.High:
          WarpDivisor = 4.5F;
          RangeModifier = 1.4F;
          break;
        case FontWarpFactor.Extreme:
          WarpDivisor = 4F;
          RangeModifier = 1.5F;
          break;
        default:
          return;
      }

      RectangleF rectF;
      rectF = new RectangleF(Convert.ToSingle(rect.Left), 0, Convert.ToSingle(rect.Width), rect.Height);

      int hrange = Convert.ToInt32(rect.Height / WarpDivisor);
      int wrange = Convert.ToInt32(rect.Width / WarpDivisor);
      int left = rect.Left - Convert.ToInt32(wrange * RangeModifier);
      int top = rect.Top - Convert.ToInt32(hrange * RangeModifier);
      int width = rect.Left + rect.Width + Convert.ToInt32(wrange * RangeModifier);
      int height = rect.Top + rect.Height + Convert.ToInt32(hrange * RangeModifier);

      if (left < 0) {
        left = 0;
      }
      if (top < 0) {
        top = 0;
      }
      if (width > _width) {
        width = _width;
      }
      if (height > _height) {
        height = _height;
      }

      PointF leftTop = RandomPoint(left, left + wrange, top, top + hrange);
      PointF rightTop = RandomPoint(width - wrange, width, top, top + hrange);
      PointF leftBottom = RandomPoint(left, left + wrange, height - hrange, height);
      PointF rightBottom = RandomPoint(width - wrange, width, height - hrange, height);

      PointF[] points = new PointF[] { leftTop, rightTop, leftBottom, rightBottom };
      Matrix m = new Matrix();
      m.Translate(0, 0);
      textPath.Warp(points, rectF, m, WarpMode.Perspective, 0);
    }


    private void AddNoise (Graphics g, Rectangle rect, BackgroundNoiseLevel BackgroundNoise) {
      int density;
      int size;

      switch (BackgroundNoise) {
        case BackgroundNoiseLevel.None:
          goto default;
        case BackgroundNoiseLevel.Low:
          density = 30;
          size = 40;
          break;
        case BackgroundNoiseLevel.Medium:
          density = 18;
          size = 40;
          break;
        case BackgroundNoiseLevel.High:
          density = 16;
          size = 39;
          break;
        case BackgroundNoiseLevel.Extreme:
          density = 12;
          size = 38;
          break;
        default:
          return;
      }

      SolidBrush br = new SolidBrush(GetRandomColor());
      int max = Convert.ToInt32(Math.Max(rect.Width, rect.Height) / size);

      for (int i = 0; i <= Convert.ToInt32((rect.Width * rect.Height) / density); i++)
        g.FillEllipse(br, _rand.Next(rect.Width), _rand.Next(rect.Height), _rand.Next(max), _rand.Next(max));

      br.Dispose();
    }


    private void AddLine (Graphics g, Rectangle rect, LineNoiseLevel lineNoise, int _height) {
      int length;
      float width;
      int linecount;

      switch (lineNoise) {
        case LineNoiseLevel.None:
          goto default;
        case LineNoiseLevel.Low:
          length = 4;
          width = Convert.ToSingle(_height / 31.25);
          linecount = 1;
          break;
        case LineNoiseLevel.Medium:
          length = 5;
          width = Convert.ToSingle(_height / 27.7777);
          linecount = 1;
          break;
        case LineNoiseLevel.High:
          length = 3;
          width = Convert.ToSingle(_height / 25);
          linecount = 2;
          break;
        case LineNoiseLevel.Extreme:
          length = 3;
          width = Convert.ToSingle(_height / 22.7272);
          linecount = 3;
          break;
        default:
          return;
      }

      PointF[] pf = new PointF[length + 1];
      using (Pen p = new Pen(GetRandomColor(), width)) {
        for (int l = 1; l <= linecount; l++) {
          for (int i = 0; i <= length; i++)
            pf[i] = RandomPoint(rect);

          g.DrawCurve(p, pf, 1.75F);
        }
      }
    }


    private PointF RandomPoint (int xmin, int xmax, int ymin, int ymax) {
      return new PointF(_rand.Next(xmin, xmax), _rand.Next(ymin, ymax));
    }

    private PointF RandomPoint (Rectangle rect) {
      return RandomPoint(rect.Left, rect.Width, rect.Top, rect.Bottom);
    }



  }
}
