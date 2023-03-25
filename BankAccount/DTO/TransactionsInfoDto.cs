namespace BankAccount.DTO;

public class TransactionsInfoDto
{
    public int AccountNumber { get; set; }
    public List<TransactionDto> OutgoingTransactionsInfo { get; set; }
    public List<TransactionDto> IncomingTransactionsInfo { get; set; }
}