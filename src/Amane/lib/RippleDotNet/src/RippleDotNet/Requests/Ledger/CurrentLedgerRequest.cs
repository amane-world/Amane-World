﻿namespace RippleDotNet.Requests.Ledger
{
    public class CurrentLedgerRequest : RippleRequest
    {
        public CurrentLedgerRequest()
        {
            Command = "ledger_current";
        }
    }
}
