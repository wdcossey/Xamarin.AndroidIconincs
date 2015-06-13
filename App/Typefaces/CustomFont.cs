using System;
using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using com.xamarin.AndroidIconics.Typefaces;


namespace App.Typefaces
{
  /// <summary>
  /// Created by wdcossey on 08.06.15.
  /// Original by mikepenz on 01.11.14.
  /// </summary>
  internal class CustomFont : ITypeface
  {

    private const string TTF_FILE = "fontello.ttf";

    private static Typeface _typeface;

    private static Dictionary<string, char> _chars;

    public static IIcon GetIcon(string key)
    {
      return new CustomFontIcon((Icon) Enum.Parse(typeof (Icon), key, true));
    }

    IIcon ITypeface.GetIcon(string key)
    {
      return GetIcon(key);
    }

    public Dictionary<string, char> Characters
    {
      get
      {
        if (_chars == null)
        {
          _chars = new Dictionary<string, char>();

          foreach (var name in Enum.GetNames(typeof (Icon)))
          {
            var value = (Icon) Enum.Parse(typeof (Icon), name);
            _chars.Add(name, (char) value);
          }
        }

        return _chars;
      }
    }

    public string MappingPrefix
    {
      get { return "fon"; }
    }


    public string FontName
    {
      get { return "CustomFont"; }
    }


    public string Version
    {
      get { return ""; }
    }

    public int IconCount
    {
      get { return _chars.Count; }
    }

    public ICollection<string> Icons
    {
      get { return Enum.GetNames(typeof (Icon)); }
    }

    public string Author
    {
      get { return ""; }
    }

    public string Url
    {
      get { return ""; }
    }

    public string Description
    {
      get { return ""; }
    }

    public string License
    {
      get { return ""; }
    }

    public string LicenseUrl
    {
      get { return ""; }
    }

    public Typeface GetTypeface(Context context)
    {
      if (_typeface == null)
      {
        try
        {
          _typeface = Typeface.CreateFromAsset(context.Assets, /*"fonts/" +*/ TTF_FILE);
        }
        catch (Exception)
        {
          return null;
        }
      }
      return _typeface;
    }

    public enum Icon
    {
      // ReSharper disable InconsistentNaming
      fon_test1 = '\ue800',
      fon_test2 = '\ue801'
      // ReSharper restore InconsistentNaming
    }


    public class CustomFontIcon : IIcon
    {
      private readonly Icon _icon;
      
      protected internal CustomFontIcon(Icon icon)
      {
        _icon = icon;
      }

      public static CustomFontIcon GetIcon(Icon icon)
      {
        return new CustomFontIcon(icon);
      }

      public string GetFormattedName
      {
        get { return string.Format("{{{0}}}", _icon); }
      }

      public char GetCharacter
      {
        get { return (char) _icon; }
      }

      public string GetName
      {
        get { return _icon.ToString().ToUpperInvariant(); }
      }

      // remember the typeface so we can use it later
      private ITypeface _iconTypeface;

      public ITypeface GetTypeface
      {
        get { return _iconTypeface ?? (_iconTypeface = new CustomFont()); }
      }
    }
  }
}