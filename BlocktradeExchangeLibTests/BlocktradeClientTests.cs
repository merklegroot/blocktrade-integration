using BlocktradeExchangeLib;
using BlocktradeExchangeLib.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BlocktradeExchangeLibTests
{
    [TestClass]
    public class BlocktradeClientTests
    {
        private BlocktradeClient _blocktradeClient;

        [TestInitialize]
        public void Setup()
        {
            _blocktradeClient = new BlocktradeClient();
        }

        [TestMethod]
        public void Get_trading_assets_raw()
        {
            var assets = _blocktradeClient.GetTradingAssetsRaw();
            assets.Dump();
        }

        [TestMethod]
        public void Get_trading_assets()
        {
            var assets = _blocktradeClient.GetTradingAssets();
            assets.Dump();
        }

        [TestMethod]
        public void Get_trading_pairs_raw()
        {
            var results = _blocktradeClient.GetTradingPairsRaw();
            results.Dump();
        }

        [TestMethod]
        public void Get_trading_pairs()
        {
            var results = _blocktradeClient.GetTradingPairs();
            results.Dump();
        }

        [TestMethod]
        public void Get_api_key()
        {
            var apiKey = GetApiKey();
            apiKey.Dump();
        }

        [TestMethod]
        public void Get_user_raw()
        {
            var apiKey = GetApiKey();
            var user = _blocktradeClient.GetUserRaw(apiKey);

            user.Dump();
        }

        [TestMethod]
        public void Get_user()
        {
            var apiKey = GetApiKey();
            var user = _blocktradeClient.GetUser(apiKey);

            user.Dump();
        }

        [TestMethod]
        public void Get_portfolios_raw()
        {
            var apiKey = GetApiKey();
            var portfolios = _blocktradeClient.GetUserPortfoliosRaw(apiKey);

            portfolios.Dump();
        }

        [TestMethod]
        public void Get_portfolios()
        {
            var apiKey = GetApiKey();
            var portfolios = _blocktradeClient.GetUserPortfolios(apiKey);

            portfolios.Dump();
        }

        [TestMethod]
        public void Cancel_active_raw()
        {
            var apiKey = GetApiKey();
            var response = _blocktradeClient.CancelActiveRaw(apiKey);

            response.Dump();
        }

        [TestMethod]
        public void Cancel_active()
        {
            var apiKey = GetApiKey();
            var response = _blocktradeClient.CancelActive(apiKey);

            response.Dump();
        }

        [TestMethod]
        public void Get_user_orders_raw()
        {
            var apiKey = GetApiKey();
            var response = _blocktradeClient.GetUserOrdersRaw(apiKey);

            response.Dump();
        }

        [TestMethod]
        public void Get_user_orders()
        {
            var apiKey = GetApiKey();
            var response = _blocktradeClient.GetUserOrders(apiKey);

            response.Dump();
        }

        [TestMethod]
        public void Cancel_order_raw()
        {
            const long OrderId = 14015569;

            var apiKey = GetApiKey();
            var response = _blocktradeClient.CancelOrderRaw(apiKey, OrderId);

            response.Dump();
        }

        [TestMethod]
        public void Cancel_order()
        {
            const long OrderId = 14015569;

            var apiKey = GetApiKey();
            var response = _blocktradeClient.CancelOrder(apiKey, OrderId);

            response.Dump();
        }

        [TestMethod]
        public void Buy_eth_btc()
        {
            var apiKey = GetApiKey();
            const decimal Quantity = 0.01m;
            const decimal Price = 0.033m;

            const string BaseSymbol = "ETH";
            const string QuoteSymbol = "BTC";

            var results = _blocktradeClient.PlaceLimitBidRaw(apiKey, BaseSymbol, QuoteSymbol, Quantity, Price);
            results.Dump();
        }

        [TestMethod]
        public void Sell_eth_btc()
        {
            var apiKey = GetApiKey();
            const decimal Quantity = 0.01m;
            const decimal Price = 0.035m;

            const string BaseSymbol = "ETH";
            const string QuoteSymbol = "BTC";

            var results = _blocktradeClient.PlaceLimitAskRaw(apiKey, BaseSymbol, QuoteSymbol, Quantity, Price);
            results.Dump();
        }

        private BlocktradeApiKey GetApiKey() => new ApiConfigRepo().GetKey();
    }
}
