using POS.Infrastructure.Commons.Bases.Request;

namespace POS.Infrastructure.Helpers
{
    public static class QueryableHelper
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, BasePaginationRequest paginationRequest)
        {
            return queryable.Skip((paginationRequest.NumPage - 1) * paginationRequest.Records).Take(paginationRequest.Records);
        }
    }
}
