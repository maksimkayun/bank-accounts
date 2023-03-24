using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAccount.DataStorage.PostgresModels;

[Table("transactions")]
public class Transaction
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("date")]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }

    [Column("amount")]
    public int Amount { get; set; }

    [Column("sender")]
    public Account? Sender { get; set; }

    [Column("recipient")]
    public Account? Recipient { get; set; }
}