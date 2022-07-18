using Microsoft.AspNetCore.Mvc;
using DTO;
using Model;

namespace Controller.Controllers;

[ApiController]
[Route("[controller]")]
public class PurchaseController : ControllerBase
{
    [HttpGet]
    [Route("clientPurchase/{clientID}")]
    public List<PurchaseDTO> getClientPurchase(int clientID)
    {
        return new List<PurchaseDTO>();
    }
    
    [HttpGet]
    [Route("storePurchase/{storeID}")]
    public List<PurchaseDTO> getStorePurchase(int storeID)
    {
        return Purchase.getStorePurchases(storeID);
    }

    [HttpPost]
    [Route("make")]
    public object makePurchase(PurchaseDTOPlus purchase)
    {
        List<Product> products = new List<Product>();
        purchase.productIds.ForEach(p => products.Add(Product.findById(p)));

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
            produtos = purchase.productIds
        });
    }
}
