using AutoMapper;
using BankAccount.DataStorage;
using BankAccount.DataStorage.MongoModels;
using BankAccount.DTO;
using BankAccount.Interfaces;
using MongoDB.Driver;

namespace BankAccount.Services;

public class BankAccountMongoService : IAccountService, IClientService, ITransactionsService
{
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