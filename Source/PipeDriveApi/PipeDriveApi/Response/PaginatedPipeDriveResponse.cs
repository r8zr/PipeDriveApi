using System.Collections.Generic;
using PipeDriveApi.Models;

namespace PipeDriveApi.Response
{
    public class PaginatedPipeDriveResponse<T> : PipeDriveResponse<List<T>>
    {
        public AdditionalDataInfo AdditionalData { get; set; }
        public class AdditionalDataInfo
        {
            public PaginationInfo Pagination { get; set; }
        }
        
    }
}
