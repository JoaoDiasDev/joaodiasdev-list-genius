using JoaoDiasDev.ListGenius.Business.Interfaces;
using JoaoDiasDev.ListGenius.Data.Converter.Implementations;
using JoaoDiasDev.ListGenius.Data.VO;
using JoaoDiasDev.ListGenius.Hypermedia.Utils;
using JoaoDiasDev.ListGenius.Repository.ProductListRepo;

namespace JoaoDiasDev.ListGenius.Business.Implementations
{
    public class ProductsListBusiness : IProductsListBusiness
    {
        private readonly ProductListConverter _converter;
        private readonly IProductsListRepository _repository;


        public ProductsListBusiness(IProductsListRepository repository)
        {
            _repository = repository;
            _converter = new ProductListConverter();
        }

        public ProductsListVO Create(ProductsListVO productsList)
        {
            var entity = _converter.Parse(productsList);
            if (entity != null)
            {
                return _converter.Parse(_repository.Create(entity));
            }
            return new ProductsListVO();
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public PagedSearchVO<ProductsListVO> FindWithPagedSearch(
            string name, string sortDirection, int pageSize, int page)
        {
            var sort = (!string.IsNullOrWhiteSpace(sortDirection)) && !sortDirection.Equals("desc") ? "asc" : "desc";
            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            string query = @"select * from products_list p where 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(name)) query = query + $" and p.Name like '%{name}%' ";
            query += $" order by p.Name {sort} limit {size} offset {offset}";

            string countQuery = @"select count(*) from products_list p where 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(name)) countQuery = countQuery + $" and p.Name like '%{name}%' ";

            var productsList = _repository.FindWithPagedSearch(query);
            int totalResults = _repository.GetCount(countQuery);

            return new PagedSearchVO<ProductsListVO>
            {
                CurrentPage = page,
                List = _converter.Parse(productsList),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalResults
            };
        }

        public ProductsListVO FindByID(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public ProductsListVO Update(ProductsListVO book)
        {
            var entity = _converter.Parse(book);
            if (entity != null)
            {
                return _converter.Parse(_repository.Update(entity));
            }
            return new ProductsListVO();
        }

        public List<ProductsListVO> FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<ProductsListVO> FindAll()
        {
            throw new NotImplementedException();
        }

        public ProductsListVO Disable(long id)
        {
            throw new NotImplementedException();
        }
    }
}
