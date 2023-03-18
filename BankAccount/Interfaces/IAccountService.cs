using BankAccount.DTO;

namespace BankAccount.Interfaces;

public interface IAccountService
{
    public List<AccountDto> GetAccounts(int skip = 0, int take = 10);
    public AccountDto GetAccountById(string id);
    public AccountDto CreateAccount(AccountDto accountDto);
    public AccountDto UpdateAccount(string id, AccountDto accountDto);
    public AccountDto DeleteAccount(string id);
}