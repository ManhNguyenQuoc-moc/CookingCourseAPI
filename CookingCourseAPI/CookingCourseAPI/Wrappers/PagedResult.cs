namespace CookingCourseAPI.Wrappers
{
    public class PagedResult<T>
    {
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public List<T> Data { get; set; }

        public PagedResult(List<T> data, int totalCount, int pageIndex, int pageSize)
        {
            Data = data;
            TotalCount = totalCount;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
}
