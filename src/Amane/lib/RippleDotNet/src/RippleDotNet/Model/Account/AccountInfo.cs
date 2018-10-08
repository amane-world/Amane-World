﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RippleDotNet.Model.Ledger;
using RippleDotNet.Model.Ledger.Objects;

namespace RippleDotNet.Model.Account
{
    public class AccountInfo
    {
        [JsonProperty("account_data")]
        public AccountRootLedgerObject AccountData { get; set; }

        [JsonProperty("ledger_current_index")]
        public int LedgerCurrentIndex { get; set; }

        [JsonProperty("queue_data")]
        public QueueData QueueData { get; set; }

        [JsonProperty("validated")]
        public bool Validated { get; set; }
    }

    public class AccountTransaction
    {
        [JsonProperty("auth_change")]
        public bool AuthChange { get; set; }

        [JsonProperty("fee")]
        public string Fee { get; set; }

        [JsonProperty("fee_level")]
        public string FeeLevel { get; set; }

        [JsonProperty("max_spend_drops")]
        public string MaxSpendDrops { get; set; }

        [JsonProperty("seq")]
        public int Sequence { get; set; }

        public int? LastLedgerSequence { get; set; }
    }

    public class QueueData
    {
        [JsonProperty("auth_change_queued")]
        public bool AuthChangeQueued { get; set; }

        [JsonProperty("highest_sequence")]
        public int HighestSequence { get; set; }

        [JsonProperty("lowest_sequence")]
        public int LowestSequence { get; set; }

        [JsonProperty("max_spend_drops_total")]
        public string MaxSpendDropsTotal { get; set; }

        [JsonProperty("transactions")]
        public List<AccountTransaction> Transactions { get; set; }

        [JsonProperty("txn_count")]
        public int TxnCount { get; set; }
    }
}
