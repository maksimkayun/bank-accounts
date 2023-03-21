using DbContext.DataStorage.PostgresModels;
using Microsoft.EntityFrameworkCore;

namespace DbContext.DataStorage;

public sealed class BankAccountPgContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    private BankAccountPgContext()
    {
       
       
    }

    public BankAccountPgContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>()
            .HasOne<Client>(e => e.Owner)
            .WithMany(e => e.Accounts)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Account>()
            .HasMany<Transaction>(e => e.Transactions)
            .WithOne(e => e.Sender)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<Account>()
            .HasMany<Transaction>(e => e.Transactions)
            .WithOne(e => e.Recipient)
            .OnDelete(DeleteBehavior.NoAction);

        // Экспериментально, оценить на сколько быстрее находится счёт по владельцу и наоборот
        // modelBuilder.Entity<Account>()
        //     .HasIndex(e => e.AccountNumber)
        //     .IncludeProperties(e => e.Owner);
        
        base.OnModelCreating(modelBuilder);
    }
}