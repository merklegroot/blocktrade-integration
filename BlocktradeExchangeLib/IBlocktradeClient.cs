using BlocktradeExchangeLib.Models;
using System.Collections.Generic;

namespace BlocktradeExchangeLib
{
    public interface IBlockTradeClient
    {
        List<BlockTradeAsset> GetTradingAssets();
        string GetTradingAssetsRaw();

        string GetTradingPairsRaw();
        List<BlockTradeTradingPair> GetTradingPairs();

        string GetOrderBookRaw(int tradingPairId);
        BlockTradeOrderBook GetOrderBook(int tradingPairId);
    }
}
