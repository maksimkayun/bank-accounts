using BankAccount.DataStorage;
using BankAccount.DataStorage.PostgresModels;
using BankAccount.DTO;
using BankAccount.Interfaces;
using Mapper;

namespace BankAccount.Services;

public class BankAccountPostgresService : IAccountService, IClientService, ITransactionsService
{
    private readonly BankAccountPgContext _context;

    public BankAccountPostgresService(BankAccountPgContext context)
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