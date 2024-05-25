using AutoMapper;
using ListGenius.Api.Entities.ProductGroups;
using ListGenius.Api.Entities.Products;
using ListGenius.Api.Entities.ProductShareds;
using ListGenius.Api.Entities.ProductsLists;
using ListGenius.Api.Entities.ProductSubGroups;

namespace ListGenius.Api.Mappings;

public class DomainToDTOProfile : Profile
{
    public DomainToDTOProfile()
    {
        CreateMap<Product, ProductDTO>()
            .ForMember(dto => dto.GroupName, opt => opt.MapFrom(src => src.ProductGroup.Name))
            .ForMember(dto => dto.SubGroupName, opt => opt.MapFrom(src => src.ProductSubGroup.Name))
            .ForMember(dto => dto.ShoppingListName, opt => opt.MapFrom(src => src.ProductsList.Name))
            .ReverseMap();
        CreateMap<ProductsList, ProductsListDTO>()
            .ForMember(dto => dto.UserName, opt => opt.MapFrom(src => src.User.NormalizedUserName))
            .ReverseMap();
        CreateMap<ProductShared, ProductSharedDTO>()
            .ForMember(dto => dto.GroupName, opt => opt.MapFrom(src => src.ProductGroup.Name))
            .ForMember(dto => dto.SubGroupName, opt => opt.MapFrom(src => src.ProductSubGroup.Name))
            .ReverseMap();
        CreateMap<ProductGroup, ProductGroupDTO>().ReverseMap();
        CreateMap<ProductSubGroup, ProductSubGroupDTO>()
            .ForMember(dto => dto.GroupName, opt => opt.MapFrom(src => src.ProductGroup.Name))
            .ReverseMap();
    }
}
