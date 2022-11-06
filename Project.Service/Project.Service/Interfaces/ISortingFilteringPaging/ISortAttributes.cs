namespace ZaPrav.NetCore.Interfaces.ISortingHelp
{
    public interface ISortAttributes
    {
        string? NameSort { get; }
        string? AbrvSort { get; }
        string? IdSort { get; }
        string? ForeignIdSort { get; }
    }
}
