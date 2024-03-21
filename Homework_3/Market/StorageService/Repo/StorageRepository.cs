using AutoMapper;
using StorageService.Db;
using StorageService.Dto;

namespace StorageService.Repo
{
    public class StorageRepository : IStorageRepository
    {
        private IMapper _mapper;
        private AppDbContext _context;

        public StorageRepository(IMapper mapper, AppDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public int AddStorage(StorageDto storageDto)
        {
            using (_context)
            {
                Storage storage = _mapper.Map<Storage>(storageDto);
                _context.Storages.Add(storage);
                _context.SaveChanges();
                return storage.Id;
            }
        }

        public List<ProductDto> GetProducts(int storageId)
        {
            using (_context)
            {
                var productList = _context.Products.Where(p => p.StorageId == storageId).Select(x => _mapper.Map<ProductDto>(x)).ToList();
                return productList;
            }
        }
    }
}
