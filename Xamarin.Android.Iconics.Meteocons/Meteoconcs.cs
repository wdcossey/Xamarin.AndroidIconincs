using System;
using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using com.xamarin.AndroidIconics.Typefaces;

namespace com.xamarin.AndroidIconics
{
  public class Meteoconcs : ITypeface
  {
    private const string TTF_FILE = "meteocons.ttf";

    private static Typeface _typeface;

    private static Dictionary<string, char> _chars;

    public IIcon GetIcon(string key)
    {
      return new MeteoconcsIcon((Icon) Enum.Parse(typeof (Icon), key, true));
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
      get { return "met"; }
    }

    public string FontName
    {
      get { return "Meteocons"; }
    }

    public string Version
    {
      get { return "1.1.1"; }
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
      get { return "Alessio Atzeni"; }
    }

    public string Url
    {
      get { return "http://www.alessioatzeni.com/meteocons/"; }
    }

    public string Description
    {
      get
      {
        return
          "Meteocons is a set of weather icons, it containing 40+ icons available in PSD, CSH, EPS, SVG, Desktop font and Web font. All icon and updates are free and always will be.";
      }
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
      met_windy_rain_inv = '\ue800',
      met_snow_inv = '\ue801',
      met_snow_heavy_inv = '\ue802',
      met_hail_inv = '\ue803',
      met_clouds_inv = '\ue804',
      met_clouds_flash_inv = '\ue805',
      met_temperature = '\ue806',
      met_compass = '\ue807',
      met_na = '\ue808',
      met_celcius = '\ue809',
      met_fahrenheit = '\ue80a',
      met_clouds_flash_alt = '\ue80b',
      met_sun_inv = '\ue80c',
      met_moon_inv = '\ue80d',
      met_cloud_sun_inv = '\ue80e',
      met_cloud_moon_inv = '\ue80f',
      met_cloud_inv = '\ue810',
      met_cloud_flash_inv = '\ue811',
      met_drizzle_inv = '\ue812',
      met_rain_inv = '\ue813',
      met_windy_inv = '\ue814',
      met_sunrise = '\ue815',
      met_sun = '\ue816',
      met_moon = '\ue817',
      met_eclipse = '\ue818',
      met_mist = '\ue819',
      met_wind = '\ue81a',
      met_snowflake = '\ue81b',
      met_cloud_sun = '\ue81c',
      met_cloud_moon = '\ue81d',
      met_fog_sun = '\ue81e',
      met_fog_moon = '\ue81f',
      met_fog_cloud = '\ue820',
      met_fog = '\ue821',
      met_cloud = '\ue822',
      met_cloud_flash = '\ue823',
      met_cloud_flash_alt = '\ue824',
      met_drizzle = '\ue825',
      met_rain = '\ue826',
      met_windy = '\ue827',
      met_windy_rain = '\ue828',
      met_snow = '\ue829',
      met_snow_alt = '\ue82a',
      met_snow_heavy = '\ue82b',
      met_hail = '\ue82c',
      met_clouds = '\ue82d',
      met_clouds_flash = '\ue82e'
      // ReSharper enable InconsistentNaming
    }

    private class MeteoconcsIcon : IIcon
    {
      private readonly Icon _icon;

      protected internal MeteoconcsIcon(Icon icon)
      {
        _icon = icon;
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
        get { return _iconTypeface ?? (_iconTypeface = new Meteoconcs()); }
      }
    }
  }
}