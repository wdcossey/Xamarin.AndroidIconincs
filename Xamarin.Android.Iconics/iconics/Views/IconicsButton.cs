using System;
using Android.Annotation;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using com.xamarin.AndroidIconics.Typefaces;
using Java.Lang;

namespace com.xamarin.AndroidIconics
{
  internal sealed class IconicsButton : Button
  {
    private IconicsButton(IntPtr javaReference, JniHandleOwnership transfer)
      : base(javaReference, transfer)
    {
    }

    public IconicsButton(Context context)
      : base(context)
    {
      if (!IsInEditMode)
      {
        Typeface = new FontAwesome().GetTypeface(context);
      }
    }

    public IconicsButton(Context context, IAttributeSet attrs)
      : base(context, attrs)
    {
      if (!IsInEditMode)
      {
        Typeface = new FontAwesome().GetTypeface(context);
      }
    }

    public IconicsButton(Context context, IAttributeSet attrs, int defStyleAttr)
      : base(context, attrs, defStyleAttr)
    {
      if (!IsInEditMode)
      {
        Typeface = new FontAwesome().GetTypeface(context);
      }
    }
    public IconicsButton(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes)
      : base(context, attrs, defStyleAttr, defStyleRes)
    {
      if (!IsInEditMode)
      {
        Typeface = new FontAwesome().GetTypeface(context);
      }
    }

    public override void SetText(ICharSequence text, BufferType type)
    {
      base.SetText(!IsInEditMode ? new Iconics.IconicsBuilder().Ctx(Context).On(text).Build() : text, type);
    }
  }
}