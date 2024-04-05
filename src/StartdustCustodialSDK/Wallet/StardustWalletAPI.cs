using StartdustCustodialSDK.Application;
using StartdustCustodialSDK.Wallet;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StartdustCustodialSDK.Profile
{
    public class StardustWalletAPI : BaseStardustAPI
    {
        public StardustWalletAPI(string apiKey) : base(apiKey)
        {
        }

        public StardustWalletAPI(string apiKey, string url) : base(apiKey, url)
        {
        }

        public async Task<StardustWallet> Create()
        {
            var wallet = await ApiPost<StardustWallet>("wallet");
            wallet.Init(this.ApiKey);
            return wallet;
        }

        public async Task<StardustWallet> Get(string walletId)
        {
            var profile = await ApiGet<StardustWallet>($"wallet/{walletId}");
            profile.Init(this.ApiKey);
            return profile;
        }

    }
}
