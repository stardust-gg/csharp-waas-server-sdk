using System;
using System.Collections.Generic;
using System.Text;

namespace StartdustCustodialSDK.Application
{
    public class StardustApplication
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string ApiKey { get; set; }
        public double? CreatedAt { get; set; }
        public double? LastUpdated { get; set; }
        public string RootUserId { get; set; }
        public string IdentityId { get; set; }

        public StardustApplication() { }

        public StardustApplication(string id, string name, string email, string description = null, string apiKey = null, double? createdAt = null, double? lastUpdated = null, string rootUserId = null, string identityId = null)
        {
            Id = id;
            Name = name;
            Email = email;
            Description = description;
            ApiKey = apiKey;
            CreatedAt = createdAt;
            LastUpdated = lastUpdated;
            RootUserId = rootUserId;
            IdentityId = identityId;
        }
    }
}
