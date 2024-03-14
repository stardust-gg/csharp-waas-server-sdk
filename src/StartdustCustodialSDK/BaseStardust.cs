using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StartdustCustodialSDK
{
    public class BaseStardust
    {
        public string ApiKey { get; set; }
      

        public BaseStardust()
        {

        }

        public virtual void Init(string apiKey)
        {
            this.ApiKey = apiKey;
        }


    }
}
