using BankAccount.DTO;

namespace BankAccount.Interfaces;

public interface IClientService
{
    public List<ClientDto> GetClients(int skip = 0, int take = 10);
    public ClientDto GetClientById(string id);
    public ClientDto CreateClient(ClientDto clientDto);
    public ClientDto UpdateClient(string id, ClientDto client);
    public ClientDto DeleteClient(string id);
}