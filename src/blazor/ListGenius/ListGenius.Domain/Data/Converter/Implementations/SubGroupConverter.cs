using ListGenius.Domain.Data.Converter.Contract;
using ListGenius.Domain.Model;
using ListGenius.Shared.VO.SubGroup;

namespace ListGenius.Domain.Data.Converter.Implementations
{
    public class SubGroupConverter : IParser<SubGroupVO, ProductSubGroup>, IParser<ProductSubGroup, SubGroupVO>
    {
        public ProductSubGroup Parse(SubGroupVO origin)
        {
            if (origin == null)
            {
                return new ProductSubGroup();
            }
            return new ProductSubGroup
            {
                Id = origin.Id,
                Name = origin.Name,
                Image = origin.Image,
                Enabled = origin.Enabled,
                Description = origin.Description,
                CreatedDate = origin.CreatedDate,
                UpdatedDate = origin.UpdatedDate,
            };
        }

        public List<ProductSubGroup> Parse(List<SubGroupVO> origin)
        {
            if (origin == null)
            {
                return new List<ProductSubGroup>();
            }
            return origin.Select(Parse).ToList();
        }

        public SubGroupVO Parse(ProductSubGroup origin)
        {
            if (origin == null)
            {
                return new SubGroupVO();
            }
            return new SubGroupVO
            {
                Id = origin.Id,
                Name = origin.Name,
                Image = origin.Image,
                Enabled = origin.Enabled,
                Description = origin.Description,
                CreatedDate = origin.CreatedDate,
                UpdatedDate = origin.UpdatedDate,
            };
        }

        public List<SubGroupVO> Parse(List<ProductSubGroup> origin)
        {
            if (origin == null)
            {
                return new List<SubGroupVO>();
            }
            return origin.Select(Parse).ToList();
        }
    }
}
