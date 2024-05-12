using JoaoDiasDev.ListGenius.Business.Interfaces;
using JoaoDiasDev.ListGenius.Data.Converter.Implementations;
using JoaoDiasDev.ListGenius.Data.VO;
using JoaoDiasDev.ListGenius.Hypermedia.Utils;
using JoaoDiasDev.ListGenius.Repository.SubGroupRepo;

namespace JoaoDiasDev.ListGenius.Business.Implementations
{
    public class SubGroupBusiness : ISubGroupBusiness
    {
        private readonly SubGroupConverter _converter;
        private readonly ISubGroupRepository _repository;


        public SubGroupBusiness(ISubGroupRepository repository)
        {
            _repository = repository;
            _converter = new SubGroupConverter();
        }

        public SubGroupVO Create(SubGroupVO subGroup)
        {
            var entity = _converter.Parse(subGroup);
            if (entity != null)
            {
                return _converter.Parse(_repository.Create(entity));
            }
            return new SubGroupVO();
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public PagedSearchVO<SubGroupVO> FindWithPagedSearch(
            string name, string sortDirection, int pageSize, int page)
        {
            var sort = (!string.IsNullOrWhiteSpace(sortDirection)) && !sortDirection.Equals("desc") ? "asc" : "desc";
            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            string query = @"select * from sub_groups p where 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(name)) query = query + $" and p.Name like '%{name}%' ";
            query += $" order by p.Name {sort} limit {size} offset {offset}";

            string countQuery = @"select count(*) from sub_groups p where 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(name)) countQuery = countQuery + $" and p.Name like '%{name}%' ";

            var subGroup = _repository.FindWithPagedSearch(query);
            int totalResults = _repository.GetCount(countQuery);

            return new PagedSearchVO<SubGroupVO>
            {
                CurrentPage = page,
                List = _converter.Parse(subGroup),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalResults
            };
        }

        public SubGroupVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public SubGroupVO Update(SubGroupVO book)
        {
            var entity = _converter.Parse(book);
            if (entity != null)
            {
                return _converter.Parse(_repository.Update(entity));
            }
            return new SubGroupVO();
        }

        public List<SubGroupVO> FindByName(string name)
        {
            return _converter.Parse(_repository.FindByName(name));
        }

        public List<SubGroupVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public SubGroupVO Disable(long id)
        {
            var productsList = _repository.Disable(id);
            return _converter.Parse(productsList);
        }
    }
}
