using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAccount.DataStorage.PostgresModels;
[Table("accounts")]
public class Account
{
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

    [Column("owner")]
    public Client? Owner { get; set; }
    
    [Column("outgoing_transactions")]
    public List<Transaction>? OutgoingTransactions { get; set; }
    
    [Column("incoming_transactions")]
    public List<Transaction>? IcomingTransactions { get; set; }
}