using Newtonsoft.Json;

namespace BlocktradeExchangeLib.Models
{
    public class BlocktradeMessage
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
