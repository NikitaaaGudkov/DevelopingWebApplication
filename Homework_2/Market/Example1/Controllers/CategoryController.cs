using Example1.Models;
using Example1.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Example1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        [HttpDelete("deleteCategory")]
        public IActionResult DeleteCategories([FromQuery] string name)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    var category = context.Categories.FirstOrDefault(c => c.Name.ToLower() == name.ToLower());
                    if (category == null)
                    {
                        return NotFound($"Не найдена категория с именем {name}");
                    }
                    context.Categories.Remove(category);
                    context.SaveChanges();
                    return Ok();
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }


        [HttpPost("postCategory")]
        public IActionResult PostCategories([FromQuery] string name, string? description)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    var category = context.Categories.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
                    if (category == null)
                    {
                        context.Add(new Category() { Name = name, Description = description});
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
    }
}
