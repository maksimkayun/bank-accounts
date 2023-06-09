namespace BankAccount.DTO;

public class AccountDto
{
    public string? Id { get; set; }
    public string AccountNumber { get; set; }
    public int Balance { get; set; }
    public DateTime OpeningDate { get; set; }
    public DateTime ClosingDate { get; set; }
    public string OwnerId { get; set; }
    public List<int>? TransactionNumbers { get; set; }
}