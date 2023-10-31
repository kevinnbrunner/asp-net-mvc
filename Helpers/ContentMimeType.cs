using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.Helpers {

  public class ContentMimeType {
    // http://en.wikipedia.org/wiki/Internet_media_type

    public static string MimeType (string Extension) {
      Extension = Extension.ToLower();
      string contentType = "";
      switch (Extension) {
        case "pdf": contentType = ContentMimeType.PDF; break;
        case "txt": contentType = ContentMimeType.Text; break;
        case "png": contentType = ContentMimeType.PNG; break;
        case "jpeg": contentType = ContentMimeType.JPG; break;
        case "jpg": contentType = ContentMimeType.JPG; break;
        case "gif": contentType = ContentMimeType.GIF; break;
        case "svg": contentType = ContentMimeType.SVG; break;
        case "mp3": contentType = ContentMimeType.MP3; break;
        case "ogg": contentType = ContentMimeType.OGG; break;
        case "wav": contentType = ContentMimeType.WAV; break;
        case "avi": contentType = ContentMimeType.AVI; break;
        case "asf": contentType = ContentMimeType.ASF; break;
        case "mp4": contentType = ContentMimeType.MP4; break;
        case "flv": contentType = ContentMimeType.FLV; break;
        case "zip": contentType = ContentMimeType.ZIP; break;
        case "xml": contentType = ContentMimeType.XML; break;
        case "webp": contentType = ContentMimeType.WEBP; break;
        case "webm": contentType = ContentMimeType.WEBM; break;
        case "js": contentType = ContentMimeType.JS; break;
        case "css": contentType = ContentMimeType.CSS; break;
        default: contentType = ContentMimeType.Unknown; break;
      }

      return contentType;
    }

    public const string PDF = "application/pdf";
    public const string Text = "text/plain";
    public const string PNG = "image/png";
    public const string JPG = "image/jpeg";
    public const string GIF = "image/gif";
    public const string SVG = "image/svg+xml";
    public const string MP3 = "audio/mpeg3";
    public const string OGG = "audio/ogg";
    public const string WAV = "audio/vnd.wave";
    public const string AVI = "video/avi";
    public const string ASF = "video/x-ms-asf";
    public const string MP4 = "video/mp4";
    public const string FLV = "video/x-flv";
    public const string ZIP = "application/zip"; // application/x-compressed or application/octet-stream
    public const string XML = "text/xml";
    public const string WEBP = "image/webp";
    public const string WEBM = "video/webm";
    public const string JS = "text/javascript";
    public const string CSS = "text/css";
    public const string Unknown = "application/octet-stream";

  }
}

//text/vcard: vCard (contact information); Defined in RFC 6350
//text/xml: Extensible Markup Language; Defined in RFC 3023
//text/javascript
//text/cmd: commands; subtype resident in Gecko browsers like Firefox 3.5
//text/css: Cascading Style Sheets; Defined in RFC 2318
//text/csv: Comma-separated values; Defined in RFC 4180
//text/html: HTML; Defined in RFC 2854
//image/tiff: Tag Image File Format (only for Baseline TIFF); Defined in RFC 3302
//image/vnd.microsoft.icon: ICO image; Registered[11]
//audio/webm: WebM open media format
//application/postscript: PostScript; Defined in RFC 2046
//application/rdf+xml: Resource Description Framework; Defined by RFC 3870
//application/rss+xml: RSS feeds
//application/soap+xml: SOAP; Defined by RFC 3902
//application/font-woff: Web Open Font Format; (candidate recommendation; use application/x-font-woff until standard is official)
//application/xhtml+xml: XHTML; Defined by RFC 3236
//application/xml-dtd: DTD files; Defined by RFC 3023
//application/xop+xml:XOP
//application/zip: ZIP archive files; Registered[7]
//application/x-gzip: Gzip
//application/atom+xml: Atom feeds
//application/ecmascript: ECMAScript/JavaScript; Defined in RFC 4329 (equivalent to application/javascript but with stricter processing rules)
//application/EDI-X12: EDI X12 data; Defined in RFC 1767
//application/EDIFACT: EDI EDIFACT data; Defined in RFC 1767
//application/json: JavaScript Object Notation JSON; Defined in RFC 4627
//video/quicktime: QuickTime video; Registered[12]
//video/webm: WebM Matroska-based open media format
//video/x-ms-wmv: Windows Media Video; Documented in Microsoft KB 288102

//application/vnd.oasis.opendocument.text: OpenDocument Text; Registered[13]
//application/vnd.oasis.opendocument.spreadsheet: OpenDocument Spreadsheet; Registered[14]
//application/vnd.oasis.opendocument.presentation: OpenDocument Presentation; Registered[15]
//application/vnd.oasis.opendocument.graphics: OpenDocument Graphics; Registered[16]
//application/vnd.ms-excel: Microsoft Excel files
//application/vnd.openxmlformats-officedocument.spreadsheetml.sheet: Microsoft Excel 2007 files
//application/vnd.ms-powerpoint: Microsoft Powerpoint files
//application/vnd.openxmlformats-officedocument.presentationml.presentation: Microsoft Powerpoint 2007 files
//application/vnd.openxmlformats-officedocument.wordprocessingml.document: Microsoft Word 2007 files
//application/vnd.mozilla.xul+xml: Mozilla XUL files
//application/vnd.google-earth.kml+xml: KML files (e.g. for Google Earth)[17]

//application/x-www-form-urlencoded Form Encoded Data; Documented in HTML 4.01 Specification, Section 17.13.4.1
//application/x-dvi: device-independent document in DVI format
//application/x-latex: LaTeX files
//application/x-font-ttf: TrueType Font No registered MIME type, but this is the most commonly used
//application/x-shockwave-flash: Adobe Flash files for example with the extension .swf
//application/x-stuffit: StuffIt archive files
//application/x-rar-compressed: RAR archive files
//application/x-tar: Tarball files
//text/x-gwt-rpc: GoogleWebToolkit data
//text/x-jquery-tmpl: jQuery template data
//application/x-javascript:
//application/x-deb: deb (file format), a software package format used by the Debian project
//audio/x-aac: .aac audio files
//audio/x-caf: Apple's CAF audio files
//application/x-mpegURL: .m3u8 variant playlist
//image/x-xcf: GIMP image file