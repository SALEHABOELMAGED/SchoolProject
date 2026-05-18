using Microsoft.EntityFrameworkCore;

namespace SchoolProject.Core.Wrappers
{
    public static class QueryableExtensions
    {
        public static async Task<PaginatedResult<T>> ToPaginatedResultAsync<T>(
            this IQueryable<T> source, int pageNumber, int pageSize)
            where T : class
        {
            if (source == null)
            {
                throw new Exception("Null");
            }

            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;
            int totalCount = await source.AsNoTracking().CountAsync();
            if (totalCount == 0) return PaginatedResult<T>.Success(new List<T>(), totalCount, pageNumber, pageSize);
            pageNumber = pageNumber - 1;
            var items = await source.Skip(pageNumber * pageSize).Take(pageSize).ToListAsync();

            return PaginatedResult<T>.Success(items, totalCount, pageNumber, pageSize);
        }
    }
}
