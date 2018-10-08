﻿using Newtonsoft.Json;

namespace RippleDotNet.Requests.Transaction
{
    public class SubmitBlobRequest : RippleRequest
    {
        public SubmitBlobRequest()
        {
            Command = "submit";
        }

        [JsonProperty("tx_blob")]
        public string TransactionBlob { get; set; }

        [JsonProperty("fail_hard")]
        public bool FailHard { get; set; }
    }
}
