﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RippleDotNet.Json.Converters;

namespace RippleDotNet.Model.Account
{
    public class AccountOffers
    {
        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("offers")]
        public List<Offer> Offers { get; set; }

        [JsonProperty("ledger_current_index")]
        public uint? LedgerCurrentIndex { get; set; }

        [JsonProperty("ledger_index")]
        public uint? LedgerIndex { get; set; }

        [JsonProperty("ledger_hash")]
        public string LedgerHash { get; set; }

        [JsonProperty("marker")]
        public object Marker { get; set; }
    }

    public class Offer
    {
        [JsonProperty("flags")]
        public uint Flags { get; set; }

        [JsonProperty("seq")]
        public uint Sequence { get; set; }

        [JsonProperty("taker_gets")]
        [JsonConverter(typeof(CurrencyConverter))]
        public Currency TakerGets { get; set; }

        [JsonProperty("taker_pays")]
        [JsonConverter(typeof(CurrencyConverter))]
        public Currency TakerPays { get; set; }

        [JsonProperty("quality")]
        public uint Quality { get; set; }

        [JsonProperty("expiration")]
        [JsonConverter(typeof(RippleDateTimeConverter))]
        public DateTime? Expiration { get; set; }
    }
}
