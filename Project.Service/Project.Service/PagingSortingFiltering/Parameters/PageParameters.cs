namespace Project.Service.PagingSortingFiltering.Parameters
{
    public class PageParameters : IPageParameters
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public PageParameters(int? _pageIndex, int _pageSize)
        {
            pageIndex = _pageIndex ?? 1;
            pageSize = _pageSize;
        }
    }
}
