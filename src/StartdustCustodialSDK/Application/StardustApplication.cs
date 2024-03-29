﻿using StartdustCustodialSDK.Profile;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartdustCustodialSDK.Application
{
    public class StardustApplication : BaseStardust
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string RootUserId { get; set; }
        public string IdentityId { get; set; }

        public StardustApplication() { }

        public StardustApplication(string id, string name, string email, string description = null, string apiKey = null, string rootUserId = null, string identityId = null)
        {
            Id = id;
            Name = name;
            Email = email;
            Description = description;
            ApiKey = apiKey;
            RootUserId = rootUserId;
            IdentityId = identityId;
        }
    }
}
