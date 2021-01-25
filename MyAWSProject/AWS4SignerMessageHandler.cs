using Aws4RequestSigner;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Nethereum.AWS
{
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
