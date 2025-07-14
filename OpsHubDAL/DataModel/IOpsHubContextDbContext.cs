using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpsHubDAL.DataModel
{
    public interface IOpsHubDbContext
    {
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellation = default(CancellationToken));
        DatabaseFacade Database { get; }
    }
}
