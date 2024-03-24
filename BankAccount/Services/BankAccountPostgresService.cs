using AutoMapper;
using BankAccount.DataStorage;
using BankAccount.DataStorage.PostgresModels;
using BankAccount.DTO;
using BankAccount.Exceptions;
using BankAccount.Interfaces;
using BankAccount.Requests;
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
            .Include(e => e.Owner)
            .Include(e => e.IcomingTransactions)
            .Include(e => e.OutgoingTransactions)
            .AsEnumerable()
            .Select(e => _mapper.Map<AccountDto>(e))
            .ToList();

    public AccountDto GetAccountById(string id)
    {
        var account = _context.Accounts
            .Include(e => e.Owner)
            .Include(e => e.IcomingTransactions)
            .Include(e => e.OutgoingTransactions)
            .SingleOrDefault(e => e.Id.ToString() == id);
        var accountDto = _mapper.Map<AccountDto>(account);
        return accountDto;
    }

    public AccountDto GetAccountByNumber(int accountNumber) =>
        GetAccountById(_context.Accounts.FirstOrDefault(e => e.AccountNumber == accountNumber.ToString()).Id
            .ToString());


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


    public List<ClientDto> GetClients(int skip = 0, int take = 10) =>
        _context.Clients.Skip(skip).Take(take)
            .Include(e => e.Accounts)
            .AsEnumerable()
            .Select(e => _mapper.Map<ClientDto>(e))
            .ToList();

    public ClientDto GetClientById(string id) =>
        _mapper.Map<ClientDto>(_context.Clients.Include(e => e.Accounts).First(e => e.Id == int.Parse(id)));

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

    public async Task<TransactionDto?> MakeTransaction(SendMoneyRequest request)
    {
        TransactionDto transactionDto = null;

        var senderAcc =
            await _context.Accounts.FirstOrDefaultAsync(e => e.AccountNumber == request.SenderAccountNumber);
        var recipientAcc =
            await _context.Accounts.FirstOrDefaultAsync(e => e.AccountNumber == request.RecipientAccountNumber);

        if (senderAcc == null || recipientAcc == null)
        {
            BusinessException.GenerateBusinessExceptionWithThrow(400,
                senderAcc == null && recipientAcc == null ? "Лицевые счта отправителя и получателя не найдены" :
                senderAcc == null ? "Лицевой счёт отправителя не найден" : "Лицевой счёт получателя не найден",
                string.Empty);
        }

        if (senderAcc.Balance - request.Amount >= 0 && senderAcc.Id != recipientAcc.Id)
        {
            senderAcc.Balance -= request.Amount;
            recipientAcc.Balance += request.Amount;
            var transaction = new Transaction
            {
                Date = DateTime.Now.ToUniversalTime(),
                Amount = request.Amount,
                Sender = senderAcc,
                Recipient = recipientAcc
            };
            transaction = _context.Transactions.Add(transaction).Entity;
            _context.Accounts.UpdateRange([senderAcc, recipientAcc]);
            await _context.SaveChangesAsync();
            transactionDto = _mapper.Map<TransactionDto>(transaction);
        }

        return transactionDto;
    }

    public List<TransactionsInfoDto> GetTransactionsByClientId(GetTransactionsByClientIdRequest request)
    {
        var accounts = _context.Accounts.Include(e => e.Owner)
            .Where(e => e.Owner.Id == int.Parse(request.ClientId))
            .Include(e => e.IcomingTransactions)
            .ThenInclude(e => e.Sender)
            .Include(e => e.OutgoingTransactions)
            .ThenInclude(e => e.Recipient);

        List<TransactionsInfoDto> result = new List<TransactionsInfoDto>();
        foreach (var acc in accounts)
        {
            var info = new TransactionsInfoDto
            {
                AccountNumber = Convert.ToInt32(acc.AccountNumber),
                OutgoingTransactionsInfo =
                    acc.OutgoingTransactions.Select(e => _mapper.Map<TransactionDto>(e)).ToList(),
                IncomingTransactionsInfo = acc.IcomingTransactions.Select(e => _mapper.Map<TransactionDto>(e)).ToList()
            };
            result.Add(info);
        }

        return result;
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