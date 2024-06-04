using ListGenius.Web.Components.Products;

namespace ListGenius.Web.Components.ProductsLists
{
    public class ProductsListPaginationDto
    {
        public List<ProductDto>? Products { get; set; }
        public int PageTotal { get; set; }
    }
}
