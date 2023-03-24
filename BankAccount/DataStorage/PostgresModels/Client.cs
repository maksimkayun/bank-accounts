using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAccount.DataStorage.PostgresModels;

[Table("clients")]
public class Client
{
    public Client()
    {
        Accounts = new HashSet<Account>();
    }

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
    public virtual ICollection<Account>? Accounts { get; set; }
}