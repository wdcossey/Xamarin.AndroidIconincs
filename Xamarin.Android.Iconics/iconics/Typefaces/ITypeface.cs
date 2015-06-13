using System.Collections.Generic;
using Android.Content;
using Java.Lang;

namespace com.xamarin.AndroidIconics.Typefaces
{

  /// <summary>
  /// Created by wdcossey on 08.06.15.
  /// Original by mikepenz on 01.11.14.
  /// </summary>
  public interface ITypeface
  {
    IIcon GetIcon(string key);
    
    Dictionary<string, char> Characters { get; }

    /// <summary>
    /// The Mapping Prefix to identify this font
    /// must have a length of 3
    /// </summary>
    /// <returns>mappingPrefix (length = 3)</returns>
    string MappingPrefix { get; }

    string FontName { get; }

    string Version { get; }

    int IconCount { get; }

    ICollection<string> Icons { get; }

    string Author { get; }

    string Url { get; }

    string Description { get; }

    string License { get; }

    string LicenseUrl { get; }

    global::Android.Graphics.Typeface GetTypeface(Context ctx);
  }
}