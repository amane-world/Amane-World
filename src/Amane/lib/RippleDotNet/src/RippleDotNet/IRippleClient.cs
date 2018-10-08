﻿using System;
using System.Collections.Concurrent;
using System.Dynamic;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RippleDotNet.Exceptions;
using RippleDotNet.Model.Account;
using RippleDotNet.Model.Ledger;
using RippleDotNet.Model.Server;
using RippleDotNet.Model.Transaction.TransactionTypes;
using RippleDotNet.Requests;
using RippleDotNet.Requests.Account;
using RippleDotNet.Requests.Ledger;
using RippleDotNet.Requests.Transaction;
using RippleDotNet.Responses;
using RippleDotNet.Responses.Transaction;
using RippleDotNet.Responses.Transaction.Interfaces;
using RippleDotNet.Responses.Transaction.TransactionTypes;
using BookOffers = RippleDotNet.Model.Transaction.BookOffers;
using ChannelAuthorize = RippleDotNet.Model.Transaction.ChannelAuthorize;
using ChannelVerify = RippleDotNet.Model.Transaction.ChannelVerify;
using Submit = RippleDotNet.Model.Transaction.Submit;

namespace RippleDotNet
{
    public interface IRippleClient
    {
        void Connect();

        void Disconnect();

        Task Ping();

        Task<AccountCurrencies> AccountCurrencies(string account);

        Task<AccountCurrencies> AccountCurrencies(AccountCurrenciesRequest request);

        Task<AccountChannels> AccountChannels(string account);

        Task<AccountChannels> AccountChannels(AccountChannelsRequest request);

        Task<AccountInfo> AccountInfo(string account);

        Task<AccountInfo> AccountInfo(AccountInfoRequest request);

        /// <summary>
        /// The account_lines method returns information about an account's trust lines, including balances in all non-XRP currencies and assets.
        /// </summary>
        /// <param name="account">The account number to query.</param>
        /// <returns>An <see cref="Model.Account.AccountLines"/> response.</returns>
        Task<AccountLines> AccountLines(string account);

        /// <summary>
        /// The account_lines method returns information about an account's trust lines, including balances in all non-XRP currencies and assets.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>An <see cref="Model.Account.AccountLines"/> response.</returns>
        Task<AccountLines> AccountLines(AccountLinesRequest request);

        Task<AccountOffers> AccountOffers(string account);

        Task<AccountOffers> AccountOffers(AccountOffersRequest request);

        /// <summary>
        /// The AccountObjects command returns the raw ledger format for all objects owned by an account. For a higher-level view of an account's trust lines and balances, see <see cref="Model.Account.AccountLines"/> instead.
        /// </summary>
        /// <param name="account"></param>
        /// <returns>An <see cref="Model.Account.AccountObjects"/> response.</returns>
        Task<AccountObjects> AccountObjects(string account);

        /// <summary>
        /// The AccountObjects command returns the raw ledger format for all objects owned by an account. For a higher-level view of an account's trust lines and balances, see <see cref="Model.Account.AccountLines"/> instead.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>An <see cref="Model.Account.AccountObjects"/> response.</returns>
        Task<AccountObjects> AccountObjects(AccountObjectsRequest request);

        Task<AccountTransactions> AccountTransactions(string account);

        Task<AccountTransactions> AccountTransactions(AccountTransactionsRequest request);

        Task<NoRippleCheck> NoRippleCheck(string account);

        Task<NoRippleCheck> NoRippleCheck(NoRippleCheckRequest request);

        Task<GatewayBalances> GatewayBalances(string account);

        Task<GatewayBalances> GatewayBalances(GatewayBalancesRequest request);

        Task<ITransactionResponseCommon> Transaction(string transaction);

        Task<IBaseTransactionResponse> TransactionAsBinary(string transaction);

        Task<ServerInfo> ServerInfo();

        Task<Fee> Fees();

        Task<ChannelAuthorize> ChannelAuthorize(ChannelAuthorizeRequest request);

        Task<ChannelVerify> ChannelVerify(ChannelVerifyRequest request);

        Task<Submit> SubmitTransactionBlob(SubmitBlobRequest request);

        Task<Submit> SubmitTransaction(SubmitRequest request);

        Task<BookOffers> BookOffers(BookOffersRequest request);

        Task<Ledger> Ledger(LedgerRequest request);

        Task<BaseLedgerInfo> ClosedLedger();

        Task<LedgerCurrentIndex> CurrentLedger();

        Task<LedgerData> LedgerData(LedgerDataRequest request);
    }

    public class RippleClient : IRippleClient
    {
        private readonly WebSocketClient client;
        private readonly ConcurrentDictionary<Guid, TaskInfo> tasks;
        private readonly JsonSerializerSettings serializerSettings;

        public RippleClient(string url)
        {
            tasks = new ConcurrentDictionary<Guid, TaskInfo>();
            serializerSettings = new JsonSerializerSettings();
            serializerSettings.NullValueHandling = NullValueHandling.Ignore;
            serializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            
            client = WebSocketClient.Create(url);
            client.OnMessageReceived(MessageReceived);
            client.OnConnectionError(Error);            
        }

        public void Connect()
        {
            client.Connect();
            do
            {
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            } while (client.State != WebSocketState.Open);
        }

        public void Disconnect()
        {
            client.Disconnect();
        }

        public Task Ping()
        {
            RippleRequest request = new RippleRequest();
            request.Command = "ping";

            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<object> task = new TaskCompletionSource<object>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(object);
            
            tasks.TryAdd(request.Id, taskInfo);
            
            client.SendMessage(command);
            return task.Task;
        }

        public Task<AccountCurrencies> AccountCurrencies(string account)
        {
            AccountCurrenciesRequest request = new AccountCurrenciesRequest(account);
            return AccountCurrencies(request);
        }

        public Task<AccountCurrencies> AccountCurrencies(AccountCurrenciesRequest request)
        {
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<AccountCurrencies> task = new TaskCompletionSource<AccountCurrencies>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(AccountCurrencies);
            
            tasks.TryAdd(request.Id, taskInfo);

            client.SendMessage(command);
            return task.Task;
        }

        public Task<AccountChannels> AccountChannels(string account)
        {
            AccountChannelsRequest request = new AccountChannelsRequest(account);
            return AccountChannels(request);
        }

        public Task<AccountChannels> AccountChannels(Requests.Account.AccountChannelsRequest request)
        {
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<AccountChannels> task = new TaskCompletionSource<AccountChannels>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(AccountChannels);
            
            tasks.TryAdd(request.Id, taskInfo);

            client.SendMessage(command);
            return task.Task;
        }

        public Task<AccountInfo> AccountInfo(string account)
        {
            AccountInfoRequest request = new AccountInfoRequest(account);
            return AccountInfo(request);
        }

        public Task<AccountInfo> AccountInfo(AccountInfoRequest request)
        {
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<AccountInfo> task = new TaskCompletionSource<AccountInfo>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(AccountInfo);
            
            tasks.TryAdd(request.Id, taskInfo);

            client.SendMessage(command);
            return task.Task;
        }

        public Task<AccountLines> AccountLines(string account)
        {
            AccountLinesRequest request = new AccountLinesRequest(account);
            return AccountLines(request);
        }

        public Task<AccountLines> AccountLines(AccountLinesRequest request)
        {
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<AccountLines> task = new TaskCompletionSource<AccountLines>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(AccountLines);
            
            tasks.TryAdd(request.Id, taskInfo);

            client.SendMessage(command);
            return task.Task;
        }

        public Task<AccountOffers> AccountOffers(string account)
        {
            AccountOffersRequest request = new AccountOffersRequest(account);
            return AccountOffers(request);
        }

        public Task<AccountOffers> AccountOffers(AccountOffersRequest request)
        {
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<AccountOffers> task = new TaskCompletionSource<AccountOffers>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(AccountOffers);
            
            tasks.TryAdd(request.Id, taskInfo);

            client.SendMessage(command);
            return task.Task;
        }

        public Task<AccountObjects> AccountObjects(string account)
        {
            AccountObjectsRequest request = new AccountObjectsRequest(account);
            return AccountObjects(request);
        }

        public Task<AccountObjects> AccountObjects(AccountObjectsRequest request)
        {
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<AccountObjects> task = new TaskCompletionSource<AccountObjects>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(AccountObjects);
            
            tasks.TryAdd(request.Id, taskInfo);

            client.SendMessage(command);
            return task.Task;
        }

        public Task<AccountTransactions> AccountTransactions(string account)
        {
            AccountTransactionsRequest request = new AccountTransactionsRequest(account);
            return AccountTransactions(request);
        }

        public Task<AccountTransactions> AccountTransactions(AccountTransactionsRequest request)
        {
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<AccountTransactions> task = new TaskCompletionSource<AccountTransactions>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(AccountTransactions);
            
            tasks.TryAdd(request.Id, taskInfo);

            client.SendMessage(command);
            return task.Task;
        }

        public Task<NoRippleCheck> NoRippleCheck(string account)
        {
            NoRippleCheckRequest request = new NoRippleCheckRequest(account);
            return NoRippleCheck(request);
        }

        public Task<NoRippleCheck> NoRippleCheck(NoRippleCheckRequest request)
        {
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<NoRippleCheck> task = new TaskCompletionSource<NoRippleCheck>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(NoRippleCheck);
            
            tasks.TryAdd(request.Id, taskInfo);

            client.SendMessage(command);
            return task.Task;
        }

        public Task<GatewayBalances> GatewayBalances(string account)
        {
            GatewayBalancesRequest request = new GatewayBalancesRequest(account);
            return GatewayBalances(request);
        }

        public Task<GatewayBalances> GatewayBalances(GatewayBalancesRequest request)
        {
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<GatewayBalances> task = new TaskCompletionSource<GatewayBalances>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(GatewayBalances);
            
            tasks.TryAdd(request.Id, taskInfo);

            client.SendMessage(command);
            return task.Task;
        }

        public Task<ITransactionResponseCommon> Transaction(string transaction)
        {
            TransactionRequest request = new TransactionRequest(transaction);
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<ITransactionResponseCommon> task = new TaskCompletionSource<ITransactionResponseCommon>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(TransactionResponseCommon);

            tasks.TryAdd(request.Id, taskInfo);

            client.SendMessage(command);
            return task.Task;
        }

        public Task<IBaseTransactionResponse> TransactionAsBinary(string transaction)
        {
            TransactionRequest request = new TransactionRequest(transaction);
            request.Binary = true;
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<IBaseTransactionResponse> task = new TaskCompletionSource<IBaseTransactionResponse>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(BinaryTransactionResponse);

            tasks.TryAdd(request.Id, taskInfo);

            client.SendMessage(command);
            return task.Task;

        }

        public Task<ServerInfo> ServerInfo()
        {
            RippleRequest request = new RippleRequest();
            request.Command = "server_info";

            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<ServerInfo> task = new TaskCompletionSource<ServerInfo>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(ServerInfo);
            
            tasks.TryAdd(request.Id, taskInfo);

            client.SendMessage(command);
            return task.Task;
        }

        public Task<Fee> Fees()
        {
            RippleRequest request = new RippleRequest();
            request.Command = "fee";

            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<Fee> task = new TaskCompletionSource<Fee>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(Fee);
            
            tasks.TryAdd(request.Id, taskInfo);

            client.SendMessage(command);
            return task.Task;
        }

        public Task<ChannelAuthorize> ChannelAuthorize(ChannelAuthorizeRequest request)
        {
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<ChannelAuthorize> task = new TaskCompletionSource<ChannelAuthorize>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(ChannelAuthorize);
            
            tasks.TryAdd(request.Id, taskInfo);

            client.SendMessage(command);
            return task.Task;
        }

        public Task<ChannelVerify> ChannelVerify(ChannelVerifyRequest request)
        {
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<ChannelVerify> task = new TaskCompletionSource<ChannelVerify>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(ChannelVerify);
            
            tasks.TryAdd(request.Id, taskInfo);

            client.SendMessage(command);
            return task.Task;
        }

        public Task<Submit> SubmitTransactionBlob(SubmitBlobRequest request)
        {
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<Submit> task = new TaskCompletionSource<Submit>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(Submit);
            
            tasks.TryAdd(request.Id, taskInfo);

            client.SendMessage(command);
            return task.Task;
        }

        public Task<Submit> SubmitTransaction(SubmitRequest request)
        {
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<Submit> task = new TaskCompletionSource<Submit>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(Submit);
            
            tasks.TryAdd(request.Id, taskInfo);

            client.SendMessage(command);
            return task.Task;
        }

        public Task<BookOffers> BookOffers(BookOffersRequest request)
        {
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<BookOffers> task = new TaskCompletionSource<BookOffers>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(BookOffers);
            
            tasks.TryAdd(request.Id, taskInfo);

            client.SendMessage(command);
            return task.Task;
        }

        public Task<Ledger> Ledger(LedgerRequest request)
        {
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<Ledger> task = new TaskCompletionSource<Ledger>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(Ledger);

            tasks.TryAdd(request.Id, taskInfo);

            client.SendMessage(command);
            return task.Task;
        }

        public Task<BaseLedgerInfo> ClosedLedger()
        {
            ClosedLedgerRequest request = new ClosedLedgerRequest();
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<BaseLedgerInfo> task = new TaskCompletionSource<BaseLedgerInfo>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(BaseLedgerInfo);

            tasks.TryAdd(request.Id, taskInfo);

            client.SendMessage(command);
            return task.Task;

        }

        public Task<LedgerCurrentIndex> CurrentLedger()
        {
            CurrentLedgerRequest request = new CurrentLedgerRequest();
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<LedgerCurrentIndex> task = new TaskCompletionSource<LedgerCurrentIndex>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(LedgerCurrentIndex);

            tasks.TryAdd(request.Id, taskInfo);

            client.SendMessage(command);
            return task.Task;
        }

        public Task<LedgerData> LedgerData(LedgerDataRequest request)
        {
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<LedgerData> task = new TaskCompletionSource<LedgerData>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(LedgerData);

            tasks.TryAdd(request.Id, taskInfo);

            client.SendMessage(command);
            return task.Task;
        }

        private void Error(Exception ex, WebSocketClient client)
        {
            throw new Exception(ex.Message, ex);            
        }

        private void MessageReceived(string s, WebSocketClient client)
        {
            RippleResponse response = JsonConvert.DeserializeObject<RippleResponse>(s);

            var taskInfoResult = tasks.TryGetValue(response.Id, out var taskInfo);
            if (taskInfoResult == false) throw new Exception("Task not found");

            if (response.Status == "success")
            {
                var deserialized = JsonConvert.DeserializeObject(response.Result.ToString(), taskInfo.Type, serializerSettings);

                var setResult = taskInfo.TaskCompletionResult.GetType().GetMethod("SetResult");
                setResult.Invoke(taskInfo.TaskCompletionResult, new[] { deserialized });

                if (taskInfo.RemoveUponCompletion)
                {
                    tasks.TryRemove(response.Id, out taskInfo);
                }
            }
            else if (response.Status == "error")
            {
                var setException = taskInfo.TaskCompletionResult.GetType().GetMethod("SetException", new Type[]{typeof(Exception)}, null);

                RippleException exception = new RippleException(response.Error);
                setException.Invoke(taskInfo.TaskCompletionResult, new[] { exception });

                tasks.TryRemove(response.Id, out taskInfo);
            }                        
        }        
    }
}
