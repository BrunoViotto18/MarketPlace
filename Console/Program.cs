using DAO;

using var context = new DAOContext();
context.Database.EnsureCreated();

//context.Address.Add(new Address
//{
//   city = "Curitiba",
//   state = "Paraná",
//   country = "Brasil",
//   street = "Rua Izaac Ferreira da Cruz",
//   postal_code = "81900-080"
//});

context.SaveChanges();
