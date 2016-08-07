
using System.Text.RegularExpressions;


namespace WebUI.Infrastructure.Utility
{
    public class CommonMethods
    {
        //ChangeUnknownCharactersToDash
     public  static string ChangeUnKnownCharacters(string inputstring)
        {
            return Regex.Replace(inputstring, @"[^A-Za-z0-9]+", "_");
        }
    }
}