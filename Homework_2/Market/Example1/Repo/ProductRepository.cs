using AutoMapper;
using Example1.Abstraction;
using Example1.Models;
using Example1.Models.DTO;
using Microsoft.Extensions.Caching.Memory;

namespace Example1.Repo
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public ProductRepository(IMapper mapper, IMemoryCache cache)
        {
            _mapper = mapper;
            _cache = cache;
        }


        public int AddGroup(GroupDto group, ProductContext context)
        {
            using (context)
            {
                var entityGroup = context.Categories.FirstOrDefault(x => x.Name.ToLower() == group.Name.ToLower());
                if(entityGroup == null)
                {
                    entityGroup = _mapper.Map<Category>(group);
                    context.Categories.Add(entityGroup);
                    context.SaveChanges();
                    _cache.Remove("groups");
                }
                return entityGroup.Id;
            }
        }

        public int AddProduct(ProductDto product, ProductContext context)
        {
            using (context)
            {
                var entityProduct = context.Products.FirstOrDefault(x => x.Name.ToLower() == product.Name.ToLower());
                if (entityProduct == null)
                {
                    entityProduct = _mapper.Map<Product>(product);
                    context.Products.Add(entityProduct);
                    context.SaveChanges();
                    _cache.Remove("products");
                }
                return entityProduct.Id;
            }
        }

        public IEnumerable<GroupDto> GetGroups(ProductContext context)
        {
            if (_cache.TryGetValue("groups", out List<GroupDto> groups))
            {
                return groups;
            }

            using (context)
            {
                var groupList = context.Categories.Select(x => _mapper.Map<GroupDto>(x)).ToList();
                _cache.Set("groups", groupList, TimeSpan.FromMinutes(30));
                return groupList;
            }
        }

        public IEnumerable<ProductDto> GetProducts(ProductContext context)
        {
            if (_cache.TryGetValue("products", out List<ProductDto> products))
            {
                return products;
            }

            using (context)
            {
                var productList = context.Products.Select(x => _mapper.Map<ProductDto>(x)).ToList();
                _cache.Set("products", productList, TimeSpan.FromMinutes(30));
                return productList;
            }
        }
    }
}
