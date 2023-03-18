namespace BankAccount.DTO;

public class ClientDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string SurName { get; set; }
    public DateTime Birthday { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public List<string> AccountIds { get; set; }
}