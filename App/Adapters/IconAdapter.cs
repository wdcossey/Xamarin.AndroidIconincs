using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using com.xamarin.AndroidIconics;

namespace App.Adapters
{
  internal class IconAdapter : RecyclerView.Adapter
  {

    private readonly List<string> _icons;
    private readonly int _rowLayout;

    public IconAdapter(List<string> icons, int rowLayout)
    {
      this._icons = icons;
      this._rowLayout = rowLayout;
    }

    public void SetIcons(List<string> icons)
    {
      this._icons.AddRange(icons);
      this.NotifyItemRangeInserted(0, icons.Count - 1);
    }

    public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
    {
      var v = LayoutInflater.From(parent.Context).Inflate(_rowLayout, parent, false);
      return new ViewHolder(v);
    }

    public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
    {
      var icon = _icons[position];
      ((ViewHolder)holder).Image.SetIcon(icon);
      ((ViewHolder)holder).Name.Text = icon;
    }

    public override int ItemCount
    {
      get { return _icons == null ? 0 : _icons.Count; }
    }

    private class ViewHolder : RecyclerView.ViewHolder
    {
      private readonly TextView _name;
      private readonly IconicsImageView _image;
      
      public ViewHolder(View itemView)
        : base(itemView)
      {
        _name = itemView.FindViewById<TextView>(Resource.Id.name);
        _image = itemView.FindViewById<IconicsImageView>(Resource.Id.icon);
      }

      public TextView Name
      {
        get { return _name; }
      }

      public IconicsImageView Image
      {
        get { return _image; }
      }
    }
  }
}