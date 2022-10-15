using System.Data.Entity;
namespace ZaPrav.NetCore.Interfaces
{
    public interface IPaginatedList <T> : IList<T>
    {
        int PageIndex { get; }
        int TotalPages { get; }
        bool HasPreviousPage => PageIndex > 1;
        bool HasNextPage => PageIndex < TotalPages;     
    }
}
