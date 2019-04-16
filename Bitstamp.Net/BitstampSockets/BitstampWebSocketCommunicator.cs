using System;
using System.Net.WebSockets;
using Websocket.Client;

namespace Bitstamp.Net.BitstampSockets
{

    public class BitstampWebSocketCommunicator : WebsocketClient, IBitstampCommunicator
    {
        /// <inheritdoc />
        public BitstampWebSocketCommunicator(Uri url, Func<ClientWebSocket> clientFactory = null)
            : base(url, clientFactory)
        {
        }
    }
}
