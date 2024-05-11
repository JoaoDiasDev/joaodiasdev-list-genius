using JoaoDiasDev.ListGenius.Business.Interfaces;
using JoaoDiasDev.ListGenius.Data.Converter.Implementations;
using JoaoDiasDev.ListGenius.Data.VO;
using JoaoDiasDev.ListGenius.Hypermedia.Utils;
using JoaoDiasDev.ListGenius.Repository.ProductRepo;

namespace JoaoDiasDev.ListGenius.Business.Implementations
{
    public class ProductBusiness : IProductBusiness
    {
        private readonly ProductConverter _converter;
        private readonly IProductRepository _repository;

        public ProductBusiness(IProductRepository repository)
        {
            _repository = repository;
            _converter = new ProductConverter();
        }

        public ProductVO Create(ProductVO product)
        {
            var entity = _converter.Parse(product);
            if (entity != null)
            {
                return _converter.Parse(_repository.Create(entity));
            }
            return new ProductVO();
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public ProductVO Disable(long id)
        {
            var productEntity = _repository.Disable(id);
            return _converter.Parse(productEntity);
        }

        public List<ProductVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public ProductVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public List<ProductVO> FindByName(string name)
        {
            return _converter.Parse(_repository.FindByName(name));
        }

        public PagedSearchVO<ProductVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            var sort = (!string.IsNullOrEmpty(sortDirection)) && !(sortDirection.ToLower().Equals("desc")) ? "asc" : "desc";
            var size = (pageSize > 0) ? pageSize : 10;
            var offset = page > 0 ? (page - 1) * size : 0;

            string query = @"select * from products p where 1 = 1";
            if (!string.IsNullOrEmpty(name))
            {
                query += $"\n and p.name like '%{name.ToLower()}%'";
            }
            query += $"\n order by p.name {sort} limit {size} offset {offset}";

            string countQuery = @"select count(*) from products p where 1 = 1";
            if (!string.IsNullOrEmpty(name))
            {
                countQuery += $"\n and p.name like '%{name.ToLower()}%'";
            }
            var products = _repository.FindWithPagedSearch(query);
            int totalResults = _repository.GetCount(countQuery);
            return new PagedSearchVO<ProductVO>
            {
                CurrentPage = page,
                TotalResults = totalResults,
                SortDirections = sort,
                PageSize = size,
                List = _converter.Parse(products)
            };
        }

        public ProductVO Update(ProductVO product)
        {
            var entity = _converter.Parse(product);
            if (entity != null)
            {
                return _converter.Parse(_repository.Update(entity));
            }
            return new ProductVO();
        }
    }
}
