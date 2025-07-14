using System;
using System.Collections.Generic;
using System.Text;

namespace OpsHubDTOModel.Paging
{
    public interface IPagingParameter
    {
        int? PageNumber { get; set; }
        int? PageSize { get; set; }
    }
}
