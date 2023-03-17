using BankAccount.DataStorage;
using BankAccount.DataStorage.MongoModels;
using BankAccount.DTO;
using BankAccount.Interfaces;
using Mapper;
using MongoDB.Driver;

namespace BankAccount.Services;

public class BankAccountMongoService : ICRUDService
{
    private readonly BankAccountMongoContext _context;

    public BankAccountMongoService(BankAccountMongoContext context)
    {
        _context = context;
    }

    public List<ClientDto> GetClients(int skip = 0, int take = 10) =>
        _context.Clients.FindAsync(_ => true, new FindOptions<Client>
        {
            Limit = take,
            Skip = skip
        }).Result.ToListAsync().Result
            .Select(client => MapUtil<Client, ClientDto>.Map(client)).ToList();
}