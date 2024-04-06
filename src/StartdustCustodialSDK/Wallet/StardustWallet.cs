using StartdustCustodialSDK.Application;
using StartdustCustodialSDK.Profile;
using StartdustCustodialSDK.Signers.Evm;
using StartdustCustodialSDK.Signers.Nethereum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StartdustCustodialSDK.Wallet
{
    public class StardustWallet : BaseStardust
    {
        public string Id { get; set; }
        public string ProfileId { get; set; }
        public StardustApplication Application { get; set; }

        [JsonIgnore]
        public EvmStardustSigner Evm { get; set; }
        [JsonIgnore]
        public NethereumStardustSigner Nethereum { get; set; }

        [JsonIgnore]
        public StardustProfileAPI StardustProfileAPI { get; set; }

        public StardustWallet()
        {

        }

        public StardustWallet(string id, string profileId, StardustApplication application, string apiKey = null)
        {
            Id = id;
            ProfileId = profileId;
            Application = application;
            ApiKey = apiKey;
            StardustProfileAPI = new StardustProfileAPI(apiKey);
            Evm = new EvmStardustSigner(apiKey, id);
            Nethereum = new NethereumStardustSigner(apiKey, id);
        }

        public async Task<StardustProfile> GetProfile()
        {
            return await this.StardustProfileAPI.Get(this.ProfileId);
        }

        public override void Init(string apiKey)
        {
            base.Init(apiKey);
            this.Application?.Init(apiKey);
            this.StardustProfileAPI = new StardustProfileAPI(apiKey);
            Evm = new EvmStardustSigner(apiKey, Id);
            Nethereum = new NethereumStardustSigner(apiKey, Id);
        }
    }
}
