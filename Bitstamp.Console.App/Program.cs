using Bitstamp.Net.BitstampSockets;
using Bitstamp.Net.BitstampSockets.Client;
using Bitstamp.Net.Objects;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
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
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
                communicator.Name = "Bitstamp1";
                communicator.ReconnectTimeoutMs = (int)TimeSpan.FromMinutes(10).TotalMilliseconds;
                //communicator.ReconnectionHappened.Subscribe(type =>
                //    Log.Information($"Reconnection happened, type: {type}"));
                InitLogging();
                using (var client = new BitstampWebSocketClient(communicator))
                {

                    //client.Streams.TradesStream.Subscribe(info =>
                    //{
                    // //   Log.Information($"Reconnection happened, Message: {info.Info}, Version: {info.Version:D}");
                    //    SendSubscriptionRequests(client).Wait();
                    //});
                    var r = new RequestBase("live_orders_btcusd");
                    Console.WriteLine(JsonConvert.SerializeObject(r));
                    client.Send(r).ConfigureAwait(false);
                    client.Streams.TradesStream.Subscribe(b =>
                    
                   Log.Fatal($"{b.Data.Id},{(long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds},{b.Data.Microtimestamp / 1000},{b.Data.Price},{b.Data.Amount},{b.Event.Replace("order_","")},{(b.Data.OrderType?"bid":"ask")}"));


                    communicator.Start();

                    ExitEvent.WaitOne();
                }
            }


        }
        private static void InitLogging()
        {
            var executingDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var logPath = Path.Combine(executingDir, "logs", "bitstamp.csv");
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Fatal()
                .WriteTo.File(logPath, rollingInterval: RollingInterval.Day, outputTemplate: "{Message:lj}{NewLine}")
                //.WriteTo(LogEventLevel.Information)
                .CreateLogger();
        }
    }
}
