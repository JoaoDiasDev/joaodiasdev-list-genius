using JoaoDiasDev.ProductList.Data.Converter.Contract;
using JoaoDiasDev.ProductList.Data.VO;
using JoaoDiasDev.ProductList.Model;

namespace JoaoDiasDev.ProductList.Data.Converter.Implementations
{
    public class BookConverter : IParser<BookVO, Model.ProductList>, IParser<Model.ProductList, BookVO>
    {
        public Model.ProductList Parse(BookVO origin)
        {
            if (origin == null) return null;
            return new Model.ProductList
            {
                Id = origin.Id,
                Author = origin.Author,
                LaunchDate = origin.LaunchDate,
                Price = origin.Price,
                Title = origin.Title
            };
        }

        public BookVO Parse(Model.ProductList origin)
        {
            if (origin == null) return null;
            return new BookVO
            {
                Id = origin.Id,
                Author = origin.Author,
                LaunchDate = origin.LaunchDate,
                Price = origin.Price,
                Title = origin.Title
            };
        }

        public List<Model.ProductList> Parse(List<BookVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<BookVO> Parse(List<Model.ProductList> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
