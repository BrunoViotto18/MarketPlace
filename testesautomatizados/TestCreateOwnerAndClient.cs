// using NUnit.Framework;
// using Model;

// namespace testesAutomatizados
// {
//     public class TestCreateOwnerAndClient
//     {
//         [Test]
//         public void CreateClients(){
//             Address adr = new Address("rua teste 1", "cidadeUm", "EstadoDois", "PaisTres", "80050450"); 

//             Client testClient1 = Client.getInstance(adr);

//             Client testClient2 = Client.getInstance(adr);

//             Assert.AreEqual(testClient1, testClient2);
//         }

//         [Test]
//         public void CreateOwner(){
//              Address adr = new Address("rua teste 1", "cidadeUm", "EstadoDois", "PaisTres", "80050450"); 

//              Owner testOwner1 = Owner.getInstance(adr);

//              Owner testOwner2 = Owner.getInstance(adr);

//              Assert.AreEqual(testOwner1, testOwner2);

//         }
//     }
// }