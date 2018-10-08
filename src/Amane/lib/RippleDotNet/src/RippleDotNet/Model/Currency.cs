﻿using System.Dynamic;
using System.Globalization;
using Newtonsoft.Json;
using RippleDotNet.Json.Converters;

namespace RippleDotNet.Model
{
    public class Currency
    {
        public Currency()
        {
            CurrencyCode = "XRP";
        }

        [JsonProperty("currency")]
        public string CurrencyCode { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("issuer")]
        public string Issuer { get; set; }

        [JsonIgnore]
        public decimal ValueAsNumber
        {
            get => string.IsNullOrEmpty(Value) ? 0 : decimal.Parse(Value);
            set => Value = value.ToString(CurrencyCode == "XRP" ? "G0" : "G15", CultureInfo.InvariantCulture);
        }

        [JsonIgnore]
        public decimal? ValueAsXrp
        {
            get
            {
                if (CurrencyCode != "XRP" || string.IsNullOrEmpty(Value))
                    return null;
                decimal val = decimal.Parse(Value);
                return val / 1000000;
            }
            set
            {
                if (value.HasValue)
                {
                    CurrencyCode = "XRP";
                    decimal val = value.Value * 1000000;
                    Value = val.ToString("G0", CultureInfo.InvariantCulture);
                }
                else
                {
                    Value = "0";
                }
            }
        }
    }
}
