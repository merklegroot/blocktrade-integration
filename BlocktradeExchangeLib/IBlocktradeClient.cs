using BlocktradeExchangeLib.Models;
using System.Collections.Generic;

namespace BlocktradeExchangeLib
{
    public interface IBlocktradeClient
    {
        List<BlocktradeAsset> GetTradingAssets();
        string GetTradingAssetsRaw();

        string GetTradingPairsRaw();
        List<BlocktradeTradingPair> GetTradingPairs();

        string GetOrderBookRaw(int tradingPairId);
        BlocktradeOrderBook GetOrderBook(int tradingPairId);

        string GetUserRaw(BlocktradeApiKey apiKey);
        BlocktradeUser GetUser(BlocktradeApiKey apiKey);

        string GetUserPortfoliosRaw(BlocktradeApiKey apiKey);
        List<BlocktradePortfolio> GetUserPortfolios(BlocktradeApiKey apiKey);

        string CancelActiveRaw(BlocktradeApiKey apiKey);
        BlocktradeMessage CancelActive(BlocktradeApiKey apiKey);

        string GetUserOrdersRaw(BlocktradeApiKey apiKey);
    }
}
