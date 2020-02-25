using System.Text.RegularExpressions;

namespace Framework.Utils
{
    public class StringUtils
    {
        public static string CutCharactersAfterComma(string valueToCut) =>
            Regex.Match(valueToCut, @"([^,]+$)")
                .Value
                .Replace(" ", string.Empty);

        public static string CutNonDigitCharacters(string valueToCut) =>
            Regex.Match(valueToCut, @"^[^aA-aA-zZ-яЯ]*")
                .Value
                .Replace(" ", string.Empty);
    }
}