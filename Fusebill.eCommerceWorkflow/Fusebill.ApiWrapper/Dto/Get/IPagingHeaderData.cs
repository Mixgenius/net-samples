namespace Fusebill.ApiWrapper.Dto.Get
{
    public interface IPagingHeaderData
    {
        long Count { get; set; }
        long CurrentPage { get; set; }
        long PreviousPage { get; set; }
        long NextPage { get; set; }
        long MaxCount { get; set; }
        long PageSize { get; set; }
        long MaxPageIndex { get; set; }
        string SortExpression { get; set; }
        string SortOrder { get; set; }
    }
}