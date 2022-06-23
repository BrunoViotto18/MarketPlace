namespace DAO;
using Microsoft.EntityFrameworkCore;


public class DAOContext : DbContext
{
    public DbSet<Address> Address { get; set; }
    public DbSet<Client> Client { get; set; }
    public DbSet<Owner> Owner { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<Purchase> Purchase { get; set; }
    public DbSet<Stocks> Stocks { get; set; }
    public DbSet<Store> Store { get; set; }
    public DbSet<WishList> WishList { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        switch (Environment.MachineName)
        {
            case "BRUNOVIOTTO":
                optionsBuilder.UseSqlServer("Data Source=BRUNOVIOTTO\\SQLEXPRESS;Initial Catalog=Marketplace;Integrated Security=True");
                break;
            case "JVLPC0524":
                optionsBuilder.UseSqlServer("Data Source=JVLPC0524\\SQLEXPRESS;Initial Catalog=Marketplace;Integrated Security=True");
                break;
            case "JVLPC0510":
                optionsBuilder.UseSqlServer("Data Source=JVLPC0510\\SQLSERVER;Initial Catalog=Marketplace;Integrated Security=True");
                break;
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(p => p.id);
            entity.Property(e => e.city);
            entity.Property(e => e.street);
            entity.Property(e => e.state);
            entity.Property(e => e.country);
            entity.Property(e => e.postal_code);
        }
        );

        modelBuilder.Entity<Owner>(entity => 
        { 
            entity.HasKey(e => e.id);
            entity.Property(e => e.name);
            entity.Property(e => e.email);
            entity.Property(e => e.date_of_birth);
            entity.Property(e => e.phone);
            entity.Property(e => e.login);
            entity.Property(e => e.passwd);
            entity.Property(e => e.document);
            entity.HasOne(e => e.address);
        }
        );

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.id);
            entity.Property(e => e.name);
            entity.Property(e => e.email);
            entity.Property(e => e.date_of_birth);
            entity.Property(e => e.phone);
            entity.Property(e => e.login);
            entity.Property(e => e.passwd);
            entity.Property(e => e.document);
            entity.HasOne(e => e.address);
        }
        );

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.id);
            entity.Property(e => e.name);
            entity.Property(e => e.bar_code);
            entity.Property(e => e.image);
            entity.Property(e => e.description);
        }
        );

        modelBuilder.Entity<WishList>(entity =>
        {
            entity.HasKey(e => e.id);
            entity.HasOne(e => e.client);
            entity.HasOne(e => e.stock);
        }
        );

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(p => p.id);
            entity.Property(e => e.name);
            entity.Property(e => e.CNPJ);
            entity.HasOne(f => f.owner);
        }
        );

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(p => p.id);
            entity.Property(e => e.number_confirmation);
            entity.Property(e => e.number_nf);
            entity.Property(e => e.payment_type);
            entity.Property(e => e.purchase_status);
            entity.Property(e => e.date_purchase);
            entity.Property(e => e.purchase_value);
            entity.HasOne(f => f.client);
            entity.HasOne(f => f.store);
            entity.HasOne(f => f.product);
        }
        );

        modelBuilder.Entity<Stocks>(entity =>
        {
            entity.HasKey(p => p.id);
            entity.Property(e => e.quantity);
            entity.Property(e => e.unit_price);
            entity.HasOne(f => f.product);
            entity.HasOne(f => f.store);
        }
        );
    }
}
