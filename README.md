# Nethereum.AWS

Example of setting up Web3 to interact with AWS Managed Blockchain

The new AWS Managed blockchain to connect to Ethereum Mainnet, Ropsten and others, requires each request to be signed and authenticated using AWS V4 signatures. This provides the template on how to achieve this.

For more information on the AWS Ethereum managed check this: https://docs.aws.amazon.com/managed-blockchain/latest/ethereum-dev/ethereum-json-rpc.html

```csharp

        const string AWS_ACCESS_KEY_ID = "AKIAIOSFODNN7EXAMPLE";
        const string AWS_SECRET_ACCESS_KEY = "wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY";
        const string AMB_HTTP_ENDPOINT = "https://nd-6eaj5va43jggnpxouzp7y47e4y.ethereum.managedblockchain.us-east-1.amazonaws.com/";
        const string PRIVATE_KEY = "0xb5b1870957d373ef0eeffecc6e4812c0fd08f554b37b233526acc331bf1544f7";
        
        static async Task Main(string[] args)
        {
            var account = new Account(PRIVATE_KEY);
            var web3 = AWSWeb3Factory.CreateWeb3(account, AMB_HTTP_ENDPOINT, AWS_ACCESS_KEY_ID, AWS_SECRET_ACCESS_KEY, "eu-west-2");
            var blockNumber = await web3.Eth.Blocks.GetBlockNumber.SendRequestAsync();
            Console.WriteLine(blockNumber.Value);
        }

```

