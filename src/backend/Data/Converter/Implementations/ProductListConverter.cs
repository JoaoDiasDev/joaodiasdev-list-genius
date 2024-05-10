using JoaoDiasDev.ListGenius.Data.Converter.Contract;
using JoaoDiasDev.ListGenius.Data.VO;
using JoaoDiasDev.ListGenius.Model;

namespace JoaoDiasDev.ListGenius.Data.Converter.Implementations
{
    public class ProductListConverter : IParser<ProductsListVO, ProductsList>, IParser<ProductsList, ProductsListVO>
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
                Public = origin.Public,
                TotalProductsCount = origin.TotalProductsCount,
                TotalProductsValue = origin.TotalProductsValue,
                UpdatedDate = origin.UpdatedDate,
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
                Public = origin.Public,
                TotalProductsCount = origin.TotalProductsCount,
                TotalProductsValue = origin.TotalProductsValue,
                UpdatedDate = origin.UpdatedDate,
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
