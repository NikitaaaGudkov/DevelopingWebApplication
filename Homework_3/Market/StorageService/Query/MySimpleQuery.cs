using StorageService.Dto;
using StorageService.Repo;

namespace StorageService.Query
{
    public class MySimpleQuery
    {
        public IEnumerable<ProductDto> GetProducts([Service] IStorageRepository service, int storageId) => service.GetProducts(storageId);
    }
}
