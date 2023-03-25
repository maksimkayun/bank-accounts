﻿using System.Dynamic;
using AutoMapper;
using BankAccount.DataStorage;
using BankAccount.DataStorage.MongoModels;
using BankAccount.DTO;
using BankAccount.Interfaces;
using BankAccount.Requests;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BankAccount.Services;

public class BankAccountMongoService : IAccountService, IClientService, ITransactionsService
{
    private readonly BsonDocument _document;
    private readonly BankAccountMongoContext _context;
    private readonly IMapper _mapper;

    public BankAccountMongoService(BankAccountMongoContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public List<AccountDto> GetAccounts(int skip = 0, int take = 10) =>
        _context.Accounts.FindSync(_ => true, new FindOptions<Account>
            {
                Limit = take,
                Skip = skip
            }).ToEnumerable()
            .Select(e => _mapper.Map<AccountDto>(e))
            .ToList();

    public AccountDto GetAccountById(string id) =>
        _mapper.Map<AccountDto>(_context.Accounts.Find(e => e.Id == id).FirstOrDefault());

    public AccountDto CreateAccount(AccountDto accountDto)
    {
        var account = _mapper.Map<Account>(accountDto);
        _context.Accounts.InsertOne(account);
        accountDto.Id = account.Id;
        return accountDto;
    }

    public AccountDto UpdateAccount(string id, AccountDto accountDto)
    {
        accountDto.Id = id;
        var account = _mapper.Map<Account>(accountDto);
        _context.Accounts.FindOneAndReplace(e => e.Id == id, account);
        return accountDto;
    }

    public AccountDto DeleteAccount(string id)
    {
        var account = _context.Accounts.FindOneAndDelete(e => e.Id == id);
        return _mapper.Map<AccountDto>(account);
    }

    public bool CreateCompositeIndex(string dbName, string schemaName, List<string> properties)
    {
        throw new NotSupportedException("The method CreateCompositeIndex is not supported for MongoDB");
    }

    public List<ClientDto> GetClients(int skip = 0, int take = 10)
    {
        var clients = _context.Clients.FindSync(_ => true, new FindOptions<Client>
            {
                Limit = take,
                Skip = skip
            }).ToEnumerable()
            .Select(e => _mapper.Map<ClientDto>(e))
            .ToList();
        for (int i = 0; i < clients.Count; i++)
        {
            var ownerId = clients[i].Id;
            clients[i].AccountIds = _context.Accounts
                .FindSync(e => e.Owner == ownerId)
                .ToEnumerable()
                .Select(e => e.Id)
                .ToList();
        }

        return clients;
    }


    public ClientDto GetClientById(string id) =>
        _mapper.Map<ClientDto>(_context.Clients.FindSync(e => e.Id == id).FirstOrDefault());

    public ClientDto CreateClient(ClientDto clientDto)
    {
        var client = _mapper.Map<Client>(clientDto);
        _context.Clients.InsertOne(client);
        clientDto.Id = client.Id;
        return clientDto;
    }

    public ClientDto UpdateClient(string id, ClientDto clientDto)
    {
        clientDto.Id = id;
        var client = _mapper.Map<Client>(clientDto);
        _context.Clients.FindOneAndReplace(e => e.Id == id, client);
        return clientDto;
    }

    public ClientDto DeleteClient(string id)
    {
        var client = _context.Clients.FindOneAndDelete(e => e.Id == id);
        return _mapper.Map<ClientDto>(client);
    }

    public TransactionDto MakeTransaction(SendMoneyRequest request)
    {
        throw new NotImplementedException();
    }

    public List<TransactionsInfoDto> GetTransactionsByClientId(GetTransactionsByClientIdRequest request)
    {
        var transactionsInfo = _context.Accounts.Aggregate()
            .Match(e => e.Owner == request.ClientId)
            .Lookup("transactions", "account_number", "sender_account_number", @as: "outgoing_transactions_info")
            .Lookup("transactions", "account_number", "recipient_account_number", @as: "incoming_transactions_info")
            .As<TransactionsInfoModel>()
            .ToList()
            .Select(e => new TransactionsInfoDto
            {
                AccountNumber = e.AccountNumber,
                OutgoingTransactionsInfo = e.OutgoingTransactionsInfo.ConvertAll(t=>_mapper.Map<TransactionDto>(t)),
                IncomingTransactionsInfo = e.IncomingTransactionsInfo.ConvertAll(t=>_mapper.Map<TransactionDto>(t))
            }).ToList();
        
        // var incoming = _context.Accounts.Aggregate()
        //     .Match(e => e.Owner == request.ClientId)
        //     .Lookup("transactions", "account_number", "recipient_account_number", @as: "incoming_transactions_info")
        //     .As<TransactionsInfoModel>()
        //     .ToList();
        return transactionsInfo;
    }

    public List<TransactionDto> GetTransactions(int skip = 0, int take = 10) =>
        _context.Transactions.FindSync(_ => true, new FindOptions<Transaction>
            {
                Limit = take,
                Skip = skip
            }).ToEnumerable()
            .Select(e => _mapper.Map<TransactionDto>(e))
            .ToList();

    public TransactionDto GetTransactionById(string id) =>
        _mapper.Map<TransactionDto>(_context.Transactions.FindSync(e => e.Id == id).FirstOrDefault());

    public TransactionDto CreateTransaction(TransactionDto transactionsDto)
    {
        var transaction = _mapper.Map<Transaction>(transactionsDto);
        _context.Transactions.InsertOne(transaction);
        transactionsDto.Id = transaction.Id;
        return transactionsDto;
    }

    public TransactionDto UpdateTransaction(string id, TransactionDto transactionsDto)
    {
        transactionsDto.Id = id;
        var transaction = _mapper.Map<Transaction>(transactionsDto);
        _context.Transactions.ReplaceOne(e => e.Id == id, transaction);
        return transactionsDto;
    }

    public TransactionDto DeleteTransaction(string id)
    {
        var transaction = _context.Transactions.FindOneAndDelete(e => e.Id == id);
        return _mapper.Map<TransactionDto>(transaction);
    }
}