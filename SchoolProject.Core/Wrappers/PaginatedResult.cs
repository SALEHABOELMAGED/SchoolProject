namespace SchoolProject.Core.Wrappers
{

    public class PaginatedResult<T>
    {
        public PaginatedResult(List<T> data)
        {
            Data = data;
        }
        #region Properties
        public List<T> Data { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int Meta { get; set; }
        public int PageSize { get; set; }
        public bool Succeeded { get; }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
        public List<string> Messages { get; set; } = new();
        #endregion

        #region Constructors
        public PaginatedResult(bool succeeded, List<T> data = default, List<string> messages = null, int totalCount = 0, int currentPage = 1, int pageSize = 10)
        {
            Data = data;
            TotalCount = totalCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
            Succeeded = succeeded;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        }
        #endregion

        #region Methods
        public static PaginatedResult<T> Success(List<T> data, int totalCount, int currentPage, int pageSize)
        {
            return new(true, data, null, totalCount, currentPage, pageSize);
        }
        #endregion
    }
}

