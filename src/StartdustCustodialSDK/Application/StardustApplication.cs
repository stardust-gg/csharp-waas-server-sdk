using System;
using System.Collections.Generic;
using System.Text;

namespace StartdustCustodialSDK.Application
{
    public class StardustApplication
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public string ApiKey { get; set; }

        public StardustApplication() { }

        public StardustApplication(string name, string email, string description, string id, string apiKey)
        {
            Name = name;
            Email = email;
            Description = description;
            Id = id;
            ApiKey = apiKey;
        }
    }
}
