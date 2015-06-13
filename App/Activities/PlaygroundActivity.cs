using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Text.Style;
using Android.Views;
using Android.Widget;
using com.xamarin.AndroidIconics;
using com.xamarin.AndroidIconics.Typefaces;

namespace App.Activities
{
  [Activity(Label = "@string/title_activity_playground", MainLauncher = false)]
  public class PlaygroundActivity : Activity
  {

    protected override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);
      SetContentView(Resource.Layout.activity_playground);

      //Show how to style the text of an existing TextView
      var tv1 = FindViewById<TextView>(Resource.Id.test1);
      new Iconics.IconicsBuilder().Ctx(this)
        .Style(new ForegroundColorSpan(Color.White), new BackgroundColorSpan(Color.Black), new RelativeSizeSpan(2f))
        .StyleFor("faw-adjust", new BackgroundColorSpan(Color.Red),
          new ForegroundColorSpan(Color.ParseColor("#33000000")), new RelativeSizeSpan(2f))
        .On(tv1)
        .Build();

      //You can also do some advanced stuff like setting an image within a text
      var tv2 = FindViewById<TextView>(Resource.Id.test5);
      var sb = new SpannableString(tv2.TextFormatted);
      var d = new IconicsDrawable<FontAwesome>(this, FontAwesome.Icon.faw_android).SizeDp(48).PaddingDp(4);
      sb.SetSpan(new ImageSpan(d, SpanAlign.Bottom), 1, 2, SpanTypes.ExclusiveExclusive);
      tv2.TextFormatted = sb;


      //Set the icon of an ImageView (or something else) as drawable
      var iv2 = FindViewById<ImageView>(Resource.Id.test2);
      iv2.SetImageDrawable(
        new IconicsDrawable<FontAwesome>(this, FontAwesome.Icon.faw_thumbs_o_up).SizeDp(48)
          .Color(Color.ParseColor("#aaFF0000"))
          .ContourWidthDp(1));

      //Set the icon of an ImageView (or something else) as bitmap
      var iv3 = FindViewById<ImageView>(Resource.Id.test3);
      iv3.SetImageBitmap(
        new IconicsDrawable<FontAwesome>(this, FontAwesome.Icon.faw_android).Color(Color.ParseColor("#deFF0000"))
          .ToBitmap());

      //Show how to style the text of an existing button (NOT WORKING AT THE MOMENT)
      var b4 = FindViewById<Button>(Resource.Id.test4);
      new Iconics.IconicsBuilder().Ctx(this)
        .Style(new BackgroundColorSpan(Color.Black))
        .Style(new RelativeSizeSpan(2f))
        .Style(new ForegroundColorSpan(Color.White))
        .On(b4)
        .Build();
    }
  }
}