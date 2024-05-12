using JoaoDiasDev.ListGenius.Data.Converter.Contract;
using JoaoDiasDev.ListGenius.Data.VO;
using JoaoDiasDev.ListGenius.Model;

namespace JoaoDiasDev.ListGenius.Data.Converter.Implementations
{
    public class SubGroupConverter : IParser<SubGroupVO, SubGroup>, IParser<SubGroup, SubGroupVO>
    {
        public SubGroup Parse(SubGroupVO origin)
        {
            if (origin == null)
            {
                return new SubGroup();
            }
            return new SubGroup
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

        public List<SubGroup> Parse(List<SubGroupVO> origin)
        {
            if (origin == null)
            {
                return new List<SubGroup>();
            }
            return origin.Select(Parse).ToList();
        }

        public SubGroupVO Parse(SubGroup origin)
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

        public List<SubGroupVO> Parse(List<SubGroup> origin)
        {
            if (origin == null)
            {
                return new List<SubGroupVO>();
            }
            return origin.Select(Parse).ToList();
        }
    }
}
