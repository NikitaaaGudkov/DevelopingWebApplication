using StorageService.Dto;

namespace StorageService.Repo
{
    public interface IStorageRepository
    {
        public int AddStorage(StorageDto storageDto);
        public List<ProductDto> GetProducts(int StorageId);
    }
}
