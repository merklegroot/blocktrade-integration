using Newtonsoft.Json;
using System.Collections.Generic;

namespace BlocktradeExchangeLib.Models
{
    public class BlocktradeGetUserOrdersResponse
    {
        [JsonProperty("data")]
        public List<BlocktradeUserOrder> Data { get; set; }
    }
}
