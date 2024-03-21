using Microsoft.AspNetCore.Mvc;
using StorageService.Dto;
using StorageService.Repo;

namespace StorageService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StorageController : ControllerBase
    {
        private IStorageRepository _repository;

        public StorageController(IStorageRepository repository)
        {
            _repository = repository;
        }

        [HttpPost(template: "AddStorage")]
        public ActionResult AddStorage(StorageDto storage)
        {
            _repository.AddStorage(storage);
            return Ok();
        }

        [HttpGet(template: "GetProducts")]
        public ActionResult GetProducts(int storageId)
        {
            return Ok(_repository.GetProducts(storageId));
        }

    }
}
