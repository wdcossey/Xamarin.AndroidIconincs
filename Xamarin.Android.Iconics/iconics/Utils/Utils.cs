using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Util;

namespace com.xamarin.AndroidIconics
{
  public static class Utils
  {
    public static int ConvertDpToPx(this Context context, float dp)
    {
      return (int) TypedValue.ApplyDimension(ComplexUnitType.Dip, dp,
        context.Resources.DisplayMetrics);
    }

    public static bool IsEnabled(this IEnumerable<int> stateSet)
    {
      return stateSet.Any(state => state == Android.Resource.Attribute.StateEnabled);
    }
  }
}