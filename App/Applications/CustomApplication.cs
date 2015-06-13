using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App.Typefaces;
using com.xamarin.AndroidIconics;

namespace App.Applications
{
  [Application(
    Icon = "@drawable/ic_launcher",
    Label = "@string/app_name",
    Theme = "@style/AppTheme",
#if DEBUG
 Debuggable = true
#else
    Debuggable = false,
#endif
    )]
  public class CustomApplication : Application
  {

    public CustomApplication(IntPtr javaReference, JniHandleOwnership transfer)
      : base(javaReference, transfer)
    {

    }

    public override void OnCreate()
    {
      base.OnCreate();
      Iconics.RegisterFont(new Meteoconcs());
      Iconics.RegisterFont(new Octicons());
      Iconics.RegisterFont(new CommunityMaterial());
      Iconics.RegisterFont(new GoogleMaterial());
      Iconics.RegisterFont(new CustomFont());
    }
  }
}