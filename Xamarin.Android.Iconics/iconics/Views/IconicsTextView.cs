using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using Java.Lang;

namespace com.xamarin.AndroidIconics
{
  public sealed class IconicsTextView : TextView
  {

    private IconicsTextView(IntPtr javaReference, JniHandleOwnership transfer)
      : base(javaReference, transfer)
    {

    }

    public IconicsTextView(Context context)
      : base(context, null)
    {

    }

    public IconicsTextView(Context context, IAttributeSet attrs)
      : base(context, attrs)
    {

    }

    public IconicsTextView(Context context, IAttributeSet attrs, int defStyle)
      : base(context, attrs, defStyle)
    {

    }

    public IconicsTextView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes)
      : base(context, attrs, defStyleAttr, defStyleRes)
    {
      
    }

    public override void SetText(ICharSequence text, BufferType type)
    {
      base.SetText(!IsInEditMode ? new Iconics.IconicsBuilder().Ctx(Context).On(text).Build() : text, type);
    }
  }
}