using DAO;

using var context = new DaoContext();
context.Database.EnsureCreated();

context.Address.Add(new Address
{
   city = "Curitiba",
   state = "Paraná",
   country = "Brasil",
   postal_code = "81900-080"
});

context.SaveChanges();
