using BankAccount.DTO;
using BankAccount.Interfaces;

namespace BankAccount.Services;

public class BankAccountPostgresService : ICRUDService
{
    public List<ClientDto> GetClients(int skip = 0, int take = 10)
    {
        throw new NotImplementedException();
    }
}