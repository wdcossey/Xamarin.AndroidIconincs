namespace com.xamarin.AndroidIconics.Typefaces
{
  public interface IIcon
  {
    string GetFormattedName { get; }

    string GetName { get; }

    char GetCharacter { get; }

    ITypeface GetTypeface { get; }
  }
}