using BankAccount.DataStorage.MongoModels;
using BankAccount.DTO;

namespace BankAccount.Interfaces;

public interface ICRUDService
{
    public List<ClientDto> GetClients(int skip = 0, int take = 10);
    
    public Task<Client?> GetClientById(string id);
    public Task CreateClient(ClientDto clientDto);
    public Task UpdateClient(string id, ClientDto client);
    public Task DeleteClient(string id);
}