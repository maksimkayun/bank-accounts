using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAccount.DataStorage.PostgresModels;

public class AccountCT
{
   /* public Account()
    {
        OutgoingTransactions = new HashSet<Transaction>();
        IcomingTransactions = new HashSet<Transaction>();
    }
    */
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("account_number")]
    public string AccountNumber { get; set; }

    [Column("balance")]
    public int Balance { get; set; }

    [Column("opening_date")]
    [DataType(DataType.Date)]
    public DateTime OpeningDate { get; set; }

    [Column("closing_date")]
    [DataType(DataType.Date)]
    public DateTime? ClosingDate { get; set; }

    [ForeignKey("owner_id")]
    public Client? Owner { get; set; }
    
    [ForeignKey("clientInfo")]
    public List<Client> ClientInfo { get; set; }
    
    [ForeignKey("transactionsInfo")]
    public List<Transaction> TransactionsInfo { get; set; }
}