using Example1.Models;
using Example1.Models.DTO;

namespace Example1.Abstraction
{
    public interface IProductRepository
    {
        public int AddGroup(GroupDto group, ProductContext context);
        public IEnumerable<GroupDto> GetGroups(ProductContext context);
        public int AddProduct(ProductDto product, ProductContext context);
        public IEnumerable<ProductDto> GetProducts(ProductContext context);
    }
}
