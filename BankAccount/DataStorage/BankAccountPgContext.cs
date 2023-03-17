using BankAccount.DataStorage.PostgresModels;
using Microsoft.EntityFrameworkCore;

namespace BankAccount.DataStorage;

public sealed class BankAccountPgContext : DbContext
{
    public DbSet<Client> Clients { get; set; }

    private BankAccountPgContext()
    {
        Database.EnsureCreated();
    }

    public BankAccountPgContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
}