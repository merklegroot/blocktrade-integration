using BlocktradeExchangeLib.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BlocktradeExchangeLib
{
    // https://trade.blocktrade.com/api_documentation
    public class BlockTradeClient : IBlockTradeClient
    {
        private readonly IWebClient _webClient;

        public BlockTradeClient(IWebClient webUtil = null)
        {
            _webClient = webUtil ?? new WebClient();
        }

        public string GetTradingAssetsRaw()
        {
            const string Url = "https://trade.blocktrade.com/api/v1/trading_assets";
            return _webClient.HttpGet(Url);
        }

        public List<BlockTradeAsset> GetTradingAssets()
        {
            var contents = GetTradingAssetsRaw();
            return !string.IsNullOrWhiteSpace(contents)
                ? JsonConvert.DeserializeObject<List<BlockTradeAsset>>(contents)
                : new List<BlockTradeAsset>();
        }

        public string GetTradingPairsRaw()
        {
            const string Url = "https://trade.blocktrade.com/api/v1/trading_pairs";
            return _webClient.HttpGet(Url);
        }

        public List<BlockTradeTradingPair> GetTradingPairs()
        {
            var contents = GetTradingPairsRaw();
            return !string.IsNullOrWhiteSpace(contents)
                ? JsonConvert.DeserializeObject<List<BlockTradeTradingPair>>(contents)
                : new List<BlockTradeTradingPair>();
        }

        public string GetOrderBookRaw(int tradingPairId)
        {
            var url = $"https://trade.blocktrade.com/api/v1/order_book/{tradingPairId}";
            return _webClient.HttpGet(url);
        }

        public BlockTradeOrderBook GetOrderBook(int tradingPairId)
        {
            var contents = GetOrderBookRaw(tradingPairId);
            return !string.IsNullOrWhiteSpace(contents)
                ? JsonConvert.DeserializeObject<BlockTradeOrderBook>(contents)
                : new BlockTradeOrderBook();
        }
    }
}
