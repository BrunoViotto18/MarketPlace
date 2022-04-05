using NUnit.Framework;
using Model;
using System.Collections.Generic;
using System;

namespace testesAutomatizados
{
    public class TestsCreate
    {   
       
          
        [Test]
        public void ClientTestCreate()
        {
            Address adr = new Address("rua teste 1", "cidadeUm", "EstadoDois", "PaisTres", "80050450"); 

            Client testClient = Client.getInstance(adr);
            
            
            testClient.setEmail("joao@teste.com");
            testClient.setLogin("joao@teste.com");
            testClient.setName("João");
            testClient.setPhone("4199999999");

            Assert.That("João", Is.EqualTo(testClient.getName()));
            Assert.That("joao@teste.com", Is.EqualTo(testClient.getEmail()));
            Assert.That("joao@teste.com", Is.EqualTo(testClient.getLogin()));
            Assert.That("4199999999", Is.EqualTo(testClient.getPhone()));
            Assert.AreEqual(adr.getStreet(), testClient.getAddress().getStreet());
            Assert.AreEqual(adr.getCity(), testClient.getAddress().getCity());
            Assert.AreEqual(adr.getState(), testClient.getAddress().getState());
            Assert.AreEqual(adr.getCountry(), testClient.getAddress().getCountry());
            Assert.AreEqual(adr.getPostalCode(), testClient.getAddress().getPostalCode());            

        }

        [Test]
        public void AddressTestCreate()
        {
            Address testAddress = new Address("rua teste 1", "cidadeUm", "EstadoDois", "PaisTres", "80050450"); 

            Assert.That("rua teste 1", Is.EqualTo(testAddress.getStreet()));
            Assert.That("cidadeUm", Is.EqualTo(testAddress.getCity()));
            Assert.That("EstadoDois", Is.EqualTo(testAddress.getState()));
            Assert.That("PaisTres", Is.EqualTo(testAddress.getCountry()));
            Assert.That("80050450", Is.EqualTo(testAddress.getPostalCode()));
        }

        [Test]
        public void OwnerTestCreate()
        {
            Address adr = new Address("rua teste 1", "cidadeUm", "EstadoDois", "PaisTres", "80050450"); 

            Owner testOwner = Owner.getInstance(adr);

            
            testOwner.setEmail("joao@teste.com");
            testOwner.setLogin("joao@teste.com");
            testOwner.setName("João");
            testOwner.setPhone("4199999999");


            Assert.That("João", Is.EqualTo(testOwner.getName()));
            Assert.That("joao@teste.com", Is.EqualTo(testOwner.getEmail()));
            Assert.That("joao@teste.com", Is.EqualTo(testOwner.getLogin()));
            Assert.That("4199999999", Is.EqualTo(testOwner.getPhone()));
            Assert.AreEqual(adr.getStreet(), testOwner.getAddress().getStreet());
            Assert.AreEqual(adr.getCity(), testOwner.getAddress().getCity());
            Assert.AreEqual(adr.getState(), testOwner.getAddress().getState());
            Assert.AreEqual(adr.getCountry(), testOwner.getAddress().getCountry());
            Assert.AreEqual(adr.getPostalCode(), testOwner.getAddress().getPostalCode());    
        }

        [Test]
        public void WishListTestCreate()
        {

            Address adr = new Address("rua teste 1", "cidadeUm", "EstadoDois", "PaisTres", "80050450"); 

            Client testClient = Client.getInstance(adr);

            WishList wishList = new WishList(testClient);

            Product product1 = new Product();
            product1.setName("produto teste1");

            Product product2 = new Product();
            product2.setName("produto teste2");

            Product product3 = new Product();
            product3.setName("produto teste3");

            wishList.addProductToWishList(product1);
            wishList.addProductToWishList(product2);
            wishList.addProductToWishList(product3);

            Assert.AreEqual(wishList.getClient(), testClient);
            Assert.That(wishList.getProducts().Count, Is.EqualTo(3));

        }

        [Test]
        public void PurchaseTestCreate()
        {
            Purchase testPurchase = new Purchase();

            DateTime date = new DateTime();

            List<Product> products = new List<Product>();

             Product product1 = new Product();
            product1.setName("produto teste1");

            Product product2 = new Product();
            product2.setName("produto teste2");

            Product product3 = new Product();
            product3.setName("produto teste3");

            products.Add(product1);
            products.Add(product2);
            products.Add(product3);

            testPurchase.setDataPurchase(date);
            testPurchase.setNumberConfirmation("0123456");
            testPurchase.setNumberNf("987654321");
            testPurchase.setPaymentType(Enums.PaymentEnum.initial);
            testPurchase.setPurchaseStatus(Enums.PurchaseStatusEnum.awaitingPayment);
            testPurchase.setProducts(products);

            Assert.That(testPurchase.getNumberConfirmation, Is.EqualTo("0123456"));
            Assert.That(testPurchase.getNumberNf, Is.EqualTo("987654321"));
            Assert.That(testPurchase.getPaymentType(), Is.EqualTo((int)Enums.PaymentEnum.initial));
            Assert.That(testPurchase.getPurchaseStatus(), Is.EqualTo((int)Enums.PurchaseStatusEnum.awaitingPayment));
            Assert.That(testPurchase.getProducts().Count, Is.EqualTo(3));
        }

        [Test]
        public void StocksTestCreate()
        {

        }

        [Test]
        public void ProductTestCreate()
        {
            Product product = new Product();

            product.setName("produto Teste 1");

            product.setUnitPrice(25.25);

            product.setBarCode("1234567878998745613");

            Assert.That(product.getName(), Is.EqualTo("produto Teste 1"));

            Assert.That(product.getBarCode(), Is.EqualTo("1234567878998745613"));

            Assert.That(product.getUnitprice(), Is.EqualTo(25.25));

        }

        [Test]
        public void StoreCreateTest()
        {
            Address adr = new Address("rua teste 1", "cidadeUm", "EstadoDois", "PaisTres", "80050450"); 

            Owner testOwner = Owner.getInstance(adr);

            
            testOwner.setEmail("joao@teste.com");
            testOwner.setLogin("joao@teste.com");
            testOwner.setName("João");
            testOwner.setPhone("4199999999");

            Store store = new Store(testOwner);

            store.setName("Loja de teste");

            store.setCNPJ("584.5258.4582/0001-60");

            Assert.AreEqual(store.getOwner(), testOwner);
            Assert.That(store.getName, Is.EqualTo("Loja de teste"));
            Assert.That(store.getCNPJ(), Is.EqualTo("584.5258.4582/0001-60"));
        }

    }
}