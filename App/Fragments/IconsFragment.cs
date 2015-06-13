using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using App.Adapters;
using com.xamarin.AndroidIconics;

namespace App.Fragments
{

  /// <summary>
  /// Created by wdcossey on 08/06/2015.
  /// Original by a557114 on 16/04/2015.
  /// </summary>
  public class IconsFragment : Fragment
  {
    private const string FONT_NAME = "FONT_NAME";
    private readonly List<string> _icons = new List<string>();

    public static IconsFragment NewInstance(string fontName) 
    {
        var bundle = new Bundle();

        var fragment = new IconsFragment();

        bundle.PutString(FONT_NAME, fontName);

        fragment.Arguments = bundle;

        return fragment;
    }

    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
    {
      return inflater.Inflate(Resource.Layout.icons_fragment, null, false);
    }

    public override void OnViewCreated(View view, Bundle savedInstanceState)
    {
      base.OnViewCreated(view, savedInstanceState);

      // Init and Setup RecyclerView
      var mRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.list);
      mRecyclerView.SetLayoutManager(new GridLayoutManager(Activity, 2));
      //animator not yet working
      //mRecyclerView.SetItemAnimator(new SlideInLeftAnimator());

      var mAdapter = new IconAdapter(new List<string>(), Resource.Layout.row_icon);
      mRecyclerView.SetAdapter(mAdapter);

      if (Arguments != null)
      {
        var fontName = Arguments.GetString(FONT_NAME);

        foreach (var iTypeface in Iconics.RegisteredFonts)
        {
          if (iTypeface.FontName.Equals(fontName, StringComparison.OrdinalIgnoreCase))
          {
            if (iTypeface.Icons != null)
            {
              //foreach (var icon in iTypeface.GetIcons())
              //{
              //  _icons.Add(icon);
              //}
              _icons.AddRange(iTypeface.Icons.OrderBy(ob => ob));
              mAdapter.SetIcons(_icons);
              break;
            }
          }
        }
      }
    }

    public override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);

      // Create your fragment here
    }
  }
}