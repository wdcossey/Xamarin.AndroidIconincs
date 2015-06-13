using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Text;
using Android.Text.Style;
using Android.Util;
using Android.Widget;
using com.xamarin.AndroidIconics.Typefaces;
using Java.Lang;
using String = Java.Lang.String;

namespace com.xamarin.AndroidIconics
{
  public static class Iconics
  {
    private static readonly string Tag = typeof (Iconics).Name;

    static readonly FontAwesome Fa = new FontAwesome();
    static readonly GoogleMaterial Gm = new GoogleMaterial();

    private static readonly Dictionary<string, ITypeface> Fonts = new Dictionary<string, ITypeface>
    {
      {
        Fa.MappingPrefix,  Fa
      },
      {
        Gm.MappingPrefix,  Gm
      },
    };

    //ADD DEFAULT to fontList
    //static {
    //    FontAwesome fa = new FontAwesome();
    //    FONTS.put(fa.getMappingPrefix(), fa);
    //    GoogleMaterial gm = new GoogleMaterial();
    //    FONTS.put(gm.getMappingPrefix(), gm);
    //}

    public static void RegisterFont(ITypeface font)
    {
      if (!Fonts.ContainsKey(font.MappingPrefix))
        Fonts.Add(font.MappingPrefix, font);
    }

    public static IList<ITypeface> RegisteredFonts
    {
      get
      {
        return Fonts.Values.ToList();
      }
    }

    public static ITypeface FindFont(string key)
    {
      return Fonts[key];
    }

    public static ITypeface FindFont(IIcon icon)
    {
      return icon.GetTypeface;
    }

    //private static Iconics()
    //{
    //  // Prevent instantiation
    //}

    private static SpannableString Style(Context ctx, Dictionary<string, ITypeface> fonts, SpannableString textSpanned,
      List<CharacterStyle> styles, Dictionary<string, List<CharacterStyle>> stylesFor)
    {
      if (fonts == null || !fonts.Any())
      {
        fonts = Fonts;
      }

      var startIndex = -1;
      var fontKey = string.Empty;

      //remember the position of removed chars
      var removed = new List<RemoveInfo>();

      //StringBuilder text = new StringBuilder(textSpanned.toString());
      var text = new StringBuilder(textSpanned);

      //find the first "{"
      while ((startIndex = text.IndexOf("{", startIndex + 1)) != -1)
      {
        //make sure we are still within the bounds of the text
        if (text.Length() < startIndex + 5)
        {
          startIndex = -1;
          break;
        }

        //make sure the found text is a real fontKey
        if (!text.Substring(startIndex + 4, startIndex + 5).Equals("-"))
        {
          break;
        }

        //get the fontKey
        fontKey = text.Substring(startIndex + 1, startIndex + 4);

        //check if the fontKey is a registeredFont
        if (fonts.ContainsKey(fontKey))
        {
          break;
        }
      }

      if (startIndex == -1)
      {
        return new SpannableString(text);
      }

      //remember total removed chars
      var removedChars = 0;

      var styleContainers = new List<StyleContainer>();
      do
      {
        //get the information from the iconString
        var endIndex = text.Substring(startIndex).IndexOf("}", StringComparison.Ordinal) + startIndex + 1;
        var iconString = text.Substring(startIndex + 1, endIndex - 1);
        iconString = iconString.Replace("-", "_");
        try
        {
          //get the correct character for this Font and Icon
          var icon = fonts[fontKey].GetIcon(iconString);

          //we can only add an icon which is a font
          if (icon != null)
          {
            char fontChar = icon.GetCharacter;
            var iconValue = String.ValueOf(fontChar);

            //get just the icon identifier
            text = text.Replace(startIndex, endIndex, iconValue);

            //store some info about the removed chars
            removedChars = removedChars + (endIndex - startIndex);
            removed.Add(new RemoveInfo(startIndex, (endIndex - startIndex - 1), removedChars));

            //add the current icon to the container
            styleContainers.Add(new StyleContainer(startIndex, startIndex + 1, iconString, fonts[fontKey]));
          }
        }
        catch (IllegalArgumentException)
        {
          Log.Warn(Tag, "Wrong icon name: " + iconString);
        }

        //reset fontKey so we can react if we are at the end but haven't found any more matches
        fontKey = null;

        //check the rest of the text for matches
        while ((startIndex = text.IndexOf("{", startIndex + 1)) != -1)
        {
          //make sure we are still within the bounds
          if (text.Length() < startIndex + 5)
          {
            startIndex = -1;
            break;
          }
          //check if the 5. char is a "-"
          if (text.Substring(startIndex + 4, startIndex + 5).Equals("-"))
          {
            //get the fontKey
            fontKey = text.Substring(startIndex + 1, startIndex + 4);
            //check if the fontKey is registered
            if (fonts.ContainsKey(fontKey))
            {
              break;
            }
          }
        }
      } while (startIndex != -1 && fontKey != null);

      var sb = new SpannableString(text);


      //reapply all previous styles
      foreach (var span in textSpanned.GetSpans(0, textSpanned.Length(), Class.FromType(typeof (StyleSpan))))
      {
        int spanStart = NewSpanPoint(textSpanned.GetSpanStart(span), removed);
        int spanEnd = NewSpanPoint(textSpanned.GetSpanEnd(span), removed);
        if (spanStart >= 0 && spanEnd > 0)
        {
          sb.SetSpan(span, spanStart, spanEnd, textSpanned.GetSpanFlags(span));
        }
      }




      //set all the icons and styles
      foreach (StyleContainer styleContainer in styleContainers)
      {
        sb.SetSpan(new IconicsTypefaceSpan("sans-serif", styleContainer.GetFont().GetTypeface(ctx)),
          styleContainer.GetStartIndex(), styleContainer.GetEndIndex(), SpanTypes.ExclusiveExclusive);

        if (stylesFor.ContainsKey(styleContainer.GetIcon()))
        {
          foreach (CharacterStyle style in stylesFor[styleContainer.GetIcon()])
          {
            sb.SetSpan(CharacterStyle.Wrap(style), styleContainer.GetStartIndex(), styleContainer.GetEndIndex(),
              SpanTypes.ExclusiveExclusive);
          }
        }
        else if (styles != null)
        {
          foreach (CharacterStyle style in styles)
          {
            sb.SetSpan(CharacterStyle.Wrap(style), styleContainer.GetStartIndex(), styleContainer.GetEndIndex(),
              SpanTypes.ExclusiveExclusive);
          }
        }
      }

      //sb = applyKerning(sb, 1);

      return sb;
    }

    private static int NewSpanPoint(int pos, IList<RemoveInfo> removed)
    {
      foreach (var removeInfo in removed)
      {
        if (pos < removeInfo.GetStart())
        {
          return pos;
        }

        pos = pos - removeInfo.GetCount();
      }
      return pos;
    }

    private static int DetermineNewSpanPoint(int pos, IList<RemoveInfo> removed)
    {
      foreach (var removeInfo in removed)
      {
        if (pos > removeInfo.GetStart())
        {
          continue;
        }

        if (pos > removeInfo.GetStart() && pos < removeInfo.GetStart() + removeInfo.GetCount())
        {
          return -1;
        }

        if (pos < removeInfo.GetStart())
        {
          return pos;
        }
        else
        {
          return pos - removeInfo.GetTotal();
        }
      }

      return -1;
    }

    /*
    KEEP THIS HERE perhaps we are able to implement proper spacing for the icons

    public static SpannableString applyKerning(CharSequence src, float kerning) {
        if (src == null) return null;
        final int srcLength = src.length();
        if (srcLength < 2) return src instanceof SpannableString
                ? (SpannableString) src
                : new SpannableString(src);

        final String nonBreakingSpace = "\u00A0";
        final SpannableStringBuilder builder = src instanceof SpannableStringBuilder
                ? (SpannableStringBuilder) src
                : new SpannableStringBuilder(src);
        for (int i = src.length() - 1; i >= 1; i--) {
            builder.insert(i, nonBreakingSpace);
            builder.setSpan(new ScaleXSpan(kerning), i, i + 1,
                    Spanned.SPAN_EXCLUSIVE_EXCLUSIVE);
        }

        return new SpannableString(builder);
    }
    */

    public class IconicsBuilderString
    {
      private readonly Context _ctx;
      private readonly SpannableString _text;
      private readonly List<CharacterStyle> _withStyles;
      private readonly Dictionary<string, List<CharacterStyle>> _withStylesFor;
      private readonly List<ITypeface> _fonts;

      public IconicsBuilderString(Context ctx, List<ITypeface> fonts, SpannableString text, List<CharacterStyle> styles,
        Dictionary<string, List<CharacterStyle>> stylesFor)
      {
        _ctx = ctx;
        _fonts = fonts;
        _text = text;
        _withStyles = styles;
        _withStylesFor = stylesFor;
      }

      public SpannableString Build()
      {
        var mappedFonts = _fonts.ToDictionary(font => font.MappingPrefix);

        return Style(_ctx, mappedFonts, _text, _withStyles, _withStylesFor);
      }
    }

    public class IconicsBuilderView
    {
      private readonly Context _ctx;
      private readonly TextView _view;
      private readonly List<CharacterStyle> _withStyles;
      private readonly Dictionary<string, List<CharacterStyle>> _withStylesFor;
      private readonly List<ITypeface> _fonts;

      public IconicsBuilderView(Context ctx, List<ITypeface> fonts, TextView view, List<CharacterStyle> styles,
        Dictionary<string, List<CharacterStyle>> stylesFor)
      {
        _ctx = ctx;
        _fonts = fonts;
        _view = view;
        _withStyles = styles;
        _withStylesFor = stylesFor;
      }


      public void Build()
      {
        var mappedFonts = _fonts.ToDictionary(font => font.MappingPrefix);

        if (_view.TextFormatted is SpannableString)
        {
          _view.TextFormatted = Style(_ctx, mappedFonts, (SpannableString) _view.Text, _withStyles, _withStylesFor);
        }
        else
        {
          _view.TextFormatted = Style(_ctx, mappedFonts, new SpannableString(_view.Text), _withStyles, _withStylesFor);
        }
      }
    }

    public class IconicsBuilder
    {
      private readonly List<CharacterStyle> _styles = new List<CharacterStyle>();

      private readonly Dictionary<string, List<CharacterStyle>> _stylesFor =
        new Dictionary<string, List<CharacterStyle>>();

      private readonly List<ITypeface> _fonts = new List<ITypeface>();
      private Context _ctx;

      public IconicsBuilder()
      {
      }

      public IconicsBuilder Ctx(Context ctx)
      {
        _ctx = ctx;
        return this;
      }


      public IconicsBuilder Style(params CharacterStyle[] styles)
      {
        if (styles != null && styles.Length > 0)
        {
          _styles.AddRange(styles);
        }
        return this;
      }

      public IconicsBuilder StyleFor(IIcon styleFor, params CharacterStyle[] styles)
      {
        return StyleFor(styleFor.GetName, styles);
      }

      public IconicsBuilder StyleFor(string styleFor, params CharacterStyle[] styles)
      {
        styleFor = styleFor.Replace("-", "_");

        if (!_stylesFor.ContainsKey(styleFor))
        {
          this._stylesFor.Add(styleFor, new List<CharacterStyle>());
        }

        if (styles != null && styles.Length > 0)
        {
          foreach (var style in styles)
          {
            this._stylesFor[styleFor].Add(style);
          }
        }
        return this;
      }

      public IconicsBuilder Font(ITypeface font)
      {
        _fonts.Add(font);
        return this;
      }


      public IconicsBuilderString On(SpannableString on)
      {
        return new IconicsBuilderString(_ctx, _fonts, on, _styles, _stylesFor);
      }

      public IconicsBuilderString On(string on)
      {
        return On(new SpannableString(on));
      }

      public IconicsBuilderString On(ICharSequence on)
      {
        return On(on.ToString());
      }

      public IconicsBuilderString On(StringBuilder on)
      {
        return On(on.ToString());
      }

      public IconicsBuilderView On(TextView on)
      {
        return new IconicsBuilderView(_ctx, _fonts, on, _styles, _stylesFor);
      }

      public IconicsBuilderView On(Button on)
      {
        return new IconicsBuilderView(_ctx, _fonts, on, _styles, _stylesFor);
      }
    }

    private class StyleContainer
    {
      private readonly int _startIndex;
      private readonly int _endIndex;
      private readonly string _icon;
      private readonly ITypeface _font;

      internal StyleContainer(int startIndex, int endIndex, string icon, ITypeface font)
      {
        _startIndex = startIndex;
        _endIndex = endIndex;
        _icon = icon;
        _font = font;
      }

      public int GetStartIndex()
      {
        return _startIndex;
      }

      public int GetEndIndex()
      {
        return _endIndex;
      }

      public string GetIcon()
      {
        return _icon;
      }

      public ITypeface GetFont()
      {
        return _font;
      }
    }

    private class RemoveInfo
    {
      private int _start;
      private int _count;
      private int _total;

      internal RemoveInfo(int start, int count)
      {
        _start = start;
        _count = count;
      }

      public RemoveInfo(int start, int count, int total)
      {
        _start = start;
        _count = count;
        _total = total;
      }

      public int GetStart()
      {
        return _start;
      }

      public void SetStart(int start)
      {
        _start = start;
      }

      public int GetCount()
      {
        return _count;
      }

      public void SetCount(int count)
      {
        _count = count;
      }

      public int GetTotal()
      {
        return _total;
      }

      public void SetTotal(int total)
      {
        _total = total;
      }
    }
  }
}