namespace Model
using Microsoft.EntityFrameworkCore;

string myConnectionString = "Data Source=JVLPC0510\SQLSERVER;Initial Catalog=TrevisanDB;Integrated Security=True";
MyDatabaseEntities entidades = new MyDatabaseEntities(myConnectionString);

public partial class MyDatabaseEntities : DbContext
{
public MyDatabaseEntities(string connectionString)
    : base(connectionString)
{
}

protected override void OnModelCreating(DbModelBuilder modelBuilder)
{
    throw new UnintentionalCodeFirstException();
}

public virtual DbSet<MyTable> MyTable { get; set; }