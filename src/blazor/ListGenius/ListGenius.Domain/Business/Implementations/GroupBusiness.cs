using ListGenius.Domain.Business.Interfaces;
using ListGenius.Domain.Data.Converter.Implementations;
using ListGenius.Domain.Repository.GroupRepo;
using ListGenius.Shared.VO.Group;
using ListGenius.Shared.VO.PagedSearch;

namespace ListGenius.Domain.Business.Implementations
{
    public class GroupBusiness : IGroupBusiness
    {
        private readonly GroupConverter _converter;
        private readonly IGroupRepository _repository;


        public GroupBusiness(IGroupRepository repository)
        {
            _repository = repository;
            _converter = new GroupConverter();
        }

        public GroupVO Create(GroupVO group)
        {
            var entity = _converter.Parse(group);
            if (entity != null)
            {
                return _converter.Parse(_repository.Create(entity));
            }
            return new GroupVO();
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public PagedSearchVO<GroupVO> FindWithPagedSearch(
            string name, string sortDirection, int pageSize, int page)
        {
            var sort = (!string.IsNullOrWhiteSpace(sortDirection)) && !sortDirection.Equals("desc") ? "asc" : "desc";
            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            string query = @"select * from `groups` g where 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(name)) query = query + $" and g.Name like '%{name}%' ";
            query += $" order by g.Name {sort} limit {size} offset {offset}";

            string countQuery = @"select count(*) from `groups` g where 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(name)) countQuery = countQuery + $" and g.Name like '%{name}%' ";

            var group = _repository.FindWithPagedSearch(query);
            int totalResults = _repository.GetCount(countQuery);

            return new PagedSearchVO<GroupVO>
            {
                CurrentPage = page,
                List = _converter.Parse(group),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalResults
            };
        }

        public GroupVO FindById(long id)
        {
            return _converter?.Parse(_repository.FindById(id)) ?? new GroupVO();
        }

        public GroupVO Update(GroupVO book)
        {
            var entity = _converter.Parse(book);
            if (entity != null)
            {
                return _converter.Parse(_repository.Update(entity));
            }
            return new GroupVO();
        }

        public List<GroupVO> FindByName(string name)
        {
            return _converter.Parse(_repository.FindByName(name));
        }

        public List<GroupVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public GroupVO Disable(long id)
        {
            var productsList = _repository.Disable(id);
            return _converter.Parse(productsList);
        }
    }
}
