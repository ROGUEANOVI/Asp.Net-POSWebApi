namespace POS.Infrastructure.Commons.Bases.Request
{
    public class BaseFilterRequest: BasePaginationRequest
    {
        public int? NumFilter { set; get; } = null;

        public string? TextFilter { set; get; } = null;
        
        public int? StateFilter { set; get;} = null;

        public string? StartDate { get; set; } = null;

        public string? EndDate { get; set; } = null;

        public bool? Download { get; set; } = false;
    }
}
