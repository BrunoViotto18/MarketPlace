using Microsoft.AspNetCore.Mvc;
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
                product = new ProductDTO
                {
                    id = p.getProducts().First().getId()
                }
            });

        return Ok(lista);
    }
    
    [HttpGet]
    [Route("storePurchase")]
    public IActionResult getStorePurchase()
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
                product = new ProductDTO
                {
                    id = p.getProducts().First().getId()
                }
            });

        return Ok(lista);
    }

    [HttpPost]
    [Route("make")]
    public object makePurchase(PurchaseDTOPlus purchase)
    {
        List<Product> products = new List<Product>() { Product.findById(purchase.product.id) };

        Purchase purch = new Purchase();
        purch.setDataPurchase(purchase.data_purchase);
        purch.setNumberConfirmation(purchase.confirmation_number);
        purch.setNumberNf(purchase.number_nf);
        purch.setPaymentType((Enums.PaymentEnum)purchase.payment_type);
        purch.setPurchaseStatus((Enums.PurchaseStatusEnum)purchase.purchase_status);
        purch.setPurchaseValue(purchase.purchase_value);
        purch.setClient(Client.findById(purchase.clientId));
        purch.setStore(Store.findById(purchase.storeId));
        purch.getProducts().AddRange(products);

        var id = purch.save();

        return Ok(new
        {
            id = id,
            dataCompra = purchase.data_purchase,
            valorCompra = purchase.purchase_value,
            tipoPagamento = purchase.payment_type,
            statusCompra = purchase.purchase_status,
            numeroConfirmacao = purchase.confirmation_number,
            numeroNF = purchase.number_nf,
            loja = purch.getStore(),
            cliente = purch.getClient(),
            produtos = purchase.product
        });
    }
}
