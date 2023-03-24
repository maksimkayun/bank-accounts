namespace BankAccount.Requests;

public class GetTransactionsByClientIdRequest
{
    public int Skip { get; set; }
    public int Take { get; set; }
    public string ClientId { get; set; }
}