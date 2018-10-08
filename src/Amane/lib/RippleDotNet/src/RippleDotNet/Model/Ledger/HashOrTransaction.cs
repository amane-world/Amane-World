using Newtonsoft.Json;
using RippleDotNet.Json.Converters;
using RippleDotNet.Model.Transaction.TransactionTypes;

namespace RippleDotNet.Model.Ledger
{
    [JsonConverter(typeof(TransactionOrHashConverter))]
    public class HashOrTransaction
    {
        public string TransactionHash { get; set; }

        public TransactionCommon Transaction { get; set; }
    }
}
