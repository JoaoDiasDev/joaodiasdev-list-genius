namespace ListGenius.Api.Mappings;

public class DomainToDtoProfile : Profile
{
    public DomainToDtoProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dto => dto.GroupName, opt => opt.MapFrom(src => src.ProductGroup.Name))
            .ForMember(dto => dto.SubGroupName, opt => opt.MapFrom(src => src.ProductSubGroup.Name))
            .ForMember(dto => dto.ShoppingListName, opt => opt.MapFrom(src => src.ProductsList.Name))
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit))
            .ReverseMap()
            .ForPath(src => src.ProductGroup, opt => opt.Ignore())
            .ForPath(src => src.ProductSubGroup, opt => opt.Ignore())
            .ForPath(src => src.ProductsList, opt => opt.Ignore());

        CreateMap<ProductsList, ProductsListDto>()
            .ForMember(dto => dto.UserName, opt => opt.MapFrom(src => src.User!.FullName))
            .ReverseMap();

        CreateMap<ProductShared, ProductSharedDto>()
            .ForMember(dto => dto.GroupName, opt => opt.MapFrom(src => src.ProductGroup.Name))
            .ForMember(dto => dto.SubGroupName, opt => opt.MapFrom(src => src.ProductSubGroup.Name))
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit))
            .ReverseMap()
            .ForPath(src => src.ProductGroup, opt => opt.Ignore())
            .ForPath(src => src.ProductSubGroup, opt => opt.Ignore());

        CreateMap<ProductGroup, ProductGroupDto>().ReverseMap();

        CreateMap<ProductSubGroup, ProductSubGroupDto>()
            .ForMember(dto => dto.GroupName, opt => opt.MapFrom(src => src.ProductGroup.Name))
            .ReverseMap();
    }
}
