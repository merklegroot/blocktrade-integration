using BlocktradeExchangeLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace BlocktradeExchangeLib
{
    // https://trade.blocktrade.com/api_documentation
    public class BlocktradeClient : IBlocktradeClient
    {
        private readonly IWebClient _webClient;

        public BlocktradeClient(IWebClient webUtil = null)
        {
            _webClient = webUtil ?? new WebClient();
        }

        public string GetTradingAssetsRaw()
        {
            const string Url = "https://trade.blocktrade.com/api/v1/trading_assets";
            return _webClient.HttpGet(Url);
        }

        public List<BlocktradeAsset> GetTradingAssets()
        {
            var contents = GetTradingAssetsRaw();
            return !string.IsNullOrWhiteSpace(contents)
                ? JsonConvert.DeserializeObject<List<BlocktradeAsset>>(contents)
                : new List<BlocktradeAsset>();
        }

        public string GetTradingPairsRaw()
        {
            const string Url = "https://trade.blocktrade.com/api/v1/trading_pairs";
            return _webClient.HttpGet(Url);
        }

        public List<BlocktradeTradingPair> GetTradingPairs()
        {
            var contents = GetTradingPairsRaw();
            return !string.IsNullOrWhiteSpace(contents)
                ? JsonConvert.DeserializeObject<List<BlocktradeTradingPair>>(contents)
                : new List<BlocktradeTradingPair>();
        }

        public string GetOrderBookRaw(int tradingPairId)
        {
            var url = $"https://trade.blocktrade.com/api/v1/order_book/{tradingPairId}";
            return _webClient.HttpGet(url);
        }

        public BlocktradeOrderBook GetOrderBook(int tradingPairId)
        {
            var contents = GetOrderBookRaw(tradingPairId);
            return !string.IsNullOrWhiteSpace(contents)
                ? JsonConvert.DeserializeObject<BlocktradeOrderBook>(contents)
                : new BlocktradeOrderBook();
        }

        public string GetUserRaw(BlocktradeApiKey apiKey)
        {
            const string Url = "https://trade.blocktrade.com/api/v1/user";
            return AuthenticatedGet(apiKey, Url);
        }

        public BlocktradeUser GetUser(BlocktradeApiKey apiKey)
        {
            var contents = GetUserRaw(apiKey);
            return !string.IsNullOrWhiteSpace(contents)
                ? JsonConvert.DeserializeObject<BlocktradeUser>(contents)
                : new BlocktradeUser();
        }

        public string GetUserPortfoliosRaw(BlocktradeApiKey apiKey)
        {
            const string Url = "https://trade.blocktrade.com/api/v1/portfolios";
            return AuthenticatedGet(apiKey, Url);
        }

        public List<BlocktradePortfolio> GetUserPortfolios(BlocktradeApiKey apiKey)
        {
            var contents = GetUserPortfoliosRaw(apiKey);
            return !string.IsNullOrWhiteSpace(contents)
                ? JsonConvert.DeserializeObject<List<BlocktradePortfolio>>(contents)
                : new List<BlocktradePortfolio>();
        }

        public string CancelActiveRaw(BlocktradeApiKey apiKey)
        {
            const string Url = "https://trade.blocktrade.com/api/v1/orders/cancel_active";
            return AuthenticatedPost(apiKey, Url);
        }

        public BlocktradeMessage CancelActive(BlocktradeApiKey apiKey)
        {
            var contents = CancelActiveRaw(apiKey);
            return !string.IsNullOrWhiteSpace(contents)
                ? JsonConvert.DeserializeObject<BlocktradeMessage>(contents)
                : new BlocktradeMessage();
        }

        public string GetUserOrdersRaw(BlocktradeApiKey apiKey)
        {
            const string Url = "https://trade.blocktrade.com/api/v1/orders";
            return AuthenticatedGet(apiKey, Url);
        }

        public BlocktradeGetUserOrdersResponse GetUserOrders(BlocktradeApiKey apiKey)
        {
            var contents = GetUserOrdersRaw(apiKey);
            return !string.IsNullOrWhiteSpace(contents)
                ? JsonConvert.DeserializeObject<BlocktradeGetUserOrdersResponse>(contents)
                : new BlocktradeGetUserOrdersResponse();
        }

        private static long GenerateNonce()
        {
            var currentTime = DateTime.UtcNow;
            return (long)(currentTime.Subtract(new DateTime(1970, 1, 1)))
                //.TotalSeconds;
                .TotalMilliseconds;
        }

        private static string Sign(string secretKey, string message)
        {
            var encoding = new UTF8Encoding();
            var keyByte = encoding.GetBytes(secretKey);

            var hmac = new HMACSHA256(keyByte);

            var messageBytes = encoding.GetBytes(message);
            var hashedMessage = hmac.ComputeHash(messageBytes);

            return ByteArrayToString(hashedMessage).ToUpper();
        }

        private static string ByteArrayToString(byte[] ba)
        {
            var hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba) { hex.AppendFormat("{0:x2}", b); }

            return hex.ToString();
        }

        private string AuthenticatedPost(BlocktradeApiKey apiKey, string url)
        {
            var nonce = GenerateNonce();
            var message = $"{apiKey.PublicKey}.{nonce}.";
            var signature = Sign(apiKey.PrivateKey, message);

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("X-Api-Key", apiKey.PublicKey);
            request.Headers.Add("X-nonce", nonce.ToString());
            request.Headers.Add("X-Signature", signature);

            using (var response = request.GetResponse())
            using (var responseStream = response.GetResponseStream())
            using (var reader = new StreamReader(responseStream))
            {
                return reader.ReadToEnd();
            }
        }

        private string AuthenticatedGet(BlocktradeApiKey apiKey, string url)
        {
            var nonce = GenerateNonce();
            var message = $"{apiKey.PublicKey}.{nonce}.";
            var signature = Sign(apiKey.PrivateKey, message);

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("X-Api-Key", apiKey.PublicKey);
            request.Headers.Add("X-nonce", nonce.ToString());
            request.Headers.Add("X-Signature", signature);

            using (var response = request.GetResponse())
            using (var responseStream = response.GetResponseStream())
            using (var reader = new StreamReader(responseStream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
