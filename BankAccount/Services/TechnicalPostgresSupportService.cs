using BankAccount.DataStorage;
using BankAccount.DataStorage.PostgresModels;
using BankAccount.Interfaces;
using BankAccount.Requests;

namespace BankAccount.Services;

public class TechnicalPostgresSupportService : ITechnicalSupport
{
    private readonly BankAccountPgContext _context;
    public TechnicalPostgresSupportService(BankAccountPgContext context)
    {
        _context = context;
    }
    public async Task SeedCollectionAccounts()
    {
        var entities = new List<Account>();
        var closingDate = new List<DateTime?>()
        {
            DateTime.Now.ToUniversalTime(), DateTime.Now.AddYears(2).ToUniversalTime(), null
        };
        var clients = await GetClients();
        for (int i = 1; i <= 100000; i++)
        {
            var acc = new Account
            {
                AccountNumber = (100000 + i).ToString(),
                Balance = new Random().Next(0, 1000000),
                OpeningDate = DateTime.Now.AddYears(new Random().Next(-10, 0)).ToUniversalTime(),
                ClosingDate = closingDate[(i - 1) % 3],
                Owner = clients[i - 1]
            };
            entities.Add(acc);
        }

        _context.Accounts.AddRange(entities);
        await _context.SaveChangesAsync();

        var transactions = GetTransactions(entities);
        _context.Transactions.AddRange(transactions);
        await _context.SaveChangesAsync();
    }

    private async Task<List<Client>> GetClients()
        => await Task.Run(() =>
        {
            var clients = new List<Client>();
            var names = new List<string>()
                { "Ivan", "Fedor", "Anatoly", "Maksim", "Nickolay", "Sergey", "Yuri", "Kirill", "Alexander", "Dmitry" };
            var surnames = new List<string>()
            {
                "Zimin", "Gref", "Kondrashov", "Alekseev", "Borisov", "Lazarev", "Sokolov", "Borodin", "Morozov",
                "Medvedev"
            };

            var email = new List<string>()
            {
                "portele@gmail.com", "draper@gmail.com", "rjones@gmail.com", "mcsporran@gmail.com", "skajan@gmail.com",
                "aibrahim@gmail.com", "zeitlin@gmail.com", "sequin@gmail.com", "peoplesr@gmail.com", "bebing@gmail.com"
            };

            for (int i = 0; i < 100000; i++)
            {
                var client = new Client
                {
                    Name = names[i % 10],
                    SurName = surnames[i % 10],
                    Birthday = DateTime.Now.AddYears(new Random().Next(-70, -16)).ToUniversalTime(),
                    Email = email[i % 10],
                    PhoneNumber = (79950000001 + i).ToString()
                };
                clients.Add(client);
            }

            return clients;
        });

    private List<Transaction> GetTransactions(List<Account> accs)
    {
        var transactions = new List<Transaction>();
        for (int i = 0; i < 100000; i++)
        {
            var sender = new Random().Next(100001, 200000);
            var recipient = new Random().Next(100001, 200000);
            while (recipient == sender)
            {
                recipient = new Random().Next(100001, 200000);
            }

            SendMoneyRequest request = new SendMoneyRequest
            {
                SenderAccountNumber = sender.ToString(),
                RecipientAccountNumber = recipient.ToString(),
                Amount = new Random().Next(1, 100000)
            };
            var senderAcc = accs.First(e => e.AccountNumber == request.SenderAccountNumber);
            var recipientAcc = accs.First(e => e.AccountNumber == request.RecipientAccountNumber);

            if (senderAcc.Id != recipientAcc.Id)
            {
                var transaction = new Transaction
                {
                    Date = DateTime.Now.ToUniversalTime(),
                    Amount = request.Amount,
                    Sender = senderAcc,
                    Recipient = recipientAcc
                };
                transactions.Add(transaction);
            }
        }

        return transactions;
    }
}