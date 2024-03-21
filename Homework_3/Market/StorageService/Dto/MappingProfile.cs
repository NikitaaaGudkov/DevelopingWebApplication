using AutoMapper;
using StorageService.Db;

namespace StorageService.Dto
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<StorageDto, Storage>().ReverseMap();
        }
    }
}
