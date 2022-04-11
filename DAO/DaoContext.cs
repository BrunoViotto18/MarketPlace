namespace DAO;
using Microsoft.EntityFrameworkCore;


public class DaoContext : DbContext
{
    public DbSet<Address> Address { get; set; }
    public DbSet<Client> Client { get; set; }
    public DbSet<Owner> Owner { get; set; }
    public DbSet<Person> Person { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<WishList> Purchase { get; set; }
    public DbSet<Stocks> Stocks { get; set; }
    public DbSet<Store> Store { get; set; }
    public DbSet<WishList> WishList { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source = JVLPC0524; Initial Catalog = marketplace; Integrated Security = True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.id);
            entity.Property(e => e.country);
            entity.Property(e => e.state);
            entity.Property(e => e.city);
            entity.Property(e => e.postal_code);
        }
        );

        modelBuilder.Entity<Owner>(entity => 
        { 
            entity.HasKey(e => e.id); 
            entity.Property(e => e.email);
            entity.Property(e => e.phone);
            entity.Property(e => e.date_of_birth);
            entity.Property(e => e.phone);
            entity.Property(e => e.passwd);
            entity.HasOne(e => e.address);
            entity.Property(e => e.login);
        }
        );

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.id);
            entity.Property(e => e.email);
            entity.Property(e => e.phone);
            entity.Property(e => e.date_of_birth);
            entity.Property(e => e.phone);
            entity.Property(e => e.passwd);
            entity.HasOne(e => e.address);
            entity.Property(e => e.login);
        }
        );

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.id);
            entity.Property(e => e.bar_code);
            entity.Property(e => e.name);
            entity.Property(e => e.unit_price);
        }
        );

        modelBuilder.Entity<WishList>(entity =>
        {
            entity.HasOne(e => e.client);
            entity.HasOne(e => e.product);
        }
        );

        modelBuilder.Entity<Store>(entity =>
        {
            entity.Property(e => e.id);
            entity.Property(e => e.CNPJ);
            entity.Property(e => e.name);
        }
        );

    }
}
