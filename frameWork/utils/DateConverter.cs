using System;
using System.Linq;

namespace Framework.Utils
{
    public static class DateConverter
    {
        private static readonly string[] Months =
        {
            "января", "февраля", "марта", "апреля", "мая", "июня", "июля", "августа", "сентября", "октября",
            "ноября", "декабря"
        };

        public static string ConvertDate(string stringValue)
        {
            return stringValue switch
            {
                "сегодня" => DateTime.Now.ToShortDateString(),
                "вчера" => DateTime.Now.AddDays(-1).ToShortDateString(),

                _ => Replace(stringValue)
            };
        }

        private static string Replace(string stringValue)
        {
            var result = stringValue;
            var year = DateTime.Now.ToString("yy");
            var sKeyResult = Months.FirstOrDefault(stringValue.Contains);

            result = sKeyResult switch
            {
                "января" => stringValue.Replace("января", $".01.{year}"),
                "февраля" => stringValue.Replace("февраля", $".02.{year}"),
                "марта" => stringValue.Replace("февраля", $".03.{year}"),
                "апреля" => stringValue.Replace("февраля", $".04.{year}"),
                "мая" => stringValue.Replace("февраля", $".05.{year}"),
                "июня" => stringValue.Replace("февраля", $".06.{year}"),
                "июля" => stringValue.Replace("февраля", $".07.{year}"),
                "августа" => stringValue.Replace("февраля", $".08.{year}"),
                "сентября" => stringValue.Replace("февраля", $".09.{year}"),
                "октября" => stringValue.Replace("февраля", $".10.{year}"),
                "ноября" => stringValue.Replace("февраля", $".11.{year}"),
                "декабря" => stringValue.Replace("февраля", $".12.{year}"),
                _ => result
            };

            return DateTime.ParseExact(result.Replace(" ", string.Empty), "d.MM.yy",
                null).ToShortDateString();
        }
    }
}