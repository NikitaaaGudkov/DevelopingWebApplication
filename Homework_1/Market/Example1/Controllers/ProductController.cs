using Example1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Example1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpGet("getProduct")]
        public ActionResult<string> GetProducts()
        {
            try
            {
                using (var context = new ProductContext())
                {
                    var products = context.Products.Select(x => new Product() { Id = x.Id, Name = x.Name, Description = x.Description });
                    var resultString = string.Empty;
                    foreach (var product in products)
                    {
                        resultString += $"Product name = {product.Name}; Cost = {product.Cost}\n";
                    }
                    return Ok(resultString);
                }
            }
            catch 
            {
                return StatusCode(500);
            }
        }

        [HttpPost("postProduct")]
        public IActionResult PostProducts([FromQuery] string name, string? description, string categoryName, int cost)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    var category = context.Categories.FirstOrDefault(c => c.Name.ToLower() == categoryName.ToLower());
                    if (category == null)
                    {
                        return BadRequest("Указанной категории не существует");
                    }

                    if(!context.Products.Any(x => x.Name.ToLower() == name.ToLower()))
                    {
                        context.Products.Add(new Product() { Name = name, Description = description, Category = category, Cost = cost });
                        context.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(409);
                    }
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("deleteProduct")]
        public IActionResult DeleteProducts([FromQuery] string name)
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

        [HttpPatch("patchProduct")]
        public IActionResult PatchProducts([FromQuery] string name, int newCost)
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
