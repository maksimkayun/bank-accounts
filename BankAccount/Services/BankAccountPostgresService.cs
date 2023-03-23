﻿using AutoMapper;
using BankAccount.DataStorage;
using BankAccount.DataStorage.PostgresModels;
using BankAccount.DTO;
using BankAccount.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankAccount.Services;

public class BankAccountPostgresService : IAccountService, IClientService, ITransactionsService
{
    private readonly BankAccountPgContext _context;
    private readonly IMapper _mapper;

    public BankAccountPostgresService(BankAccountPgContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public List<AccountDto> GetAccounts(int skip = 0, int take = 10) =>
        _context.Accounts.Skip(skip).Take(take)
            .AsEnumerable()
            .Select(e => _mapper.Map<AccountDto>(e))
            .ToList();

    public AccountDto GetAccountById(string id)
    {
        var account = _context.Accounts.SingleOrDefault(e => e.Id.ToString() == id);
        var accountDto = _mapper.Map<AccountDto>(account);
        return accountDto;
    }


    public AccountDto CreateAccount(AccountDto accountDto)
    {
        var account = _mapper.Map<Account>(accountDto);
        accountDto.Id = _context.Accounts.Add(account).Entity.Id.ToString();
        _context.SaveChanges();
        return accountDto;
    }

    public AccountDto UpdateAccount(string id, AccountDto accountDto)
    {
        accountDto.Id = id;
        var account = _mapper.Map<Account>(accountDto);
        _context.Accounts.Update(account);
        _context.SaveChanges();
        return accountDto;
    }

    public AccountDto DeleteAccount(string id)
    {
        var account = _context.Accounts.First(e => e.Id.ToString() == id);
        _context.Accounts.Remove(account);
        return _mapper.Map<AccountDto>(account);
    }

    public bool CreateCompositeIndex(string dbName, string schemaName, List<string> properties)
    {
        try
        {
            var key = $"{schemaName}_{string.Join("_", properties)}_index";
            var names = properties.ToList().ConvertAll(e => $"(\"{e}\")");
            var sqlEndQuery = string.Join($" INCLUDE ", names);
            FormattableString sqlQuery = $"CREATE INDEX \"{key}\" ON {schemaName} {sqlEndQuery}";
            _context.Database.ExecuteSqlInterpolated(sqlQuery);
            return true;
        }
        catch (Exception e)
        {
        }
        
        return false;
    }
    
    public void SeedCollectionAccounts()
    {
        var entities = new List<Account>();
        var closingDate = new List<DateTime?>()
        {
            DateTime.Now.ToUniversalTime(), DateTime.Now.AddYears(2).ToUniversalTime(), null
        };
        for (int i = 1; i <= 100000; i++)
        {
            var acc = new Account
            {
                AccountNumber = (100000 + i).ToString(),
                Balance = new Random().Next(0, 1000000),
                OpeningDate = DateTime.Now.AddYears(new Random().Next(-10, 0)).ToUniversalTime(),
                ClosingDate = closingDate[i % 3]
            };
            entities.Add(acc);    
        }
        _context.Accounts.AddRange(entities);
        _context.SaveChanges();
    }

    public List<ClientDto> GetClients(int skip = 0, int take = 10) =>
        _context.Clients.Skip(skip).Take(take)
            .AsEnumerable()
            .Select(e => _mapper.Map<ClientDto>(e))
            .ToList();

    public ClientDto GetClientById(string id) =>
        _mapper.Map<ClientDto>(_context.Clients.First(e => e.Id == int.Parse(id)));

    public ClientDto CreateClient(ClientDto clientDto)
    {
        var client = _mapper.Map<Client>(clientDto);
        clientDto.Id = _context.Clients.Add(client).Entity.Id.ToString();
        _context.SaveChanges();
        return clientDto;
    }

    public ClientDto UpdateClient(string id, ClientDto clientDto)
    {
        clientDto.Id = id;
        var client = _mapper.Map<Client>(clientDto);
        _context.Clients.Update(client);
        return clientDto;
    }

    public ClientDto DeleteClient(string id)
    {
        var client = _context.Clients.First(e => e.Id == int.Parse(id));
        var clientDto = _mapper.Map<ClientDto>(_context.Clients.Remove(client).Entity);
        _context.SaveChanges();
        return clientDto;
    }

    public List<TransactionDto> GetTransactions(int skip = 0, int take = 10) =>
        _context.Transactions.Skip(skip).Take(take)
            .AsEnumerable()
            .Select(e => _mapper.Map<TransactionDto>(e))
            .ToList();

    public TransactionDto GetTransactionById(string id) =>
        _mapper.Map<TransactionDto>(_context.Transactions.First(e => e.Id == int.Parse(id)));

    public TransactionDto CreateTransaction(TransactionDto transactionDto)
    {
        var transaction = _mapper.Map<Transaction>(transactionDto);
        transactionDto.Id = _context.Transactions.Add(transaction).Entity.Id.ToString();
        _context.SaveChanges();
        return transactionDto;
    }

    public TransactionDto UpdateTransaction(string id, TransactionDto transactionDto)
    {
        transactionDto.Id = id;
        var transaction = _mapper.Map<Transaction>(transactionDto);
        _context.Transactions.Update(transaction);
        return transactionDto;
    }

    public TransactionDto DeleteTransaction(string id)
    {
        var transaction = _context.Transactions.First(e => e.Id == int.Parse(id));
        var transactionDto = _mapper.Map<TransactionDto>(_context.Transactions.Remove(transaction).Entity);
        _context.SaveChanges();
        return transactionDto;
    }
}