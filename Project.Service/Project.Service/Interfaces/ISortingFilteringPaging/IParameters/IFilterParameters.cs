namespace Project.Service.PagingSortingFiltering.Parameters
{
    public interface IFilterParameters
    {
        string? searchString { get; }
        string currentFilter { get; }
        string GetCurrentSearch();
    }
}