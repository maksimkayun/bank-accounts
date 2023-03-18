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
    public async Task<Client?> GetClientById(string id) =>
        await _context.Clients.Find(x => x.Id == id).FirstOrDefaultAsync();
  /*  public async Task CreateClient(ClientDto newClient) =>
        await _context.Clients.InsertOneAsync(newClient);

    public async Task UpdateClient(string id,ClientDto client) =>
        await _context.Clients.ReplaceOneAsync(x => x.Id == id, client);
    
    public async Task DeleteClient(string id) =>
        await _context.Clients.DeleteOneAsync(x => x.Id == id);*/
}