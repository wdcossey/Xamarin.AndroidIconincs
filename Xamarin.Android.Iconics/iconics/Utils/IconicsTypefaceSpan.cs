using Android.Graphics;
using Android.Text;
using Android.Text.Style;

namespace com.xamarin.AndroidIconics
{
  public class IconicsTypefaceSpan : TypefaceSpan
  {
    private readonly Android.Graphics.Typeface _newType;

    public IconicsTypefaceSpan(string family, Android.Graphics.Typeface type)
      : base(family)
    {
      _newType = type;
    }

    public override void UpdateDrawState(TextPaint ds)
    {
      ApplyCustomTypeFace(ds, _newType);
    }

    public override void UpdateMeasureState(TextPaint paint)
    {
      ApplyCustomTypeFace(paint, _newType);
    }

    private static void ApplyCustomTypeFace(Paint paint, Android.Graphics.Typeface tf)
    {
      var old = paint.Typeface;
      var oldStyle = old == null ? 0 : old.Style;


      var fake = oldStyle & ~tf.Style;

      if ((fake & TypefaceStyle.Bold) != 0)
      {
        paint.FakeBoldText = true;
      }

      if ((fake & TypefaceStyle.Italic) != 0)
      {
        paint.TextSkewX = -0.25f;
      }

      paint.SetTypeface(tf);
    }
  }
}