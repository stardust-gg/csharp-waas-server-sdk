using StartdustCustodialSDK.Utils;
using StartdustCustodialSDK.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StartdustCustodialSDK.Profile
{
    public class StardustProfile : BaseStardust
    {
        private StardustWallet _wallet;
        [JsonIgnore]
        public StardustWallet Wallet { get { return _wallet; } }
        [JsonIgnore]
        public StardustProfileAPI StardustProfileAPI { get; set; }
        [JsonIgnore]
        public StardustProfileIdentifierAPI StardustProfileIdentifierAPI { get; set; }


        public string Id { get; set; }
        public string RootUserId { get; set; }
        public string ApplicationId { get; set; }
        public List<StardustWallet> Wallets { get; set; }
        public List<StardustProfileIdentifier> Identifiers { get; set; }
        public string Name { get; set; }

        public StardustProfile()
        {

        }

        public StardustProfile(string id, string rootUserId, string applicationId, List<StardustWallet> wallets = null, List<StardustProfileIdentifier> identifiers = null, string name = null, string apiKey = null)
        {
            Id = id;
            RootUserId = rootUserId;
            ApplicationId = applicationId;
            Identifiers = identifiers;
            Wallets = wallets;
            Name = name;
            ApiKey = apiKey;


        }

        public override void Init(string apiKey)
        {
            base.Init(apiKey);
            if (Wallets?.Count > 0)
            {
                // set api key for each wallet and initialize field like profile
                Wallets.ForEach(w => w.Init(apiKey));
                _wallet = Wallets.Where(w => w.ProfileId == this.Id).FirstOrDefault();
            }
            this.StardustProfileAPI = new StardustProfileAPI(apiKey);
            this.StardustProfileIdentifierAPI = new StardustProfileIdentifierAPI(apiKey);
        }

        public async Task<StardustProfileIdentifier> AddIdentifier(StardustProfileIdentifierService service, string value)
        {
            if (!StardustProfileIdentifier.ValidateIdentifier(service, value))
            {
                throw new Exception($"Invalid service {service}, please use StardustProfileIdentifierService enums");
            }
            var newProfileIdentifier = new StardustProfileIdentifierCreateParams(this.Id, service.DisplayName(), value);
            return await this.StardustProfileIdentifierAPI.Create(newProfileIdentifier);
        }

        public async Task<StardustProfileIdentifier> AddCustomIdentifier(string service, string value)
        {
            var newProfileIdentifier = new StardustProfileIdentifierCreateParams(this.Id, $"{service}", value);
            return await this.StardustProfileIdentifierAPI.Create(newProfileIdentifier);
        }

        public async Task<List<StardustProfileIdentifier>> GetIdentifiers(int start = 0, int limit = 10)
        {
            var listParams = new StardustProfileIdentifierListParams(this.Id, start, limit);
            return await this.StardustProfileIdentifierAPI.List(listParams);
        }
    }
}
