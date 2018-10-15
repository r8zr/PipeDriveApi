namespace PipeDriveApi.Models
{
    public class PaginationInfo
    {
        public int Start { get; set; }
        public int Limit { get; set; }
        public bool MoreItemsInCollection { get; set; }
        public int NextStart { get; set; }
    }
}
