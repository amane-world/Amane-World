﻿using RippleDotNet.Model.Transaction.Interfaces;
using RippleDotNet.Responses.Transaction.Interfaces;

namespace RippleDotNet.Responses.Transaction.TransactionTypes
{
    public class AccountSetTransactionResponse : TransactionResponseCommon, IAccountSetTransaction
    {
        public uint? ClearFlag { get; set; }
        public string Domain { get; set; }
        public string EmailHash { get; set; }
        public string MessageKey { get; set; }
        public uint? SetFlag { get; set; }
        public uint? TransferRate { get; set; }
        public uint? TickSize { get; set; }        
    }
}
