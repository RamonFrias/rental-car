namespace RentalCar.Application.Pagination
{
    public class PagedList<T>
    {
        public IReadOnlyList<T> Items { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPage;

        public PagedList(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
        {
            Items = items.ToList();
            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPage = pageSize > 0 ? (int)Math.Ceiling(totalCount / (double)pageSize) : 0;
        }
    }
}
