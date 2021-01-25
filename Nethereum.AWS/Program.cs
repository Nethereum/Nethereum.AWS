using Aws4RequestSigner;
using Nethereum.Model;
using Nethereum.RPC.Accounts;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Nethereum.AWS
{
    class Program
    {
        const string AWS_ACCESS_KEY_ID = "AKIAIOSFODNN7EXAMPLE";
        const string AWS_SECRET_ACCESS_KEY = "wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY";
        const string AMB_HTTP_ENDPOINT = "https://nd-6eaj5va43jggnpxouzp7y47e4y.ethereum.managedblockchain.us-east-1.amazonaws.com/";
        const string PRIVATE_KEY = "0x0000";

        static void Main(string[] args)
        {
            var account = new Web3.Accounts.Account(PRIVATE_KEY);
            var web3 = AWSWeb3Factory.CreateWeb3(account, AMB_HTTP_ENDPOINT, AWS_ACCESS_KEY_ID, AWS_SECRET_ACCESS_KEY, "us-east-1");
        }
    }

    public class AWSWeb3Factory
    {
        public static Web3.Web3 CreateWeb3(IAccount account, string url, string accessKey, string secretKey, string region = "us-east-1", string service = "managedblockchain")
        {
            var handler = new AWS4SignerMessageHandler(accessKey, secretKey, region, service);
            var rpcClient = new Nethereum.JsonRpc.Client.RpcClient(new Uri(url), null, null, handler);
            return new Web3.Web3(account, rpcClient);
        }
    }

    public class AWS4SignerMessageHandler : HttpClientHandler
    {
        private readonly string accessKey;
        private readonly string secretKey;
        private readonly string region;
        private readonly string service;

        public AWS4SignerMessageHandler(string accessKey, string secretKey, string region = "us-east-1", string service = "managedblockchain")
        {
            this.accessKey = accessKey;
            this.secretKey = secretKey;
            this.region = region;
            this.service = service;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var signer = new AWS4RequestSigner(accessKey, secretKey);
            request = await signer.Sign(request, service, region);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
