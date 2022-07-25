using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DTO;
using Model;

namespace Controller.Controllers;

[ApiController]
[Route("[controller]")]
public class PurchaseController : ControllerBase
{
    [HttpGet]
    [Route("clientPurchase")]
    public IActionResult getClientPurchase()
    {
        int clientId = JWT.GetIdFromToken(Request.Headers["Authorization"].ToString());

        var purch = Purchase.getAllClientPurchases().Where(p => p.getClient().getId() == clientId);

        var lista = new List<PurchaseDTOPlus>();
        foreach (var p in purch)
            lista.Add(new PurchaseDTOPlus
            {
                id = p.getId(),
                data_purchase = p.getDataPurchase(),
                purchase_value = p.getPurchaseValue(),
                payment_type = p.getPaymentType(),
                purchase_status = p.getPurchaseStatus(),
                confirmation_number = p.getNumberConfirmation(),
                number_nf = p.getNumberNf(),
                storeId = p.getStore().getId(),
                clientId = p.getClient().getId(),
                product = p.getProducts().First().convertModelToDTO()
            });

        return Ok(lista);
    }
    
    [HttpGet]
    [Route("ownerPurchase")]
    public IActionResult getOwnerPurchase()
    {
        int ownerId = JWT.GetIdFromToken(Request.Headers["Authorization"].ToString());

        var purch = Purchase.getAllClientPurchases().Where(p => p.getStore().getOwner().getId() == ownerId);

        var lista = new List<PurchaseDTOPlus>();
        foreach (var p in purch)
            lista.Add(new PurchaseDTOPlus
            {
                id = p.getId(),
                data_purchase = p.getDataPurchase(),
                purchase_value = p.getPurchaseValue(),
                payment_type = p.getPaymentType(),
                purchase_status = p.getPurchaseStatus(),
                confirmation_number = p.getNumberConfirmation(),
                number_nf = p.getNumberNf(),
                storeId = p.getStore().getId(),
                clientId = p.getClient().getId(),
                product = p.getProducts().First().convertModelToDTO()
            });

        return Ok(lista);
    }

    [Authorize]
    [HttpPost]
    [Route("make")]
    public object makePurchase(PurchaseDTOPlus purchase)
    {
        var clientId = JWT.GetIdFromToken(Request.Headers["Authorization"].ToString());
        var storeId = Store.findByStockId(purchase.storeId);
        List<Product> products = new List<Product>() { Product.findById(purchase.product.id) };

        var rand = new Random((int)DateTime.Now.Ticks);
        string nums = "0123456789";
        string nf = "";
        string conf = "";

        for (int i = 0; i < 6; i++)
            conf += nums[rand.Next(0, 9)];

        for (int i = 0; i < 20; i++)
            nf += nums[rand.Next(0, 9)];

        Purchase purch = new Purchase();
        purch.setDataPurchase(DateTime.Now);
        purch.setNumberConfirmation(conf);
        purch.setNumberNf(nf);
        purch.setPaymentType((Enums.PaymentEnum)purchase.payment_type);
        purch.setPurchaseStatus((Enums.PurchaseStatusEnum)purchase.purchase_status);
        purch.setPurchaseValue(purchase.purchase_value);
        purch.setClient(Client.findById(clientId));
        purch.setStore(Store.findById(storeId));
        purch.getProducts().AddRange(products);

        var id = purch.save();

        return Ok(new
        {
            id = id,
            dataCompra = purchase.data_purchase,
            valorCompra = purchase.purchase_value,
            tipoPagamento = purchase.payment_type,
            statusCompra = purchase.purchase_status,
            numeroConfirmacao = conf,
            numeroNF = nf,
            loja = purch.getStore(),
            cliente = purch.getClient(),
            produtos = purchase.product
        });
    }
}
