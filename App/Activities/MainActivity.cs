using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using App.Fragments;
using com.xamarin.AndroidIconics;
using com.xamarin.AndroidIconics.Typefaces;
using com.xamarin.component.MaterialDrawer;
using com.xamarin.component.MaterialDrawer.Models;
using com.xamarin.component.MaterialDrawer.Models.Interfaces;
using Toolbar = Android.Support.V7.Widget.Toolbar;


namespace App.Activities
{
  [Activity(MainLauncher = true)]
  public class MainActivity : AppCompatActivity, Drawer.IOnDrawerItemClickListener
  {

    protected override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);

      SetContentView(Resource.Layout.activity_main);

      // Handle Toolbar
      Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
      SetSupportActionBar(toolbar);
      SupportActionBar.SetDisplayHomeAsUpEnabled(false);

      List<IDrawerItem> items = new List<IDrawerItem>(Iconics.RegisteredFonts.Count);
      //add all icons of all registered Fonts to the list
      foreach (var font in Iconics.RegisteredFonts)
      {
        items.Add(new PrimaryDrawerItem().WithName(font.FontName));
      }

      new DrawerBuilder(this) /*.WithActivity()*/
        .WithToolbar(toolbar)
        .WithSliderBackgroundColor(Color.White)
        .WithTranslucentStatusBar(true)
        //.WithFullscreen(true)
        .WithDrawerItems(items)
        .WithOnDrawerItemClickListener(this)
        //.WithOnDrawerItemClickListener(new Drawer.OnDrawerItemClickListener() {
        //    @Override
        //    public void onItemClick(AdapterView<?> adapterView, View view, int i, long l, IDrawerItem iDrawerItem) {
        //        ITypeface font = new ArrayList<>(Iconics.getRegisteredFonts()).get(i);
        //        loadIcons(font.getFontName());

        //        getSupportActionBar().setTitle(font.getFontName());

        //    }
        //})
        .WithFireOnInitialOnClick(true)
        .WithSelectedItem(1)
        .Build();

      //LoadIcons(Iconics.GetRegisteredFonts().Skip(5).FirstOrDefault().FontName);
    }


    public override bool OnCreateOptionsMenu(IMenu menu)
    {
      // Inflate the menu items for use in the action bar
      MenuInflater.Inflate(Resource.Menu.menu_main, menu);

      var menuItem = menu.FindItem(Resource.Id.action_opensource);

      menuItem.SetIcon(new IconicsDrawable<FontAwesome>(this, FontAwesome.Icon.faw_github).ActionBar()
        .Color(Color.White));
      //menuItem.setIcon(new IconicsDrawable(this, "faw-github").actionBarSize().color(Color.WHITE));

      return base.OnCreateOptionsMenu(menu);
    }


    public override bool OnOptionsItemSelected(IMenuItem item)
    {
      // Handle presses on the action bar items
      switch (item.ItemId)
      {
        case Resource.Id.action_opensource:
          //new Libs.Builder()
          //        .WithFields(Resource.String.class.getFields())
          //        .WithLicenseShown(true)
          //        .WithActivityTitle(getString(Resource.String.action_opensource))
          //        .WithActivityTheme(Resource.Style.AppTheme)
          //        .WithActivityStyle(Libs.ActivityStyle.LIGHT_DARK_TOOLBAR)
          //        .Start(this);

          return true;
        case Resource.Id.action_playground:
          var ip = new Intent(ApplicationContext, typeof (PlaygroundActivity));
          StartActivity(ip);
          return true;
        default:
          return base.OnOptionsItemSelected(item);
      }
    }

    private void LoadIcons(string fontName)
    {
      SupportFragmentManager.BeginTransaction()
        .Replace(Resource.Id.content, IconsFragment.NewInstance(fontName))
        .Commit();
    }

    public bool OnItemClick(AdapterView parent, View view, int position, long id, IDrawerItem drawerItem)
    {
      var font = Iconics.RegisteredFonts[position];
      LoadIcons(font.FontName);

      SupportActionBar.Title = font.FontName;

      return false;
    }
  }
}