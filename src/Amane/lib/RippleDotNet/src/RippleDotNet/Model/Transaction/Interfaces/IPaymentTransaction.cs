﻿using System.Collections.Generic;

namespace RippleDotNet.Model.Transaction.Interfaces
{
    public interface IPaymentTransaction : ITransactionCommon
    {
        Currency Amount { get; set; }
        Currency DeliverMin { get; set; }
        string Destination { get; set; }
        uint? DestinationTag { get; set; }
        new PaymentFlags? Flags { get; set; }
        string InvoiceId { get; set; }
        List<List<Path>> Paths { get; set; }
        Currency SendMax { get; set; }
    }
}