using ListGenius.Domain.Data.Converter.Contract;
using ListGenius.Domain.Model;
using ListGenius.Shared.VO.Product;

namespace ListGenius.Domain.Data.Converter.Implementations
{
    public class ProductConverter : IParser<ProductVO, Product>, IParser<Product, ProductVO>
    {
        public Product Parse(ProductVO origin)
        {
            if (origin == null)
            {
                return new Product();
            }
            return new Product
            {
                Id = origin.Id,
                Name = origin.Name,
                Image = origin.Image,
                Enabled = origin.Enabled,
                Value = origin.Value,
                Unit = origin.Unit,
                Description = origin.Description,
                Link = origin.Link,
                Qrcode = origin.Qrcode,
            };
        }

        public List<Product> Parse(List<ProductVO> origin)
        {
            if (origin == null)
            {
                return new List<Product>();
            }
            return origin.Select(Parse).ToList();
        }

        public ProductVO Parse(Product origin)
        {
            if (origin == null)
            {
                return new ProductVO();
            }
            return new ProductVO
            {
                Id = origin.Id,
                Name = origin.Name,
                Image = origin.Image,
                Enabled = origin.Enabled,
                Value = origin.Value,
                Unit = origin.Unit,
                Description = origin.Description,
                Link = origin.Link,
                Qrcode = origin.Qrcode,
            };
        }

        public List<ProductVO> Parse(List<Product> origin)
        {
            if (origin == null)
            {
                return new List<ProductVO>();
            }
            return origin.Select(Parse).ToList();
        }
    }
}
