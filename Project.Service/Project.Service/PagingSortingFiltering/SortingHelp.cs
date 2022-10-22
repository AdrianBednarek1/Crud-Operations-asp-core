using ZaPrav.NetCore.Interfaces.ISortingHelp;

namespace ZaPrav.NetCore
{
    public class SortingHelp : ISortingHelp
    {
        public string? NameSort { get; set; } 
        public string? AbrvSort { get; set; } 
        public string? IdSort { get; set; } 
        public string? ForeignIdSort { get; set; } 
        public string? CurrentSort { get; set; }
    }
}
