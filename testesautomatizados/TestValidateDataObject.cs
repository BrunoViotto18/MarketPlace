// using NUnit.Framework;
// using Model;
// using System.Collections.Generic;
// using System;

// namespace testesAutomatizados;

// public class TestValidateDataObject
// { 
//     public static Address adr = new Address("rua teste 1", "cidadeUm", "EstadoDois", "PaisTres", "80050450");  

//     [Test]
//     public void ValidateProductDataWithOutName(){        

//         Product product = new Product();

//         product.setUnitPrice(25.25);

//         product.setBarCode("1234567878998745613");

//         Assert.That(product.validateObject(), Is.EqualTo(false));
           
//     }
//     [Test]
//     public void ValidateProductDataWithOutUnitPrice(){

//        Product product = new Product();

//         product.setName("produto Teste 1");

//         product.setBarCode("1234567878998745613");

//         Assert.That(product.validateObject(), Is.EqualTo(false));
           
//     }
//     [Test]
//      public void ValidateProductDataWithOutBarCode(){

//         Product product = new Product();

//         product.setName("produto Teste 1");

//         product.setUnitPrice(25.25);

//         Assert.That(product.validateObject(), Is.EqualTo(false));           
//     }
//     [Test]
//     public void ValidateOwnerWithOutName(){
//         Owner testOwner = Owner.getInstance(adr);
       
//         testOwner.setEmail("joao@teste.com");
//         testOwner.setLogin("joao@teste.com");       
//         testOwner.setPhone("4199999999");

//         Assert.That(testOwner.validateObject(), Is.EqualTo(false));       
//     }

//     [Test]
//     public void ValidateOwnerWithOutEmail(){
//         Owner testOwner = Owner.getInstance(adr);
             
//         testOwner.setLogin("joao@teste.com");
//         testOwner.setName("João");
//         testOwner.setPhone("4199999999");

//         Assert.That(testOwner.validateObject(), Is.EqualTo(false));      

//     }
//     [Test]
//     public void ValidateOwnerWithOutLogin(){
//         Owner testOwner = Owner.getInstance(adr);
             
//         testOwner.setEmail("joao@teste.com");
//         testOwner.setName("João");
//         testOwner.setPhone("4199999999");

//         Assert.That(testOwner.validateObject(), Is.EqualTo(false));      

//     }

//     [Test]
//     public void ValidateOwnerWithOutPhone(){
//         Owner testOwner = Owner.getInstance(adr);
             
//         testOwner.setEmail("joao@teste.com");
//         testOwner.setName("João");
//         testOwner.setLogin("joao@teste.com");
//         Assert.That(testOwner.validateObject(), Is.EqualTo(false));      

//     }

//     [Test]
//     public void ValidateClientWithOutName(){
//         Client client = Client.getInstance(adr);
       
//         client.setEmail("joao@teste.com");
//         client.setLogin("joao@teste.com");       
//         client.setPhone("4199999999");

//         Assert.That(client.validateObject(), Is.EqualTo(false));       
//     }

//    [Test]
//     public void ValidateClientWithOutEmail(){
//         Client client = Client.getInstance(adr);
             
//         client.setLogin("joao@teste.com");
//         client.setName("João");
//         client.setPhone("4199999999");

//         Assert.That(client.validateObject(), Is.EqualTo(false));      

//     }
//     [Test]
//     public void ValidateClientWithOutLogin(){
//         Client client = Client.getInstance(adr);
             
//         client.setEmail("joao@teste.com");
//         client.setName("João");
//         client.setPhone("4199999999");

//         Assert.That(client.validateObject(), Is.EqualTo(false));      

//     }

//     [Test]
//     public void ValidateClientWithOutPhone(){
//         Client client = Client.getInstance(adr);
              
//         client.setEmail("joao@teste.com");
//         client.setName("João");
//         client.setLogin("joao@teste.com");
//         Assert.That(client.validateObject(), Is.EqualTo(false));      

//     }

//     [Test]
//     public void ValidateStoreWithOutName(){
//         Address adr = new Address("rua teste 1", "cidadeUm", "EstadoDois", "PaisTres", "80050450"); 

//         Owner testOwner = Owner.getInstance(adr);

        
//         testOwner.setEmail("joao@teste.com");
//         testOwner.setLogin("joao@teste.com");
//         testOwner.setName("João");
//         testOwner.setPhone("4199999999");

//         Store store = new Store(testOwner);           

//         store.setCNPJ("584.5258.4582/0001-60");

//         Assert.That(store.validateObject(), Is.EqualTo(false));
//     }

//     [Test]
//     public void ValidateStoreWithOutCNPJ(){
//         Address adr = new Address("rua teste 1", "cidadeUm", "EstadoDois", "PaisTres", "80050450"); 

//         Owner testOwner = Owner.getInstance(adr);

        
//         testOwner.setEmail("joao@teste.com");
//         testOwner.setLogin("joao@teste.com");
//         testOwner.setName("João");
//         testOwner.setPhone("4199999999");

//         Store store = new Store(testOwner);           

//         store.setName("Loja de teste");

//         Assert.That(store.validateObject(), Is.EqualTo(false));
//     }

//    [Test]
//     public void ValidateStockWithProductWithoutQuantity(){
//         Stocks stock = new Stocks();

//         Address adr = new Address("rua teste 1", "cidadeUm", "EstadoDois", "PaisTres", "80050450"); 

//         Owner testOwner = Owner.getInstance(adr);        

//         Store store = new Store(testOwner);      

//         Product product = new Product();


//         stock.setStore(store);

//         stock.setProduct(product);

//         Assert.That(stock.validateObject(), Is.EqualTo(false));
//     }

//     [Test]
//     public void ValidateStockWithQuantityWithoutProduct(){
//         Stocks stock = new Stocks();

//         Address adr = new Address("rua teste 1", "cidadeUm", "EstadoDois", "PaisTres", "80050450"); 

//         Owner testOwner = Owner.getInstance(adr);

//         Store store = new Store(testOwner);   

//         stock.setStore(store);

//         stock.setQuantity(35);

//         Assert.That(stock.validateObject(), Is.EqualTo(false));
//     }
// }