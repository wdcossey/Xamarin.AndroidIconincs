using System;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using com.xamarin.AndroidIconics.Typefaces;

namespace com.xamarin.AndroidIconics
{
  public sealed class IconicsImageView : ImageView
  {

    private IconicsDrawable mIcon = null;
    private Color mColor = Color.Transparent;

    private IconicsImageView(IntPtr javaReference, JniHandleOwnership transfer)
      : base(javaReference, transfer)
    {

    }

    public IconicsImageView(Context context)
      : this(context, null)
    {

    }

    public IconicsImageView(Context context, IAttributeSet attrs)
      : this(context, attrs, 0)
    {

    }

    public IconicsImageView(Context context, IAttributeSet attrs, int defStyle)
      : base(context, attrs, defStyle)
    {

      if (!IsInEditMode)
      {
        // Attribute initialization
        var a = context.ObtainStyledAttributes(attrs, Resource.Styleable.IconicsImageView, defStyle, 0);
        var icon = a.GetString(Resource.Styleable.IconicsImageView_iiv_icon);
        if (icon == null)
        {
          return;
        }
        mColor = a.GetColor(Resource.Styleable.IconicsImageView_iiv_color, 0);

        //get the drawable
        mIcon = new IconicsDrawable(context, icon);
        if (mColor != 0)
        {
          mIcon.Color(mColor);
        }

        a.Recycle();

        //set our values for this view
        SetImageDrawable(mIcon);
        SetScaleType(ScaleType.Matrix);
      }
    }

    public void SetIcon(string icon)
    {
      SetIcon(new IconicsDrawable(Context, icon));
    }

    public void SetIcon(IIcon icon)
    {
      SetIcon(new IconicsDrawable(Context, icon));
    }

    public void SetIcon(IconicsDrawable icon)
    {
      if (mColor != 0)
      {
        icon.Color(mColor);
      }
      mIcon = icon;
      SetImageDrawable(mIcon);
    }

    public void SetColor(Color color)
    {
      if (Drawable is IconicsDrawable)
      {
        ((IconicsDrawable) Drawable).Color(color);
      }
    }

    public void SetColorRes(int colorRes)
    {
      if (Drawable is IconicsDrawable)
      {
        ((IconicsDrawable) Drawable).ColorRes(colorRes);
      }
    }

    public IconicsDrawable GetIcon()
    {
      if (Drawable is IconicsDrawable)
      {
        return ((IconicsDrawable) Drawable);
      }
      return null;
    }


    protected override void OnSizeChanged(int w, int h, int oldW, int oldH)
    {
      base.OnSizeChanged(w, h, oldW, oldH);

      if (Drawable is IconicsDrawable)
      {
        //set the size
        ((IconicsDrawable) Drawable).SizePx(w > h ? w : h);
      }
    }
  }
}