namespace KMorcinek.YetAnotherTodo.FromXmlConverter
{
    public class EncodingImprover
    {
        readonly static string[][] PairsToChange =
        {
            new[] {"Ä…", "ą"},
            new[] {"Å", "ł"},
            new[] {"Ä™", "ę"},
            new[] {"Å„", "ń"},
            new[] {"Å›", "ś"},
            new[] {"Å‚", "ł"},
                new[] {"Ä‡", "ć"},
                new[] {"Ĺ‚", "ł"},
                new[] {"Ăł", "ó"},
                new[] {"ĹĽ", "ż"},
                new[] {"Ĺ›", "ś"},
                new[] {"Ĺ„", "ń"},
                new[] {"ł¼", "ż"},
                new[] {"Ã³", "ó"},
                new[] {"ł›", "ś"},
                new[] {"ł‚", "ł"},
                new[] {"ł„", "ń"},
                new[] {"â€“", "-"},
                new[] {"ł»", "Ż"},
                new[] {"łº", "ź"},
                new[] {"â€œ", "“"},
                new[] {"â€", "”"},
                new[] {"łš", "Ś"},
//                new[] {"", ""},
//                new[] {"", ""},
//                new[] {"", ""},
        };

        public static string Improve(string content)
        {
            foreach (var pairToChange in PairsToChange)
            {
                content = content.Replace(pairToChange[0], pairToChange[1]);
            }

            return content;
        }
    }
}