using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
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
        return new List<StoreDTO>();
    }

    [Authorize]
    [HttpPost]
    [Route("register")]
    public object registerStore([FromBody] StoreRegisterDTO store)
    {
        var ownerID = JWT.GetIdFromToken(Request.Headers["Authorization"].ToString());
        var storeModel = Store.convertDTOToModelRegister(store);
        Console.WriteLine(ownerID);
        var id = storeModel.save(ownerID);

        return new
        {
            id = id,
            name = store.name,
            CNPJ = store.CNPJ
        };
    }

    [HttpGet]
    [Route("information/{CNPJ}")]
    public StoreDTO getStoreInformation(String CNPJ)
    {
        return Store.findByCNPJ(CNPJ);
    }

    [HttpGet]
    [Route("getAllOwnerStores")]
    public object getAllStores()
    {
        int ownerId = JWT.GetIdFromToken(Request.Headers["Authorization"].ToString());

        Console.WriteLine(ownerId);
        var stores = Store.getAllOwnerStores(ownerId);

        var storesDTO = new List<StoreDTO>();

        foreach(var store in stores){
            var storeDTO = new StoreDTO();
            storeDTO.id = store.getId();
            storeDTO.CNPJ = store.getCNPJ();
            storeDTO.name = store.getName();
            storesDTO.Add(storeDTO);
        }

        return storesDTO;
    }

}
