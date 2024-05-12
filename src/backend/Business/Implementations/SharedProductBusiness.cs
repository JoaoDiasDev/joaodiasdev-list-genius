using JoaoDiasDev.ListGenius.Business.Interfaces;
using JoaoDiasDev.ListGenius.Data.Converter.Implementations;
using JoaoDiasDev.ListGenius.Data.VO;
using JoaoDiasDev.ListGenius.Hypermedia.Utils;
using JoaoDiasDev.ListGenius.Repository.SharedProductRepo;

namespace JoaoDiasDev.ListGenius.Business.Implementations
{
    public class SharedProductBusiness : ISharedProductBusiness
    {
        private readonly SharedProductConverter _converter;
        private readonly ISharedProductRepository _repository;


        public SharedProductBusiness(ISharedProductRepository repository)
        {
            _repository = repository;
            _converter = new SharedProductConverter();
        }

        public SharedProductVO Create(SharedProductVO sharedProduct)
        {
            var entity = _converter.Parse(sharedProduct);
            if (entity != null)
            {
                return _converter.Parse(_repository.Create(entity));
            }
            return new SharedProductVO();
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public PagedSearchVO<SharedProductVO> FindWithPagedSearch(
            string name, string sortDirection, int pageSize, int page)
        {
            var sort = (!string.IsNullOrWhiteSpace(sortDirection)) && !sortDirection.Equals("desc") ? "asc" : "desc";
            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            string query = @"select * from shared_products p where 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(name)) query = query + $" and p.Name like '%{name}%' ";
            query += $" order by p.Name {sort} limit {size} offset {offset}";

            string countQuery = @"select count(*) from shared_products p where 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(name)) countQuery = countQuery + $" and p.Name like '%{name}%' ";

            var sharedProduct = _repository.FindWithPagedSearch(query);
            int totalResults = _repository.GetCount(countQuery);

            return new PagedSearchVO<SharedProductVO>
            {
                CurrentPage = page,
                List = _converter.Parse(sharedProduct),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalResults
            };
        }

        public SharedProductVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public SharedProductVO Update(SharedProductVO book)
        {
            var entity = _converter.Parse(book);
            if (entity != null)
            {
                return _converter.Parse(_repository.Update(entity));
            }
            return new SharedProductVO();
        }

        public List<SharedProductVO> FindByName(string name)
        {
            return _converter.Parse(_repository.FindByName(name));
        }

        public List<SharedProductVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public SharedProductVO Disable(long id)
        {
            var sharedProduct = _repository.Disable(id);
            return _converter.Parse(sharedProduct);
        }
    }
}
