using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using com.xamarin.AndroidIconics.Typefaces;
using Java.Lang;
using Enum = System.Enum;

namespace com.xamarin.AndroidIconics
{

  /// <summary>
  /// A custom {@link Drawable} which can display icons from icon fonts.
  /// </summary>
  public class IconicsDrawable : Drawable
  {
    private const int ANDROID_ACTIONBAR_ICON_SIZE_DP = 24;
    private const int ANDROID_ACTIONBAR_ICON_SIZE_PADDING_DP = 6;

    protected Context _context;

    private int _size = -1;

    private Paint _iconPaint;
    private Paint _contourPaint;

    private Color _backgroundColor = Android.Graphics.Color.Transparent;

    private Rect _paddingBounds;
    private RectF _pathBounds;
     
    private Path _path;

    private int _iconPadding;
    private int _contourWidth;

    private int _iconOffsetX = 0;
    private int _iconOffsetY = 0;

    private int _alpha = 255;

    private bool _drawContour;

    private IIcon _icon;
    private char _plainIcon;

		protected internal IconicsDrawable()
		{
		}

    //public IconicsDrawable(Context context, char icon)
    //{
    //  _context = context.ApplicationContext;
    //  Prepare();
    //  Icon(icon);
    //}

    public IconicsDrawable(Context context, string icon)
    {
      _context = context.ApplicationContext;
      Prepare();

      var font = Iconics.FindFont(icon.Substring(0, 3));
      icon = icon.Replace("-", "_");
      Icon(font.GetIcon(icon));
    }

    public IconicsDrawable(Context context, IIcon icon)
    {
      _context = context.ApplicationContext;
      Prepare();
      Icon(icon);
    }

    public IconicsDrawable(Context context, ITypeface typeface, IIcon icon)
    {
      _context = context.ApplicationContext;
      Prepare();
      Icon(typeface, icon);
    }

    public IconicsDrawable(Context context, ITypeface typeface, System.Enum icon)
    {
      _context = context.ApplicationContext;
      Prepare();
      Icon(typeface, typeface.GetIcon(icon.ToString()));
    }

    internal void Prepare()
    {
      _iconPaint = new Paint(PaintFlags.AntiAlias);

      _contourPaint = new Paint(PaintFlags.AntiAlias);
      _contourPaint.SetStyle(Paint.Style.Stroke);

      _path = new Path();

      _pathBounds = new RectF();
      _paddingBounds = new Rect();
    }

    /// <summary>
    /// Loads and draws given.
    /// </summary>
    /// <param name="icon"></param>
    /// <returns>The current IconExtDrawable for chaining.</returns>
    //public IconicsDrawable Icon(char icon)
    //{
    //  _plainIcon = icon;
    //  _iconPaint.SetTypeface(Android.Graphics.Typeface.Default);
    //  InvalidateSelf();
    //  return this;
    //}

    /// <summary>
    /// Loads and draws given.
    /// </summary>
    /// <param name="icon"></param>
    /// <returns>The current IconExtDrawable for chaining.</returns>
    public IconicsDrawable Icon(IIcon icon)
    {
      _icon = icon;

      ITypeface typeface = icon.GetTypeface;
      _iconPaint.SetTypeface(typeface.GetTypeface(_context));
      InvalidateSelf();
      return this;
    }

    /// <summary>
    /// Loads and draws given.
    /// </summary>
    /// <param name="typeface"></param>
    /// <param name="icon"></param>
    /// <returns>The current IconExtDrawable for chaining.</returns>
    public IconicsDrawable Icon(ITypeface typeface, IIcon icon)
    {
      _icon = icon;
      _iconPaint.SetTypeface(typeface.GetTypeface(_context));
      InvalidateSelf();
      return this;
    }

    /// <summary>
    /// Set the color of the drawable.
    /// </summary>
    /// <param name="color">The color, usually from android.graphics.Color or 0xFF012345.</param>
    /// <returns>The current IconExtDrawable for chaining.</returns>
    public IconicsDrawable Color(Color color)
    {
      var red = Android.Graphics.Color.GetRedComponent(color);
      var green = Android.Graphics.Color.GetGreenComponent(color);
      var blue = Android.Graphics.Color.GetBlueComponent(color);
      _iconPaint.Color = Android.Graphics.Color.Rgb(red, green, blue);
      SetAlpha(Android.Graphics.Color.GetAlphaComponent(color));
      InvalidateSelf();
      return this;
    }

/*    
    public int AdjustAlpha(int color, float factor) 
    {
        var alpha = Math.Round(global::Android.Graphics.Color.GetAlphaComponent(color));
        var red = global::Android.Graphics.Color.GetRedComponent(color);
        var green = global::Android.Graphics.Color.GetGreenComponent(color);
        var blue = global::Android.Graphics.Color.GetBlueComponent(color);
        return global::Android.Graphics.Color.Argb(alpha, red, green, blue);
    }
*/

    /// <summary>
    /// Set the color of the drawable.
    /// </summary>
    /// <param name="colorRes">The color resource, from your R file.</param>
    /// <returns>The current IconExtDrawable for chaining.</returns>
    public IconicsDrawable ColorRes(int colorRes)
    {
      return Color(_context.Resources.GetColor(colorRes));
    }


    /// <summary>
    /// set the icon offset for X from resource
    /// </summary>
    /// <param name="iconOffsetXRes"></param>
    /// <returns></returns>
    public IconicsDrawable IconOffsetXRes(int iconOffsetXRes)
    {
      return IconOffsetXPx(_context.Resources.GetDimensionPixelSize(iconOffsetXRes));
    }

    /// <summary>
    /// set the icon offset for X as dp
    /// </summary>
    /// <param name="iconOffsetXDp"></param>
    /// <returns></returns>
    public IconicsDrawable IconOffsetXDp(int iconOffsetXDp)
    {
      return IconOffsetXPx(_context.ConvertDpToPx(iconOffsetXDp));
    }

    /// <summary>
    /// set the icon offset for X
    /// </summary>
    /// <param name="iconOffsetX"></param>
    /// <returns></returns>
    public IconicsDrawable IconOffsetXPx(int iconOffsetX)
    {
      _iconOffsetX = iconOffsetX;
      return this;
    }

    /// <summary>
    /// set the icon offset for Y from resource
    /// </summary>
    /// <param name="iconOffsetYRes"></param>
    /// <returns></returns>
    public IconicsDrawable IconOffsetYRes(int iconOffsetYRes)
    {
      return IconOffsetYPx(_context.Resources.GetDimensionPixelSize(iconOffsetYRes));
    }

    /// <summary>
    /// set the icon offset for Y as dp
    /// </summary>
    /// <param name="iconOffsetYDp"></param>
    /// <returns></returns>
    public IconicsDrawable IconOffsetYDp(int iconOffsetYDp)
    {
      return IconOffsetYPx(_context.ConvertDpToPx(iconOffsetYDp));
    }

    /// <summary>
    /// set the icon offset for Y
    /// </summary>
    /// <param name="iconOffsetY"></param>
    /// <returns></returns>
    public IconicsDrawable IconOffsetYPx(int iconOffsetY)
    {
      _iconOffsetY = iconOffsetY;
      return this;
    }

    /// <summary>
    /// Set the padding of the drawable from res
    /// </summary>
    /// <param name="dimenRes"></param>
    /// <returns>The current IconExtDrawable for chaining.</returns>
    public IconicsDrawable PaddingRes(int dimenRes)
    {
      return PaddingPx(_context.Resources.GetDimensionPixelSize(dimenRes));
    }

    /// <summary>
    /// Set the padding in dp for the drawable
    /// </summary>
    /// <param name="iconPadding"></param>
    /// <returns>The current IconExtDrawable for chaining.</returns>
    public IconicsDrawable PaddingDp(int iconPadding)
    {
      return PaddingPx(_context.ConvertDpToPx(iconPadding));
    }

    /// <summary>
    /// Set a padding for the.
    /// </summary>
    /// <param name="iconPadding"></param>
    /// <returns>The current IconExtDrawable for chaining.</returns>
    public IconicsDrawable PaddingPx(int iconPadding)
    {
      if (_iconPadding != iconPadding)
      {
        _iconPadding = iconPadding;
        if (_drawContour)
        {
          _iconPadding += _contourWidth;
        }

        InvalidateSelf();
      }
      return this;
    }

    /// <summary>
    /// Set the size of this icon to the standard Android ActionBar.
    /// </summary>
    /// <returns>The current IconExtDrawable for chaining.</returns>
    public IconicsDrawable ActionBarSize()
    {
      return SizeDp(ANDROID_ACTIONBAR_ICON_SIZE_DP);
    }

    /// <summary>
    /// Sets the size and the Padding to the correct values to be used for the actionBar / toolBar
    /// </summary>
    /// <returns></returns>
    public IconicsDrawable ActionBar()
    {
      SizeDp(ANDROID_ACTIONBAR_ICON_SIZE_DP + (2*ANDROID_ACTIONBAR_ICON_SIZE_PADDING_DP));
      PaddingDp(ANDROID_ACTIONBAR_ICON_SIZE_PADDING_DP);
      return this;
    }


    /// <summary>
    /// Set the size of the drawable.
    /// </summary>
    /// <param name="dimenRes">The dimension resource.</param>
    /// <returns>The current IconExtDrawable for chaining.</returns>
    public IconicsDrawable SizeRes(int dimenRes)
    {
      return SizePx(_context.Resources.GetDimensionPixelSize(dimenRes));
    }

    /// <summary>
    /// Set the size of the drawable.
    /// </summary>
    /// <param name="size">The size in density-independent pixels (dp).</param>
    /// <returns>The current IconExtDrawable for chaining.</returns>
    public IconicsDrawable SizeDp(int size)
    {
      return SizePx(_context.ConvertDpToPx(size));
    }

    /// <summary>
    /// Set the size of the drawable.
    /// </summary>
    /// <param name="size">The size in pixels (px).</param>
    /// <returns>The current IconExtDrawable for chaining.</returns>
    public IconicsDrawable SizePx(int size)
    {
      _size = size;
      SetBounds(0, 0, size, size);
      InvalidateSelf();
      return this;
    }

    /// <summary>
    /// Set contour color for the.
    /// </summary>
    /// <param name="contourColor"></param>
    /// <returns>The current IconExtDrawable for chaining.</returns>
    public IconicsDrawable ContourColor(Color contourColor)
    {
      _contourPaint.Color = contourColor;
      DrawContour(true);
      InvalidateSelf();
      return this;
    }

    /// <summary>
    /// Set contour color from color res.
    /// </summary>
    /// <param name="contourColorRes"></param>
    /// <returns>The current IconExtDrawable for chaining.</returns>
    public IconicsDrawable ContourColorRes(int contourColorRes)
    {
      _contourPaint.Color = _context.Resources.GetColor(contourColorRes);
      DrawContour(true);
      InvalidateSelf();
      return this;
    }

    /// <summary>
    /// set background color
    /// </summary>
    /// <param name="backgroundColor"></param>
    /// <returns></returns>
    public IconicsDrawable BackgroundColor(Color backgroundColor)
    {
      _backgroundColor = backgroundColor;
      return this;
    }

    /// <summary>
    /// set background color from res
    /// </summary>
    /// <param name="backgroundColorRes"></param>
    /// <returns></returns>
    public IconicsDrawable BackgroundColorRes(int backgroundColorRes)
    {
      _backgroundColor = _context.Resources.GetColor(backgroundColorRes);
      return this;
    }

    /// <summary>
    /// Set contour width from an dimen res for the icon
    /// </summary>
    /// <param name="contourWidthRes"></param>
    /// <returns>The current IconExtDrawable for chaining.</returns>
    public IconicsDrawable ContourWidthRes(int contourWidthRes)
    {
      return ContourWidthPx(_context.Resources.GetDimensionPixelSize(contourWidthRes));
    }

    /// <summary>
    /// Set contour width from dp for the icon
    /// </summary>
    /// <param name="contourWidthDp"></param>
    /// <returns>The current IconExtDrawable for chaining.</returns>
    public IconicsDrawable ContourWidthDp(int contourWidthDp)
    {
      return ContourWidthPx(_context.ConvertDpToPx(contourWidthDp));
    }

    /// <summary>
    /// Set contour width for the icon.
    /// </summary>
    /// <param name="contourWidth"></param>
    /// <returns>The current IconExtDrawable for chaining.</returns>
    public IconicsDrawable ContourWidthPx(int contourWidth)
    {
      _contourWidth = contourWidth;
      _contourPaint.StrokeWidth = _contourWidth;
      DrawContour(true);
      InvalidateSelf();
      return this;
    }

    /// <summary>
    /// Enable/disable contour drawing.
    /// </summary>
    /// <param name="drawContour"></param>
    /// <returns>The current IconExtDrawable for chaining.</returns>
    public IconicsDrawable DrawContour(bool drawContour)
    {
      if (_drawContour != drawContour)
      {
        _drawContour = drawContour;

        if (_drawContour)
        {
          _iconPadding += _contourWidth;
        }
        else
        {
          _iconPadding -= _contourWidth;
        }

        InvalidateSelf();
      }
      return this;
    }

    /// <summary>
    /// Set the colorFilter
    /// </summary>
    /// <param name="cf"></param>
    /// <returns>The current IconExtDrawable for chaining.</returns>
    public IconicsDrawable ColorFilter(ColorFilter cf)
    {
      SetColorFilter(cf);
      return this;
    }

    /// <summary>
    /// Sets the opacity
    /// </summary>
    /// <param name="alpha"></param>
    /// <returns>The current IconExtDrawable for chaining.</returns>
    public IconicsDrawable SetOpacity(int alpha)
    {
      SetAlpha(alpha);
      return this;
    }

    /// <summary>
    /// Sets the style
    /// </summary>
    /// <param name="style"></param>
    /// <returns>The current IconExtDrawable for chaining.</returns>
    public IconicsDrawable Style(Paint.Style style)
    {
      _iconPaint.SetStyle(style);
      return this;
    }

    /// <summary>
    /// sets the typeface of the drawable
    /// NOTE THIS WILL OVERWRITE THE ICONFONT!
    /// </summary>
    /// <param name="typeface"></param>
    /// <returns></returns>
    public IconicsDrawable Typeface(Typeface typeface)
    {
      _iconPaint.SetTypeface(typeface);
      return this;
    }

    public override void Draw(Canvas canvas)
    {
      if (_icon != null || _plainIcon != null)
      {
        var viewBounds = Bounds;

        UpdatePaddingBounds(viewBounds);
        UpdateTextSize(viewBounds);
        OffsetIcon(viewBounds);

        if (_backgroundColor != -1)
        {
          canvas.DrawColor(_backgroundColor);
        }

        _path.Close();

        if (_drawContour)
        {
          canvas.DrawPath(_path, _contourPaint);
        }

        _iconPaint.Alpha = _alpha;

        canvas.DrawPath(_path, _iconPaint);
      }
    }

    public override bool IsStateful
    {
      get { return true; }
    }

    public override bool SetState(int[] stateSet)
    {
      SetAlpha(_alpha);
      return true;
    }

    public override int IntrinsicWidth
    {
      get { return _size; }
    }

    public override int IntrinsicHeight
    {
      get { return _size; }
    }

    public override int Opacity
    {
      get { return (int) Format.Opaque; }
    }

    public override int Alpha
    {
      get { return _alpha; }
      set { _alpha = value; }
    }

    public override void SetAlpha(int alpha)
    {
      _alpha = alpha;
    }

    /// <summary>
    /// just a helper method to get the alpha value
    /// </summary>
    /// <returns></returns>
    public int GetCompatAlpha()
    {
      return _alpha;
    }

    public override void SetColorFilter(ColorFilter cf)
    {
      _iconPaint.SetColorFilter(cf);
    }
			
		/// <summary>
		/// Creates a BitMap to use in Widgets or anywhere else
		/// </summary>
		/// <returns>bitmap to set</returns>
    public Bitmap ToBitmap()
    {
      if (_size == -1)
      {
        ActionBarSize();
      }

      var bitmap = Bitmap.CreateBitmap(IntrinsicWidth, IntrinsicHeight, Bitmap.Config.Argb8888);

      Style(Paint.Style.Fill);

      var canvas = new Canvas(bitmap);
      SetBounds(0, 0, canvas.Width, canvas.Height);
      Draw(canvas);

      return bitmap;
    }

    //------------------------------------------
    // PRIVATE HELPER METHODS
    //------------------------------------------

    /// <summary>
    /// Update the Padding Bounds
    /// </summary>
    /// <param name="viewBounds"></param>
    private void UpdatePaddingBounds(Rect viewBounds)
    {
      if (_iconPadding >= 0
          && !(_iconPadding*2 > viewBounds.Width())
          && !(_iconPadding*2 > viewBounds.Height()))
      {
        _paddingBounds.Set(
          viewBounds.Left + _iconPadding,
          viewBounds.Top + _iconPadding,
          viewBounds.Right - _iconPadding,
          viewBounds.Bottom - _iconPadding);
      }
    }

    /// <summary>
    /// Update the TextSize
    /// </summary>
    /// <param name="viewBounds"></param>
    private void UpdateTextSize(Rect viewBounds)
    {
      float textSize = (float) viewBounds.Height()*2;
      _iconPaint.TextSize = textSize;

      var textValue = _icon != null
        ? String.ValueOf(_icon.GetCharacter)
        : String.ValueOf(_plainIcon);
      _iconPaint.GetTextPath(textValue, 0, 1, 0, viewBounds.Height(), _path);
      _path.ComputeBounds(_pathBounds, true);

      var deltaWidth = ((float) _paddingBounds.Width()/_pathBounds.Width());
      var deltaHeight = ((float) _paddingBounds.Height()/_pathBounds.Height());
      var delta = (deltaWidth < deltaHeight) ? deltaWidth : deltaHeight;
      textSize *= delta;

      _iconPaint.TextSize = textSize;

      _iconPaint.GetTextPath(textValue, 0, 1, 0, viewBounds.Height(), _path);
      _path.ComputeBounds(_pathBounds, true);
    }

    /// <summary>
    /// Set the icon offset
    /// </summary>
    /// <param name="viewBounds"></param>
    private void OffsetIcon(Rect viewBounds)
    {
      var startX = viewBounds.CenterX() - (_pathBounds.Width()/2);
      var offsetX = startX - _pathBounds.Left;
      
      var startY = viewBounds.CenterY() - (_pathBounds.Height()/2);
      var offsetY = startY - (_pathBounds.Top);

      _path.Offset(offsetX + _iconOffsetX, offsetY + _iconOffsetY);
    }
  }

  public class IconicsDrawable<T> : IconicsDrawable
    where T : ITypeface, new()
  {

    private T _typeface;

    protected internal IconicsDrawable()
    {
      _typeface = new T();
    }

    //private IconicsDrawable(Context context, char icon)
    //  : base(context, icon)
    //{

    //}

    private IconicsDrawable(Context context, string icon)
      : base(context, icon)
    {

    }

    private IconicsDrawable(Context context, IIcon icon)
      : base(context, icon)
    {

    }

    private IconicsDrawable(Context context, ITypeface typeface, IIcon icon)
      : base(context, typeface, icon)
    {

    }

    public IconicsDrawable(Context context, System.Enum icon)
      : this()
    {
      _context = context;
      Prepare();
      Icon(_typeface.GetIcon(icon.ToString()));
    }
  }
}