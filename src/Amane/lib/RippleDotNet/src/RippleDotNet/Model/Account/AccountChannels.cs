﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using RippleDotNet.Json.Converters;

namespace RippleDotNet.Model.Account
{
    public class AccountChannels
    {
        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("channels")]
        public List<Channel> Channels { get; set; }

        [JsonProperty("limit")]
        public int? Limit { get; set; }

        [JsonProperty("marker")]
        public object Marker { get; set; }
    }

    public class Channel
    {
        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("balance")]
        public string Balance { get; set; }

        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }

        [JsonProperty("destination_account")]
        public string DestinationAccount { get; set; }

        [JsonProperty("public_key")]
        public string PublicKey { get; set; }

        [JsonProperty("public_key_hex")]
        public string PublicKeyHex { get; set; }

        [JsonProperty("settle_delay")]
        public uint SettleDelay { get; set; }

        [JsonProperty("expiration")]
        [JsonConverter(typeof(RippleDateTimeConverter))]
        public DateTime? Expiration { get; set; }

        [JsonProperty("cancel_after")]
        [JsonConverter(typeof(RippleDateTimeConverter))]
        public DateTime? CancelAfter { get; set; }

        [JsonProperty("source_tag")]
        public uint? SourceTag { get; set; }

        [JsonProperty("destination_tag")]
        public uint? DestinationTag { get; set; }        
    }
}
