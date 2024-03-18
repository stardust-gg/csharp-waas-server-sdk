using StartdustCustodialSDK.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace StartdustCustodialSDK.Profile
{
    public class StardustProfileIdentifier : BaseStardust
    {
        public string Id { get; set; }
        public string RootUserId { get; set; }
        public string ProfileId { get; set; }
        public string Service { get; set; }
        public string Value { get; set; }

        public StardustProfileIdentifier()
        {

        }

        public StardustProfileIdentifier(string id, string rootUserId, string profileId, string service, string value)
        {
            Id = id;
            RootUserId = rootUserId;
            ProfileId = profileId;
            Service = service;
            Value = value;
        }

        // TODO: implement opinionated validation for each service type
        public static bool ValidateIdentifier(StardustProfileIdentifierService service, string value)
        {
            switch (service)
            {
                case StardustProfileIdentifierService.ExternalWallet:
                    return true;
                case StardustProfileIdentifierService.Discord:
                    return true;
                case StardustProfileIdentifierService.Apple:
                    return true;
                case StardustProfileIdentifierService.Google:
                    return true;
                case StardustProfileIdentifierService.Facebook:
                    return true;
                case StardustProfileIdentifierService.Twitter:
                    return true;
                case StardustProfileIdentifierService.Email:
                    return true;
                case StardustProfileIdentifierService.Phone:
                    return true;

                default:
                    return false;
            }
        }
    }
}
