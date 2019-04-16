using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bitstamp.Net.Objects
{
    //{"event": "bts:subscribe", "data": { "channel": "live_orders_btcusd" }  }
    public class RequestBase
    {
        public RequestBase(string channel, bool isConnect=true)
        {
            string @event = isConnect ? "bts:subscribe" : "bts:unsubscribe";
            Event = @event;
            data = new RequestData(channel);
        }
        [JsonProperty("event")]
        public string Event { get; set; }
        public RequestData data { get; set; }
    }
    public class RequestData
    {
        public RequestData(string channel)
        {
            this.channel = channel;
        }

        public string channel { get; set; }
    }
}
