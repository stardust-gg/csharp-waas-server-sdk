using StartdustCustodialSDK.Application;
using StartdustCustodialSDK.Profile;
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
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUsedAt { get; set; }

        [JsonIgnore]
        public StardustProfileAPI StardustProfileAPI { get; set; }

        public StardustWallet(string id, string profileId, StardustApplication application, DateTime createdAt, DateTime? lastUsedAt = null, string apiKey = null)
        {
            Id = id;
            ProfileId = profileId;
            Application = application;
            CreatedAt = createdAt;
            LastUsedAt = lastUsedAt;
            ApiKey = apiKey;
            StardustProfileAPI = new StardustProfileAPI(apiKey);
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
        }
    }
}
