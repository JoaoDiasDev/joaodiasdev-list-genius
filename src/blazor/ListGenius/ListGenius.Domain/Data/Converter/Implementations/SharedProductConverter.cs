using ListGenius.Domain.Data.Converter.Contract;
using ListGenius.Domain.Model;
using ListGenius.Shared.VO.Product;

namespace ListGenius.Domain.Data.Converter.Implementations
{
    public class SharedProductConverter : IParser<SharedProductVO, SharedProduct>, IParser<SharedProduct, SharedProductVO>
    {
        public SharedProduct Parse(SharedProductVO origin)
        {
            if (origin == null) return new SharedProduct();
            return new SharedProduct
            {
                Id = origin.Id,
                CreatedDate = origin.CreatedDate,
                Description = origin.Description,
                Enabled = origin.Enabled,
                Name = origin.Name,
                UpdatedDate = origin.UpdatedDate,
                Image = origin.Image,
                Link = origin.Link,
                Qrcode = origin.Qrcode,
                Unit = origin.Unit,
                Value = origin.Value,
            };
        }

        public SharedProductVO Parse(SharedProduct origin)
        {
            if (origin == null) return new SharedProductVO();
            return new SharedProductVO
            {
                Id = origin.Id,
                CreatedDate = origin.CreatedDate,
                Description = origin.Description,
                Enabled = origin.Enabled,
                Name = origin.Name,
                UpdatedDate = origin.UpdatedDate,
                Image = origin.Image,
                Link = origin.Link,
                Qrcode = origin.Qrcode,
                Unit = origin.Unit,
                Value = origin.Value,
            };
        }

        public List<SharedProduct> Parse(List<SharedProductVO> origin)
        {
            if (origin == null) return new List<SharedProduct>();
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<SharedProductVO> Parse(List<SharedProduct> origin)
        {
            if (origin == null) return new List<SharedProductVO>();
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
