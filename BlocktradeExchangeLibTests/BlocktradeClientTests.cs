using BlocktradeExchangeLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        public void Get_trading_assets()
        {
            var assets = _blocktradeClient.GetTradingAssets();
            assets.Dump();
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

        private BlocktradeApiKey GetApiKey() => new ApiConfigRepo().GetKey();
    }
}
