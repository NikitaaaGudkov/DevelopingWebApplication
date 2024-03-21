using Microsoft.AspNetCore.Mvc;
using StorageService.Dto;
using StorageService.Repo;

namespace StorageService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpPost(template:"AddProduct")]
        public ActionResult AddProduct(ProductDto product)
        {
            return Ok(_repository.AddProduct(product));
        }
    }
}
