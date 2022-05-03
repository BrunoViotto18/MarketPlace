using Microsoft.AspNetCore.Mvc;
using DTO;
using Model;

namespace Controller.Controllers;

[ApiController]
[Route("[controller]")]
public class StoreController : ControllerBase
{
    [HttpGet]
    [Route("all")]
    public List<StoreDTO> getAllStore()
    {
        return Store.getAllStores();
    }

    [HttpPost]
    [Route("register")]
    public object registerStore([FromBody] StoreDTO store)
    {
        var storeModel = Store.convertDTOToModel(store);
        var id = storeModel.save(storeModel.getOwner().findID());

        return new
        {
            name = store.name,
            CNPJ = store.CNPJ,
            owner = store.owner,
            purchases = store.purchases
        };
    }

    [HttpGet]
    [Route("information")]
    public StoreDTO getStoreInformation(String CNPJ)
    {
        return Store.findByCNPJ(CNPJ);
    }
}
