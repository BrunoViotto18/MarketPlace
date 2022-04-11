namespace Model;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

public class Contexto : DbContext
{
    public DbSet<Address> Address { get; set; }
    public DbSet<Client> Client { get; set; }
    public DbSet<Owner> Owner { get; set; }
    public DbSet<Person> Person { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<Purchase> Purchase { get; set; }
    public DbSet<Stocks> Stocks { get; set; }
    public DbSet<Store> Store { get; set; }
    public DbSet<WishList> WishList { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source = JVLPC0524; Initial Catalog = marketplace; Integrated Security = True");
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {

    }
}
