using BankAccount.DataStorage.PostgresModels;
using Microsoft.EntityFrameworkCore;

namespace BankAccount.DataStorage;

public sealed class BankAccountPgContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>()
            .HasOne<Client>(e => e.Owner)
            .WithMany(e => e.Accounts)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Account>()
            .HasMany<Transaction>(e => e.OutgoingTransactions)
            .WithOne(e => e.Sender)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Account>()
            .HasMany<Transaction>(e => e.IcomingTransactions)
            .WithOne(e => e.Recipient)
            .OnDelete(DeleteBehavior.SetNull);

        // Экспериментально, оценить на сколько быстрее находится счёт по владельцу и наоборот
        // modelBuilder.Entity<Account>()
        //     .HasIndex(e => e.AccountNumber)
        //     .IncludeProperties(e => e.Owner);

        modelBuilder.Entity<Account>(e =>
        {
            e.HasKey(k => k.Id);
            e.Property(k => k.Id).ValueGeneratedOnAdd();
        });
        modelBuilder.Entity<Transaction>(e =>
        {
            e.HasKey(k => k.Id);
            e.Property(k => k.Id).ValueGeneratedOnAdd();
        });
        modelBuilder.Entity<Client>(e =>
        {
            e.HasKey(k => k.Id);
            e.Property(k => k.Id).ValueGeneratedOnAdd();
        });

        base.OnModelCreating(modelBuilder);
    }
}