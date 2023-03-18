using BankAccount.DataStorage;
using BankAccount.DataStorage.PostgresModels;
using BankAccount.DTO;
using BankAccount.Interfaces;
using Mapper;

namespace BankAccount.Services;

public class BankAccountPostgresService : ICRUDService
{
    private readonly BankAccountPgContext _context;

    public BankAccountPostgresService(BankAccountPgContext context)
    {
        _context = context;
    }

    public List<ClientDto> GetClients(int skip = 0, int take = 10) =>
        _context.Clients.Skip(skip).Take(take).ToList()
            .Select(client => MapUtil<Client, ClientDto>.Map(client)).ToList();

    public Task<DataStorage.MongoModels.Client?> GetClientById(string id)
    {
        throw new NotImplementedException();
    }

    public Task CreateClient(ClientDto clientDto)
    {
        throw new NotImplementedException();
    }

    public Task UpdateClient(string id, ClientDto client)
    {
        throw new NotImplementedException();
    }

    public Task DeleteClient(string id)
    {
        throw new NotImplementedException();
    }
}