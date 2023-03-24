namespace BankAccount.Requests;

public class SendMoneyRequest
{
    public string SenderAccountNumber { get; set; }
    public string RecipientAccountNumber { get; set; }
    public int Amount { get; set; }
}