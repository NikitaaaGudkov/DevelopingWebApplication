using AutoMapper;
using StorageService.Db;
using StorageService.Dto;

namespace StorageService.Repo
{
    public class ProductRepository : IProductRepository
    {
        private IMapper _mapper;
        private AppDbContext _context;

        public ProductRepository(IMapper mapper, AppDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public int AddProduct(ProductDto productDto)
        {
            using (_context)
            {
                Product product = _mapper.Map<Product>(productDto);
                _context.Products.Add(product);
                _context.SaveChanges();
                return product.Id;
            }
        }
    }
}
