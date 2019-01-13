# .NET Blocktrade API Integration

A .NET client for the Blocktrade crypto exchange.  
Covers most of the public and private endpoints.  

Official API documentation  
https://trade.blocktrade.com/api_documentation

Exchange  
https://blocktrade.com/

# Examples
## Placing an order
```CSharp
var blocktradeClient = new BlocktradeClient();
var apiKey = new BlocktradeApiKey { PublicKey = "YOUR_PUBLIC_KEY", PrivateKey = "YOUR_PRIVATE_KEY" };
            
var response = blocktradeClient.PlaceLimitBid(apiKey, "ETH", "BTC", 0.01m, 0.033m);
Console.WriteLine($"Order ID: {response.Id}");
```