using ListGenius.Domain.Data.Converter.Contract;
using ListGenius.Domain.Model;
using ListGenius.Shared.VO.ProductList;

namespace ListGenius.Domain.Data.Converter.Implementations
{
    public class ProductsListConverter : IParser<ProductsListVO, ProductsList>, IParser<ProductsList, ProductsListVO>
    {
        public ProductsList Parse(ProductsListVO origin)
        {
            if (origin == null) return new ProductsList();
            return new ProductsList
            {
                Id = origin.Id,
                CreatedDate = origin.CreatedDate,
                Description = origin.Description,
                Enabled = origin.Enabled,
                Name = origin.Name,
                PublicVisibility = origin.Public,
                TotalProductsCount = origin.TotalProductsCount,
                TotalProductsValue = origin.TotalProductsValue,
                UpdatedDate = origin.UpdatedDate,
                Image = origin.Image,
            };
        }

        public ProductsListVO Parse(ProductsList origin)
        {
            if (origin == null) return new ProductsListVO();
            return new ProductsListVO
            {
                Id = origin.Id,
                CreatedDate = origin.CreatedDate,
                Description = origin.Description,
                Enabled = origin.Enabled,
                Name = origin.Name,
                Public = origin.PublicVisibility,
                TotalProductsCount = origin.TotalProductsCount,
                TotalProductsValue = origin.TotalProductsValue,
                UpdatedDate = origin.UpdatedDate,
                Image = origin.Image,
                ExternalLink = origin.ExternalLink,
            };
        }

        public List<ProductsList> Parse(List<ProductsListVO> origin)
        {
            if (origin == null) return new List<ProductsList>();
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<ProductsListVO> Parse(List<ProductsList> origin)
        {
            if (origin == null) return new List<ProductsListVO>();
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
