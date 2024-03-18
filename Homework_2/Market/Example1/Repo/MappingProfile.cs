using AutoMapper;
using Example1.Models;
using Example1.Models.DTO;

namespace Example1.Repo
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>(MemberList.Destination).ReverseMap();
            CreateMap<Category, GroupDto>(MemberList.Destination).ReverseMap();
            CreateMap<Storage, StoreDto>(MemberList.Destination).ReverseMap();
        }
    }
}
