using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bitstamp.Net.Objects
{
    public class BitstampOrder
    {
        [JsonProperty("microtimestamp")]
        public long Microtimestamp { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("order_type")]
        public bool OrderType { get; set; }

        [JsonProperty("amount_str")]
        public string AmountStr { get; set; }

        [JsonProperty("price_str")]
        public string PriceStr { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("datetime")]
        
        public long Datetime { get; set; }
    }
}
