﻿
using RippleDotNet.Model.Transaction.Interfaces;

namespace RippleDotNet.Model.Transaction.TransactionTypes
{
    public class AccountSetTransaction : TransactionCommon, IAccountSetTransaction
    {
        public AccountSetTransaction()
        {
            TransactionType = TransactionType.AccountSet;
        }

        public AccountSetTransaction(string account) : this()
        {
            Account = account;
        }

        public uint? ClearFlag { get; set; }

        public string Domain { get; set; }

        public string EmailHash { get; set; }

        public string MessageKey { get; set; }

        public uint? SetFlag { get; set; }

        public uint? TransferRate { get; set; }

        public uint? TickSize { get; set; }
    }


}
