using JoaoDiasDev.ProductList.Data.Converter.Contract;
using JoaoDiasDev.ProductList.Data.VO;
using JoaoDiasDev.ProductList.Model;

namespace JoaoDiasDev.ProductList.Data.Converter.Implementations
{
    public class PersonConverter : IParser<PersonVO, Product>, IParser<Product, PersonVO>
    {
        public Product Parse(PersonVO origin)
        {
            if (origin == null)
            {
                return null;
            }
            return new Product
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.Address,
                Gender = origin.Gender
            };
        }

        public List<Product> Parse(List<PersonVO> origin)
        {
            if (origin == null)
            {
                return null;
            }
            return origin.Select(item => Parse(item)).ToList();
        }

        public PersonVO Parse(Product origin)
        {
            if (origin == null)
            {
                return null;
            }
            return new PersonVO
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.Address,
                Gender = origin.Gender
            };
        }

        public List<PersonVO> Parse(List<Product> origin)
        {
            if (origin == null)
            {
                return null;
            }
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
