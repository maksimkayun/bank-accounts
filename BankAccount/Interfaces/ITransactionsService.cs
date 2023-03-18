using BankAccount.DTO;

namespace BankAccount.Interfaces;

public interface ITransactionsService
{
    public List<TransactionDto> GetTransactions(int skip = 0, int take = 10);
    public TransactionDto GetTransactionById(string id);
    public TransactionDto CreateTransaction(TransactionDto transactionsDto);
    public TransactionDto UpdateTransaction(string id, TransactionDto transactionsDto);
    public TransactionDto DeleteTransaction(string id);
}