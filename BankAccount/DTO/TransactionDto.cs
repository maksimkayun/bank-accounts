namespace BankAccount.DTO;

public class TransactionDto
{
    public string Id { get; set; }
    public DateTime Date { get; set; }
    public int Amount { get; set; }
    public string SenderAccountId { get; set; }
    public string RecipientAccountId { get; set; }
    public string Comment { get; set; }
}