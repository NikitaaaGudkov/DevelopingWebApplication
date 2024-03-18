using Example1.Abstraction;
using Example1.Models;
using Example1.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Text;

namespace Example1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMemoryCache _cache;

        private ProductContext _context;

        public ProductController(IProductRepository productRepository, IMemoryCache cache, ProductContext context)
        {
            _productRepository = productRepository;
            _cache = cache;
            _context = context;
        }


        [HttpGet("get_cache_stats")]
        public ActionResult<string> GetCacheStats()
        {
            var content = $"Entry count = {_cache.GetCurrentStatistics().CurrentEntryCount}";

            string fileName = "cache_stats" + DateTime.Now.ToBinary().ToString() + ".csv";

            System.IO.File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles", fileName), content);

            return "http://" + Request.Host.ToString() + "/static/" + fileName;
        }


        private string GetCsv(IEnumerable<ProductDto> products)
        {
            StringBuilder sb = new StringBuilder();

            foreach(var product in products)
            {
                sb.AppendLine(product.Id + ";" + product.Name + ";" + product.Description + ";" + product.Cost + ";" + product.CategoryId);
            }
            return sb.ToString();
        }


        [HttpGet("get_products_csv")]
        public FileContentResult GetProductsCsv()
        {
            IEnumerable<ProductDto> products = _productRepository.GetProducts(_context);

            var content = GetCsv(products);

            return File(new UTF8Encoding().GetBytes(content), "text/csv", "products.csv");
        }


        [HttpGet("get_product")]
        public ActionResult<string> GetProducts()
        {
            using (var context = new ProductContext())
            {
                var products = _productRepository.GetProducts(_context);
                return Ok(products);
            }
        }


        [HttpGet("get_group")]
        public ActionResult<string> GetGroups()
        {
            var groups = _productRepository.GetGroups(_context);
            return Ok(groups);
        }
        

        [HttpPost("add_product")]
        public IActionResult AddProduct([FromBody] ProductDto productDto)
        {
            var result = _productRepository.AddProduct(productDto, _context);
            return Ok(result);
        }


        [HttpPost("add_group")]
        public IActionResult AddGroup([FromBody] GroupDto groupDto)
        {
            var result = _productRepository.AddGroup(groupDto, _context);
            return Ok(result);
        }


        [HttpDelete("delete_product")]
        public IActionResult DeleteProduct([FromQuery] string name)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    var product = context.Products.FirstOrDefault(p => p.Name.ToLower() == name.ToLower());
                    if (product == null)
                    {
                        return NotFound($"Не найден продукт с именем {name}");
                    }
                    context.Products.Remove(product);
                    context.SaveChanges();
                    return Ok();
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }


        [HttpPatch("change_cost")]
        public IActionResult ChangeCost([FromQuery] string name, int newCost)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    var product = context.Products.FirstOrDefault(p => p.Name.ToLower() == name.ToLower());
                    if (product == null)
                    {
                        return NotFound($"Не найден продукт с именем {name}");
                    }
                    product.Cost = newCost;
                    context.SaveChanges();
                    return Ok();
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
