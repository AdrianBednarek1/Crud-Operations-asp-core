namespace Project.Service.PagingSortingFiltering.Parameters
{
    public class FilterParameters
    {
        public string? searchString { get; set; }
        public string currentFilter { get; set; }
        public FilterParameters(string? _searchString, string _currentFilter)
        {
            searchString = _searchString;
            currentFilter = _currentFilter;
        }
        public string GetCurrentSearch()
        {
            if (searchString == null)
            {
                searchString = currentFilter;
            }
            return searchString;
        }
    }
}
