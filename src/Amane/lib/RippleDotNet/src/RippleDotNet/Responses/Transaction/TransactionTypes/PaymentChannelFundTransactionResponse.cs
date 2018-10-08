﻿using System;
using Newtonsoft.Json;
using RippleDotNet.Json.Converters;
using RippleDotNet.Model.Transaction.Interfaces;
using RippleDotNet.Responses.Transaction.Interfaces;

namespace RippleDotNet.Responses.Transaction.TransactionTypes
{
    public class PaymentChannelFundTransactionResponse : TransactionResponseCommon, IPaymentChannelFundTransaction
    {
        public string Amount { get; set; }

        public string Channel { get; set; }

        [JsonConverter(typeof(RippleDateTimeConverter))]
        public DateTime? Expiration { get; set; }
    }
}
