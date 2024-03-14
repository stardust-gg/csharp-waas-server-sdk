﻿using StartdustCustodialSDK.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace StartdustCustodialSDK.Profile
{
    public class StardustProfile
    {
        private StardustWallet _wallet;
        [JsonIgnore]
        public StardustWallet Wallet { get { return _wallet; } }
        //public stardustProfileIdentifierAPI stardustProfileIdentifierAPI { get; set; }
        [JsonIgnore]
        public StardustProfileAPI StardustProfileAPI { get; set; }


        public string Id { get; set; }
        public string RootUserId { get; set; }
        public string ApplicationId { get; set; }
        public long CreatedAt { get; set; }
        public List<StardustWallet> Wallets { get; set; }
        //public List<stardustProfileIdentifier> Identifiers { get; set; }
        public string Name { get; set; }
        public string ApiKey { get; set; }

        public StardustProfile()
        {

        }

        public StardustProfile(string id, string rootUserId, string applicationId, long createdAt, List<StardustWallet> wallets = null, string name = null, string apiKey = null )
        {
            Id = id;
            RootUserId = rootUserId;
            ApplicationId = applicationId;
            CreatedAt = createdAt;
            Wallets = wallets;
            Name = name;
            ApiKey = apiKey;

           
        }

        public void Init(string apiKey)
        {
            if (Wallets?.Count > 0)
            {
                _wallet = Wallets.Where(w => w.ProfileId == this.Id).FirstOrDefault();
            }
            this.ApiKey = apiKey;
            this.StardustProfileAPI = new StardustProfileAPI(apiKey);
        }
    }
}