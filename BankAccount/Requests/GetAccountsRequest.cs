namespace BankAccount.Requests;

public class GetAccountsRequest
{
    public int Take { get; set; }
    public int Skip { get; set; }
}