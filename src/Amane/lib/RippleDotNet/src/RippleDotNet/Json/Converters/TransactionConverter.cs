﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RippleDotNet.Model.Transaction.TransactionTypes;
using RippleDotNet.Responses.Transaction.Interfaces;
using RippleDotNet.Responses.Transaction.TransactionTypes;

namespace RippleDotNet.Json.Converters
{
    public class TransactionConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public ITransactionResponseCommon Create(Type objectType, JObject jObject)
        {
            JProperty transactionType = jObject.Property("TransactionType");
            
            switch (transactionType.Value.ToString())
            {
                case "AccountSet":
                    return new AccountSetTransactionResponse();
                case "EscrowCancel":
                    return new EscrowCancelTransactionResponse();
                case "EscrowCreate":
                    return new EscrowCreateTransactionResponse();
                case "EscrowFinish":
                    return new EscrowFinishTransactionResponse();
                case "OfferCancel":
                    return new OfferCancelTransactionResponse();
                case "OfferCreate":
                    return new OfferCreateTransactionResponse();
                case "Payment":
                    return new PaymentTransactionResponse();
                case "PaymentChannelClaim":
                    return new PaymentChannelClaimTransactionResponse();
                case "PaymentChannelCreate":
                    return new PaymentChannelCreateTransactionResponse();
                case "PaymentChannelFund":
                    return new PaymentChannelFundTransactionResponse();
                case "SetRegularKey":
                    return new SetRegularKeyTransactionResponse();
                case "SignerListSet":
                    return new SignerListSetTransactionResponse();
                case "TrustSet":
                    return new TrustSetTransactionResponse();

                case "EnableAmendment":
                    return new EnableAmendmentTransactionResponse();
                case "SetFee":
                    return new SetFeeTransactionResponse();
            }
            throw new Exception("Can't create transaction type" + transactionType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);
            ITransactionResponseCommon transactionCommon = Create(objectType, jObject);
            serializer.Populate(jObject.CreateReader(), transactionCommon);
            return transactionCommon;            
        }

        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(ITransactionResponseCommon))
                return true;
            return false;
        }

        public override bool CanWrite => false;
    }
}
