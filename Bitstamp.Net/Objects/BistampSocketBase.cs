using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reactive.Subjects;

namespace Bitstamp.Net.Objects
{
    public class BistampSocketBase<T>: BistampBase
    {
        [JsonProperty("data")]
        public T Data { get; set; }

        //[JsonProperty("event")]
        //public string Event { get; set; }

        //[JsonProperty("channel")]
        //public string Channel { get; set; }
        internal static bool TryHandle(JObject response, ISubject<BistampSocketBase<T>> subject)
        {
            try
            {
                var parsed = response.ToObject<BistampSocketBase<T>>();
                subject.OnNext(parsed);
                return true;
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex);
                return false;

            }
        }
    }
    public class BistampBase
    {     

        [JsonProperty("event")]
        public string Event { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; }
        internal static bool TryHandle(JObject response, ISubject<BistampBase> subject)
        {
            try
            {
                var parsed = response.ToObject<BistampBase>();
                subject.OnNext(parsed);
                return true;
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex);
                return false;

            }
        }
    }
}
