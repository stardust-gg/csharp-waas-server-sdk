using StartdustCustodialSDK.Application;
using StartdustCustodialSDK.Profile;
using StartdustCustodialSDK.Wallet;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StartdustCustodialSDK
{
    public class StardustCustodialSdk
    {
        private StardustApplicationAPI stardustApplicationAPI;
        private StardustProfileAPI stardustProfileAPI;
        private StardustProfileIdentifierAPI stardustProfileIdentifierAPI;
        private StardustWalletAPI stardustWalletAPI;

        public StardustCustodialSdk(string apiKey, string url = BaseStardustAPI.StardustUrl)
        {
            stardustApplicationAPI = new StardustApplicationAPI(apiKey, url);
            stardustProfileAPI = new StardustProfileAPI(apiKey, url);
            stardustProfileIdentifierAPI = new StardustProfileIdentifierAPI(apiKey, url);
            stardustWalletAPI = new StardustWalletAPI(apiKey, url);
        }

        public async Task<StardustApplication> GetApplication()
        {
            return await stardustApplicationAPI.Get();
        }

        public async Task<StardustProfile> GetProfile(string profileId)
        {
            return await stardustProfileAPI.Get(profileId);
        }

        public async Task<StardustProfile> CreateProfile(string applicationId, string name = "")
        {
            var profileParams = new StardustProfileCreateParams(applicationId, name);
            return await stardustProfileAPI.Create(profileParams);
        }

        public async Task<StardustProfileIdentifier> GetProfileIdentifier(string profileIdentifierId)
        {
            return await stardustProfileIdentifierAPI.Get(profileIdentifierId);
        }

        public async Task<string> GenerateProfileJWT(string profileId, int duration)
        {
            return await stardustProfileAPI.GenerateClientJWT(profileId, duration);
        }



        [Obsolete("Please create a profile and use the wallet generated for the profile via profile.Wallet or profile.Wallets")]
        public async Task<StardustWallet> CreateWallet()
        {
            return await this.stardustWalletAPI.Create();
        }

        [Obsolete("Please use getProfile in order to access your wallet(s) for the user")]
        public async Task<StardustWallet> GetWallet(string walletId)
        {
            return await this.stardustWalletAPI.Get(walletId);
        }
    }
}
