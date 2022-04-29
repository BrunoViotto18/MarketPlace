using Microsoft.AspNetCore.Mvc;
using DTO;

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
    
    }

    [HttpGet]
    [Route("information")]
    public void getStoreInformation(StoreDTO store)
    {
    
    }
}
