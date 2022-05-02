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
    public void getAllStore()
    {
    
    }

    [HttpPost]
    [Route("register")]
    public void registerStore(StoreDTO store)
    {
        var storeModel = Store.convertDTOToModel(store);
        var id = storeModel.save(store.find());
    }

    [HttpGet]
    [Route("information")]
    public void getStoreInformation(StoreDTO store)
    {
    
    }
}
