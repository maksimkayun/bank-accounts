using AutoMapper;
using BankAccount.DataStorage;
using BankAccount.DataStorage.PostgresModels;
using BankAccount.DTO;
using BankAccount.Interfaces;

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
        var account = _context.Accounts.FirstOrDefault(e => e.Id.ToString() == id);
        _context.Accounts.Remove(account);
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