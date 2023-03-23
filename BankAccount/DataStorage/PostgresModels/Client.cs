using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAccount.DataStorage.PostgresModels;

[Table("clients")]
public class Client
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("surname")]
    public string SurName { get; set; }

    [Column("birthday_date")]
    [DataType(DataType.Date)]
    public DateTime Birthday { get; set; }

    [Column("email")]
    public string Email { get; set; }

    [Column("phone_number")]
    public string PhoneNumber { get; set; }
    
    [Column("accounts")]
    public List<Account>? Accounts { get; set; }
}