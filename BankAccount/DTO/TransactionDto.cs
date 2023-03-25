namespace BankAccount.DTO;

public class TransactionDto
{
    public string Id { get; set; }
    public DateTime Date { get; set; }
    public int Amount { get; set; }
    public int SenderAccountNumber { get; set; }
    public int RecipientAccountNumber { get; set; }
    public string Comment { get; set; }
}