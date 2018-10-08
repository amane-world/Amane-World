﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RippleDotNet.Model.Ledger;

namespace RippleDotNet.Json.Converters
{
    public class LedgerBinaryConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public object Create(Type objectType, JObject jObject)
        {
            JToken ledgerDataToken = jObject.Property("ledger_data");
            if (ledgerDataToken == null)
            {
                return new LedgerEntity();
            }
            return new LedgerBinaryEntity();            
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);
            var target = Create(objectType, jObject);
            serializer.Populate(jObject.CreateReader(), target);
            return target;            
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}
