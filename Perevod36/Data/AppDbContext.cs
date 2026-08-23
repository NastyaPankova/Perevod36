using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Perevod36.Features.ApplicationUsers;
using Perevod36.Features.Common;
using Perevod36.Features.Customers;
using Perevod36.Features.Languages;
using Perevod36.Features.Managers;
using Perevod36.Features.Offices;
using Perevod36.Features.Orders;
using Perevod36.Features.TranslationTypes;
using Perevod36.Features.Translators;

namespace Perevod36.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<TranslationType> TranslationTypes => Set<TranslationType>();
    public DbSet<Language> Languages => Set<Language>();
    public DbSet<Office> Offices => Set<Office>();

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Translator> Translators => Set<Translator>();
    public DbSet<Manager> Managers => Set<Manager>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //инициализация таблиц для Identity
        base.OnModelCreating(modelBuilder);

        //таблицы наследования
        modelBuilder.Entity<Customer>().ToTable("Customers");
        modelBuilder.Entity<Translator>().ToTable("Translators");
        modelBuilder.Entity<Manager>().ToTable("Managers");

        // конверторы id->SmthId
        modelBuilder.Entity<Order>().Property(o => o.Id)
            .HasConversion(id => id.Id, value => new OrderId(value));

        modelBuilder.Entity<TranslationType>().Property(t => t.Id)
            .HasConversion(id => id.Id, value => new TranslationTypeId(value));

        modelBuilder.Entity<Language>().Property(l => l.Id)
            .HasConversion(id => id.Id, value => new LanguageId(value));

        modelBuilder.Entity<Office>().Property(o => o.Id)
           .HasConversion(id => id.Id, value => new OfficeId(value));

        modelBuilder.Entity<Order>().Property(o => o.TranslationTypeId)
            .HasConversion(id => id.Id, value => new TranslationTypeId(value));

        modelBuilder.Entity<Manager>().Property(m => m.OfficeId)
            .HasConversion(id => id.Id, value => new OfficeId(value));

        
        modelBuilder.Entity<Translator>()
            .HasMany(t => t.Languages)
            .WithMany(l => l.Translators)
            .UsingEntity(j => j.ToTable("Translator_Languages"));

        // ключи для order в явном виде для таблиц-наследников
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasOne(o => o.Manager)
                .WithMany()
                .HasForeignKey(o => o.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(o => o.Customer)
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(o => o.Translator)
                .WithMany()
                .HasForeignKey(o => o.TranslatorId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}