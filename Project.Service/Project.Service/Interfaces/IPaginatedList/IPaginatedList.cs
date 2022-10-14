using System.Data.Entity;
namespace Project.Service.Interfaces
{
    public interface IPaginatedList <T> : IList<T>
    {
        int PageIndex { get; }
        int TotalPages { get; }
        bool HasPreviousPage => PageIndex > 1;
        bool HasNextPage => PageIndex < TotalPages;
        Task<IPaginatedList<T>> CreateAsync
            (IQueryable<T> source, int pageIndex, int pageSize);
        
    }
}
