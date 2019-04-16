using Bitstamp.Net.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Websocket.Client;

namespace Bitstamp.Net.BitstampSockets.Client
{
    public class BitstampWebSocketClient : IDisposable
    {
     //   private static readonly ILog Log = LogProvider.GetCurrentClassLogger();

        private readonly IBitstampCommunicator _communicator;
        private readonly IDisposable _messageReceivedSubscription;

        /// <inheritdoc />
        public BitstampWebSocketClient(IBitstampCommunicator communicator)
        {
         //   BmxValidations.ValidateInput(communicator, nameof(communicator));

            _communicator = communicator;
            _messageReceivedSubscription = _communicator.MessageReceived.Subscribe(HandleMessage);
        }

        /// <summary>
        /// Provided message streams
        /// </summary>
        public BitstampSocketClientStreams Streams { get; } = new BitstampSocketClientStreams();

        /// <summary>
        /// Cleanup everything
        /// </summary>
        public void Dispose()
        {
            _messageReceivedSubscription?.Dispose();
        }

        /// <summary>
        /// Serializes request and sends message via websocket communicator. 
        /// It logs and re-throws every exception. 
        /// </summary>
        /// <param name="request">Request/message to be sent</param>
        public async Task Send<T>(T request) where T : RequestBase
        {
            try
            {
                //BmxValidations.ValidateInput(request, nameof(request));

                var serialized = JsonConvert.SerializeObject(request);
                await _communicator.Send(serialized).ConfigureAwait(false);
            }
            catch (Exception e)
            {
              //  Log.Error(e, L($"Exception while sending message '{request}'. Error: {e.Message}"));
                Console.WriteLine($"Exception while sending message '{request}'. Error: {e.Message}");
                throw;
            }
        }

        /// <summary>
        /// Sends authentication request via websocket communicator
        /// </summary>
        /// <param name="apiKey">Your API key</param>
        /// <param name="apiSecret">Your API secret</param>
        //public Task Authenticate(string apiKey, string apiSecret)
        //{
        //    return Send(new AuthenticationRequest(apiKey, apiSecret));
        //}

        private string L(string msg)
        {
            return $"[BMX WEBSOCKET CLIENT] {msg}";
        }

        private void HandleMessage(ResponseMessage message)
        {
            try
            {
                bool handled;
                var messageSafe = (message.Text ?? string.Empty).Trim();

                if (messageSafe.StartsWith("{"))
                {
                    handled = HandleObjectMessage(messageSafe);
                    if (handled)
                        return;
                }

                //handled = HandleRawMessage(messageSafe);
                //if (handled)
                //    return;

                //Log.Warn(L($"Unhandled response:  '{messageSafe}'"));
                Console.WriteLine(messageSafe);
            }
            catch (Exception e)
            {
                //Log.Error(e, L("Exception while receiving message"));
                Console.WriteLine(e);
            }
        }

        //private bool HandleRawMessage(string msg)
        //{
        //    // ********************
        //    // ADD RAW HANDLERS BELOW
        //    // ********************

        //    return
        //        PongResponse.TryHandle(msg, Streams.PongSubject);
        //}

        private bool HandleObjectMessage(string msg)
        {
            var response = JsonConvert.DeserializeObject<JObject>(msg);

            // ********************
            // ADD OBJECT HANDLERS BELOW
            // ********************

            return

                BistampSocketBase<BitstampOrder>.TryHandle(response, Streams.OrdersSubject); //||
                //TradeBinResponse.TryHandle(response, Streams.TradeBinSubject) ||
                //BookResponse.TryHandle(response, Streams.BookSubject) ||
                //QuoteResponse.TryHandle(response, Streams.QuoteSubject) ||
                //LiquidationResponse.TryHandle(response, Streams.LiquidationSubject) ||
                //PositionResponse.TryHandle(response, Streams.PositionSubject) ||
                //MarginResponse.TryHandle(response, Streams.MarginSubject) ||
                //OrderResponse.TryHandle(response, Streams.OrderSubject) ||
                //WalletResponse.TryHandle(response, Streams.WalletSubject) ||
                //InstrumentResponse.TryHandle(response, Streams.InstrumentSubject) ||


                //ErrorResponse.TryHandle(response, Streams.ErrorSubject) ||
                //SubscribeResponse.TryHandle(response, Streams.SubscribeSubject) ||
                //InfoResponse.TryHandle(response, Streams.InfoSubject) ||
                //AuthenticationResponse.TryHandle(response, Streams.AuthenticationSubject);
        }
    }
}
