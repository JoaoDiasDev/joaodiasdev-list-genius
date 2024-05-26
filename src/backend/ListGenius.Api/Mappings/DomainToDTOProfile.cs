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
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit))
            .ReverseMap();

        CreateMap<ProductsList, ProductsListDTO>()
            .ForMember(dto => dto.UserName, opt => opt.MapFrom(src => src.User.FullName))
            .ReverseMap();

        CreateMap<ProductShared, ProductSharedDTO>()
            .ForMember(dto => dto.GroupName, opt => opt.MapFrom(src => src.ProductGroup.Name))
            .ForMember(dto => dto.SubGroupName, opt => opt.MapFrom(src => src.ProductSubGroup.Name))
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit))
            .ReverseMap();

        CreateMap<ProductGroup, ProductGroupDTO>().ReverseMap();

        CreateMap<ProductSubGroup, ProductSubGroupDTO>()
            .ForMember(dto => dto.GroupName, opt => opt.MapFrom(src => src.ProductGroup.Name))
            .ReverseMap();
    }
}
