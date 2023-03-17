using BankAccount.DTO;

namespace BankAccount.Interfaces;

public interface ICRUDService
{
    public List<ClientDto> GetClients(int skip = 0, int take = 10);
}