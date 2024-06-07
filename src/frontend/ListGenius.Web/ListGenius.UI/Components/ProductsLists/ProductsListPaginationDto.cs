using ListGenius.UI.Components.Products;

namespace ListGenius.UI.Components.ProductsLists
{
    public class ProductsListPaginationDto
    {
        public List<ProductDto>? Products { get; set; }
        public int PageTotal { get; set; }
    }
}
