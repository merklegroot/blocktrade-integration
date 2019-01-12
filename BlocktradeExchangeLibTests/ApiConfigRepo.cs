using BlocktradeExchangeLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlocktradeExchangeLibTests
{
    public class ApiConfigRepo
    {
        public BlocktradeApiKey GetKey()
        {
            const string Url = "http://localhost/trade/api/get-blocktrade-api-key";
            var contents = new WebClient().HttpPost(Url);

            var dict = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(contents);

            var apiKey = dict["apiKey"];

            return new BlocktradeApiKey
            {
                PublicKey = apiKey["key"],
                PrivateKey = apiKey["secret"]
            };
        }
    }
}
