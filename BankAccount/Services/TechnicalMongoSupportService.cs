using BankAccount.DataStorage;
using BankAccount.DataStorage.MongoModels;
using BankAccount.Interfaces;
using BankAccount.Requests;
using MongoDB.Driver;

namespace BankAccount.Services;

public class TechnicalMongoSupportService : ITechnicalSupport
{
    private BankAccountMongoContext _context;

    public TechnicalMongoSupportService(BankAccountMongoContext context)
    {
        _context = context;
    }

    public void SeedCollectionAccounts()
    {
        var entities = new List<Account>();
        var closingDate = new List<DateTime?>()
        {
            DateTime.Now.ToUniversalTime(), DateTime.Now.AddYears(2).ToUniversalTime(), null
        };
        var clients = GetClients();
        var transactions = GetTransactions();
        
        _context.Clients.InsertMany(clients);
        for (int i = 1; i <= 100000; i++)
        {
            var transaction = transactions
                .Where(e => e.RecipientAccountNumber == 100000 + i || e.SenderAccountNumber == 100000 + i)
                .Select(e => e.NumberTransaction).ToList();
            var acc = new Account
            {
                AccountNumber = 100000 + i,
                Balance = new Random().Next(0, 1000000),
                OpeningDate = DateTime.Now.AddYears(new Random().Next(-10, 0)).ToUniversalTime(),
                ClosingDate = closingDate[(i - 1) % 3],
                Owner = clients[i - 1].Id,
                TransactionNumbers = new List<int>(transaction)
            };
            entities.Add(acc);
        }
        
        
        _context.Accounts.InsertMany(entities);
    }

    private List<Client> GetClients()
    {
        var clients = new List<Client>();
        var names = new List<string>()
            {"Ivan", "Fedor", "Anatoly", "Maksim", "Nickolay", "Sergey", "Yuri", "Kirill", "Alexander", "Dmitry"};
        var surnames = new List<string>()
        {
            "Zimin", "Gref", "Kondrashov", "Alekseev", "Borisov", "Lazarev", "Sokolov", "Borodin", "Morozov", "Medvedev"
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
    }

    private List<Transaction> GetTransactions()
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

            if (sender != recipient)
            {
                var transaction = new Transaction
                {
                    NumberTransaction = 100001 + i,
                    Date = DateTime.Now.ToUniversalTime(),
                    Amount = new Random().Next(1, 100000),
                    SenderAccountNumber = sender,
                    RecipientAccountNumber = recipient
                };
                transactions.Add(transaction);
            }
        }

        _context.Transactions.InsertMany(transactions);
        return transactions;
    }
}