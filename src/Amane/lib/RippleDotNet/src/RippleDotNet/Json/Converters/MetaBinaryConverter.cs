﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using RippleDotNet.Model.Transaction.TransactionTypes;

namespace RippleDotNet.Json.Converters
{
    public class MetaBinaryConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;
            Meta meta = new Meta();
            if (reader.TokenType == JsonToken.String)
            {
                meta.MetaBlob = reader.Value.ToString();
            }
            else if (reader.TokenType == JsonToken.StartObject)
            {
                meta = serializer.Deserialize<Meta>(reader);
            }
            return meta;
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}
