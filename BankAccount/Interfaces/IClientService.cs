using BankAccount.DataStorage.PostgresModels;
using BankAccount.DTO;
using BankAccount.Requests;

namespace BankAccount.Interfaces;

public interface IClientService
{
    public List<ClientDto> GetClients(int skip = 0, int take = 10);
    public ClientDto GetClientById(string id);
    public ClientDto CreateClient(ClientDto clientDto);
    public ClientDto UpdateClient(string id, ClientDto clientDto);
    public ClientDto DeleteClient(string id);

    public TransactionDto MakeTransaction(SendMoneyRequest request);

    public List<TransactionDto> GetTransactionsByClientId(GetTransactionsByClientIdRequest request);
}