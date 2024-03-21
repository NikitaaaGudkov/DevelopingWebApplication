using StorageService.Dto;
using StorageService.Repo;

namespace StorageService.Mutation
{
    public class MySimpleMutation
    {
        public int AddProduct([Service] IProductRepository service, ProductDto product)
        {
            var id = service.AddProduct(product);
            return id;
        }

        public int AddStorage([Service] IStorageRepository service, StorageDto storage)
        {
            var id = service.AddStorage(storage);
            return id;
        }
    }
}
