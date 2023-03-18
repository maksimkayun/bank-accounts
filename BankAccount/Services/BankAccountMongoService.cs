using BankAccount.DataStorage;
using BankAccount.DataStorage.MongoModels;
using BankAccount.DTO;
using BankAccount.Interfaces;
using Mapper;
using MongoDB.Driver;

namespace BankAccount.Services;

public class BankAccountMongoService : IAccountService, IClientService, ITransactionsService
{
    private readonly BankAccountMongoContext _context;

    public BankAccountMongoService(BankAccountMongoContext context)
    {
        _context = context;
    }

    public List<AccountDto> GetAccounts(int skip = 0, int take = 10)
    {
        throw new NotImplementedException();
    }

    public AccountDto GetAccountById(string id)
    {
        throw new NotImplementedException();
    }

    public AccountDto CreateAccount(AccountDto accountDto)
    {
        throw new NotImplementedException();
    }

    public AccountDto UpdateAccount(string id, AccountDto accountDto)
    {
        throw new NotImplementedException();
    }

    public AccountDto DeleteAccount(string id)
    {
        throw new NotImplementedException();
    }

    public List<ClientDto> GetClients(int skip = 0, int take = 10)
    {
        throw new NotImplementedException();
    }

    public ClientDto GetClientById(string id)
    {
        throw new NotImplementedException();
    }

    public ClientDto CreateClient(ClientDto clientDto)
    {
        throw new NotImplementedException();
    }

    public ClientDto UpdateClient(string id, ClientDto client)
    {
        throw new NotImplementedException();
    }

    public ClientDto DeleteClient(string id)
    {
        throw new NotImplementedException();
    }

    public List<TransactionDto> GetTransactions(int skip = 0, int take = 10)
    {
        throw new NotImplementedException();
    }

    public TransactionDto GetTransactionById(string id)
    {
        throw new NotImplementedException();
    }

    public TransactionDto CreateTransaction(TransactionDto transactionsDto)
    {
        throw new NotImplementedException();
    }

    public TransactionDto UpdateTransaction(string id, TransactionDto transactionsDto)
    {
        throw new NotImplementedException();
    }

    public TransactionDto DeleteTransaction(string id)
    {
        throw new NotImplementedException();
    }
}