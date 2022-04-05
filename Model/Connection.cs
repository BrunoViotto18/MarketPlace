namespace Model;
using System.Data.Entity;

public MyDbContext CreateDbContext()
{
     return new MyDbContext(connectionString);
}