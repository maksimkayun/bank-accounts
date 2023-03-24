namespace BankAccount.DTO;

public class AccountCTDto
{
   
    public string Id { get; set; }
    
    public int AccountNumber { get; set; }

    public int Balance { get; set; }
   
    public DateTime OpeningDate { get; set; }
 
    public DateTime? ClosingDate { get; set; }
  
    public string? Owner { get; set; }
    
    public List<string> ClientInfo { get; set; }
    
    public List<string> TransactionsInfo { get; set; }
}