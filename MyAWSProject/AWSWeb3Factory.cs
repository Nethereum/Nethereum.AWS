using Nethereum.RPC.Accounts;
using System;

namespace Nethereum.AWS
{
    public class AWSWeb3Factory
    {
        public static Web3.Web3 CreateWeb3(IAccount account, string url, string accessKey, string secretKey, string region = "us-east-1", string service = "managedblockchain")
        {
            var handler = new AWS4SignerMessageHandler(accessKey, secretKey, region, service);
            var rpcClient = new Nethereum.JsonRpc.Client.RpcClient(new Uri(url), null, null, handler);
            return new Web3.Web3(account, rpcClient);
        }
    }
}
