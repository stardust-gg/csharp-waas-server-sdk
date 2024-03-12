using StartdustCustodialSDK.Application;
using StartdustCustodialSDK.Profile;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartdustCustodialSDK.Wallet
{
    public class StardustWallet
    {
        public string Id { get; set; }
        public string ProfileId { get; set; }
        public StardustApplication Application { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUsedAt { get; set; }
        public string ApiKey { get; set; }

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
    }
}
