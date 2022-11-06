namespace Project.Service.PagingSortingFiltering.Parameters
{
    public interface IPageParameters
    {
        int pageIndex { get; }
        int pageSize { get; }
    }
}