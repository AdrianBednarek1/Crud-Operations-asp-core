namespace Project.Service.PagingSortingFiltering.Parameters
{
    public class SortParameters : ISortParameters
    {
        public string sortOrder { get; set; }
        public SortParameters(string _sortOrder)
        {
            sortOrder = _sortOrder;
        }
    }
}
