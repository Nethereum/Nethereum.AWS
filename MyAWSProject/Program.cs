using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System;
using System.Threading.Tasks;
using Nethereum.AWS;

namespace MyAWSProject
{
    class Program
    {
        //*Enter your details here..*//
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
    }
}
