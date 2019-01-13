using BlocktradeExchangeLib;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BlocktradeExchangeLibTests
{
    /// <summary>
    /// This is just how the local tests were retrieving the api key.
    /// Please store your api key securely.
    /// </summary>
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
