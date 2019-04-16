using Bitstamp.Net.BitstampSockets;
using Bitstamp.Net.BitstampSockets.Client;
using Bitstamp.Net.Objects;
using System;
using System.Linq;
using System.Threading;

namespace Bitstamp.ConsoleApp
{
    class Program
    {
        private static readonly ManualResetEvent ExitEvent = new ManualResetEvent(false);

        static void Main(string[] args)
        {
            var url = "wss://ws.bitstamp.net";
            using (var communicator = new BitstampWebSocketCommunicator(new Uri(url)))
            {
                communicator.Name = "Bitstamp1";
                communicator.ReconnectTimeoutMs = (int)TimeSpan.FromMinutes(10).TotalMilliseconds;
                //communicator.ReconnectionHappened.Subscribe(type =>
                //    Log.Information($"Reconnection happened, type: {type}"));

                using (var client = new BitstampWebSocketClient(communicator))
                {

                    //client.Streams.TradesStream.Subscribe(info =>
                    //{
                    // //   Log.Information($"Reconnection happened, Message: {info.Info}, Version: {info.Version:D}");
                    //    SendSubscriptionRequests(client).Wait();
                    //});
                    client.Send(new RequestBase("live_orders_btcusd")).ConfigureAwait(false);
                    client.Streams.TradesStream.Subscribe(y =>
                y.Data.ToList().ForEach(x =>
                Console.WriteLine(y.Event +
                        $" Order {x.Amount} {x.Id} updated. Status: {x.Id}"))
            );


                    communicator.Start();

                    ExitEvent.WaitOne();
                }
            }

        }
    }
}
