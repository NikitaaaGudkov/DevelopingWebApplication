using StorageService.Dto;

namespace StorageService.Repo
{
    public interface IProductRepository
    {
        public int AddProduct(ProductDto product);
    }
}
