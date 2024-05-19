

namespace ListGenius.Shared.VO.PagedSearch
{
    public class PagedSearchVO<T>
    {
        public int CurrentPage { get; set; } = 1;

        public Dictionary<string, object> Filters { get; set; } = new Dictionary<string, object>();

        public List<T> List { get; set; } = new List<T>();

        public int PageSize { get; set; } = 20;

        public string SortDirections { get; set; } = string.Empty;

        public string SortFields { get; set; } = string.Empty;

        public int TotalResults { get; set; } = int.MaxValue;

        public PagedSearchVO()
        {
        }

        public PagedSearchVO(int currentPage, int pageSize, string sortFields, string sortDirections)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            SortFields = sortFields;
            SortDirections = sortDirections;
        }

        public PagedSearchVO(int currentPage, string sortFields, string sortDirections, List<T> list) : this(
            currentPage,
            10,
            sortFields,
            sortDirections)
        {
        }

        public PagedSearchVO(
            int currentPage,
            int pageSize,
            string sortFields,
            string sortDirections,
            Dictionary<string, object> filters)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            SortFields = sortFields;
            SortDirections = sortDirections;
            Filters = filters;
        }

        public int GetCurrentPage()
        {
            return CurrentPage == 0 ? 2 : CurrentPage;
        }

        public int GetPageSize()
        {
            return PageSize == 0 ? 10 : PageSize;
        }
    }
}