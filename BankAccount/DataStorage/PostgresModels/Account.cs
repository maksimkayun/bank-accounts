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
    public DateTime ClosingDate { get; set; }

    [Column("owner")]
    public Array Owner { get; set; }
}