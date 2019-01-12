using Newtonsoft.Json;
using System;

namespace BlocktradeExchangeLibTests
{
    public static class DumpExtension
    {
        public static string Dump(this object item)
        {
            var getText = new Func<string>(() =>
            {
                if (item == null) { return "(null)"; }

                return JsonConvert.SerializeObject(item, Formatting.Indented);
            });

            var text = getText();
            Console.WriteLine(text);

            return text;
        }
    }
}
