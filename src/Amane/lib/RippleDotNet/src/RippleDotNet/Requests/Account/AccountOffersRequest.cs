﻿using Newtonsoft.Json;

namespace RippleDotNet.Requests.Account
{
    public class AccountOffersRequest : BaseLedgerRequest
    {
        public AccountOffersRequest(string account)
        {
            Account = account;
            Command = "account_offers";
        }

        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("marker")]
        public object Marker { get; set; }
    }
}
