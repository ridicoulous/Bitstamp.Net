using Bitstamp.Net.Objects;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;

namespace Bitstamp.Net.BitstampSockets
{
    public class BitstampSocketClientStreams
    {
        internal readonly Subject<BistampSocketBase<BitstampOrder>> OrdersSubject = new Subject<BistampSocketBase<BitstampOrder>>();
        public IObservable<BistampSocketBase<BitstampOrder>> TradesStream => OrdersSubject.AsObservable();

    }
}
