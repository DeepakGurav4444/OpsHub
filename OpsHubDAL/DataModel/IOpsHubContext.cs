using Microsoft.EntityFrameworkCore;
using OpsHubDAL.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpsHubDAL.DataModel
{
    public interface IOpsHubContext : IOpsHubDbContext
    {
        public DbSet<ModModules> ModModules { get; set; }
        public DbSet<RefreshTokens> RefreshTokens { get; set; }

        public DbSet<SubModMap> SubModMap { get; set; }

        public DbSet<SubPlans> SubPlans { get; set; }

        public DbSet<SysAdmins> SysAdmins { get; set; }

        public DbSet<TenModules> TenModules { get; set; }
        public DbSet<TenSubMap> TenSubMap { get; set; }

        public DbSet<TenTenants> TenTenants { get; set; }

        public DbSet<TenUsers> TenUsers { get; set; }
        public DbSet<UserClient> UserClient { get; set; }
        public DbSet<ClientWfSteps> ClientWfSteps { get; set; }

        public DbSet<UserPermissions> UserPermissions { get; set; }

        public DbSet<UserRolePermissions> UserRolePermissions { get; set; }

        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<UserRoleMap> UserRoleMap { get; set; }

        public DbSet<Wf> Wf { get; set; }

        public DbSet<WfSteps> WfSteps { get; set; }
    }
}
  

