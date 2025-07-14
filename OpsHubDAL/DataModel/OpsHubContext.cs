using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace OpsHubDAL.DataModel;

public partial class OpsHubContext : DbContext, IOpsHubContext
{
    //public OpsHubContext()
    //{
    //}

    public OpsHubContext(DbContextOptions<OpsHubContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ClientWfSteps> ClientWfSteps { get; set; }

    public virtual DbSet<ModModules> ModModules { get; set; }

    public virtual DbSet<RefreshTokens> RefreshTokens { get; set; }

    public virtual DbSet<RoleRoles> RoleRoles { get; set; }

    public virtual DbSet<SubModMap> SubModMap { get; set; }

    public virtual DbSet<SubPlans> SubPlans { get; set; }

    public virtual DbSet<SysAdmins> SysAdmins { get; set; }

    public virtual DbSet<TenModules> TenModules { get; set; }

    public virtual DbSet<TenSubMap> TenSubMap { get; set; }

    public virtual DbSet<TenTenants> TenTenants { get; set; }

    public virtual DbSet<TenUsers> TenUsers { get; set; }

    public virtual DbSet<UserClient> UserClient { get; set; }

    public virtual DbSet<UserPermissions> UserPermissions { get; set; }

    public virtual DbSet<UserRoleMap> UserRoleMap { get; set; }

    public virtual DbSet<UserRolePermissions> UserRolePermissions { get; set; }

    public virtual DbSet<UserRoles> UserRoles { get; set; }

    public virtual DbSet<Wf> Wf { get; set; }

    public virtual DbSet<WfSteps> WfSteps { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//    => optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=root;database=ops_hub_new", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.34-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<ClientWfSteps>(entity =>
        {
            entity.HasKey(e => e.IdclientWfStep).HasName("PRIMARY");

            entity.ToTable("client_wf_steps");

            entity.HasIndex(e => e.ClientId, "fk_cl_wf_step_cl_id_idx");

            entity.HasIndex(e => e.UserId, "fk_cl_wf_step_user_id_idx");

            entity.HasIndex(e => e.WfStepId, "fk_cl_wf_step_wf_step_id_idx");

            entity.Property(e => e.IdclientWfStep).HasColumnName("idclient_wf_step");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.CompletedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("completed_at");
            entity.Property(e => e.StepStatus)
                .IsRequired()
                .HasMaxLength(20)
                .HasDefaultValueSql("'Pending'")
                .HasColumnName("step_status");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.WfStepId).HasColumnName("wf_step_id");

            entity.HasOne(d => d.Client).WithMany(p => p.ClientWfSteps)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cl_wf_step_cl_id");

            entity.HasOne(d => d.User).WithMany(p => p.ClientWfSteps)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cl_wf_step_user_id");

            entity.HasOne(d => d.WfStep).WithMany(p => p.ClientWfSteps)
                .HasForeignKey(d => d.WfStepId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cl_wf_step_wf_step_id");
        });

        modelBuilder.Entity<ModModules>(entity =>
        {
            entity.HasKey(e => e.IdmodModules).HasName("PRIMARY");

            entity.ToTable("mod_modules");

            entity.Property(e => e.IdmodModules).HasColumnName("idmod_modules");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.IconUrl)
                .IsRequired()
                .HasMaxLength(1000)
                .HasColumnName("icon_url");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.ModuleName)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("module_name");
        });

        modelBuilder.Entity<RefreshTokens>(entity =>
        {
            entity.HasKey(e => e.IdRefreshToken).HasName("PRIMARY");

            entity.ToTable("refresh_tokens");

            entity.HasIndex(e => e.ExpiresAt, "idx_expire_at");

            entity.HasIndex(e => new { e.UserId, e.UserType, e.IsUsed, e.IsRevoked }, "idx_id_type_used_revoked");

            entity.HasIndex(e => e.Token, "idx_uniq_token").IsUnique();

            entity.Property(e => e.IdRefreshToken).HasColumnName("idRefresh_Token");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DeviceId)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("device_id");
            entity.Property(e => e.ExpiresAt)
                .HasColumnType("datetime")
                .HasColumnName("expires_at");
            entity.Property(e => e.IsRevoked)
                .HasDefaultValueSql("'0'")
                .HasColumnName("is_revoked");
            entity.Property(e => e.IsUsed)
                .HasDefaultValueSql("'0'")
                .HasColumnName("is_used");
            entity.Property(e => e.Token)
                .IsRequired()
                .HasColumnName("token");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.UserType)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("user_type");
        });

        modelBuilder.Entity<RoleRoles>(entity =>
        {
            entity.HasKey(e => e.IdroleRoles).HasName("PRIMARY");

            entity.ToTable("role_roles");

            entity.HasIndex(e => e.RoleName, "role_name").IsUnique();

            entity.Property(e => e.IdroleRoles).HasColumnName("idrole_roles");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.IsForAdmin)
                .HasDefaultValueSql("'0'")
                .HasColumnName("is_for_admin");
            entity.Property(e => e.RoleName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<SubModMap>(entity =>
        {
            entity.HasKey(e => e.IdsubModMap).HasName("PRIMARY");

            entity.ToTable("sub_mod_map");

            entity.HasIndex(e => e.ModuleId, "fk_map_module_id_idx");

            entity.HasIndex(e => e.SubPlanId, "fk_map_sub_plan_id_idx");

            entity.HasIndex(e => new { e.SubPlanId, e.ModuleId }, "idx_sub_mod_map");

            entity.Property(e => e.IdsubModMap).HasColumnName("idsub_mod_map");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.ModuleId).HasColumnName("module_id");
            entity.Property(e => e.SubPlanId).HasColumnName("sub_plan_id");

            entity.HasOne(d => d.Module).WithMany(p => p.SubModMap)
                .HasForeignKey(d => d.ModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_map_module_id");

            entity.HasOne(d => d.SubPlan).WithMany(p => p.SubModMap)
                .HasForeignKey(d => d.SubPlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_map_sub_plan_id");
        });

        modelBuilder.Entity<SubPlans>(entity =>
        {
            entity.HasKey(e => e.IdsubPlans).HasName("PRIMARY");

            entity.ToTable("sub_plans");

            entity.Property(e => e.IdsubPlans).HasColumnName("idsub_plans");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DurationInMonth).HasColumnName("duration_in_month");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.MaxUsers).HasColumnName("max_users");
            entity.Property(e => e.PlanName)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("plan_name");
            entity.Property(e => e.Price)
                .HasPrecision(14, 2)
                .HasColumnName("price");
        });

        modelBuilder.Entity<SysAdmins>(entity =>
        {
            entity.HasKey(e => e.IdsysAdmins).HasName("PRIMARY");

            entity.ToTable("sys_admins");

            entity.HasIndex(e => new { e.EmailId, e.UserPass }, "idx_email_pass");

            entity.Property(e => e.IdsysAdmins).HasColumnName("idsys_admins");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.EmailId)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("email_id");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("user_name");
            entity.Property(e => e.UserPass)
                .IsRequired()
                .HasMaxLength(64)
                .HasColumnName("user_pass");
        });

        modelBuilder.Entity<TenModules>(entity =>
        {
            entity.HasKey(e => e.IdtenModules).HasName("PRIMARY");

            entity.ToTable("ten_modules");

            entity.HasIndex(e => e.ModulesId, "fk_ten_mod_modules_id_idx");

            entity.HasIndex(e => e.TenantId, "fk_ten_mod_tenant_id_idx");

            entity.HasIndex(e => new { e.TenantId, e.ModulesId, e.IsActive }, "idx_tenid_moduleid_active");

            entity.Property(e => e.IdtenModules).HasColumnName("idten_modules");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.ExpiresOn)
                .HasColumnType("timestamp")
                .HasColumnName("expires_on");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.ModulesId).HasColumnName("modules_id");
            entity.Property(e => e.PurchaseDate).HasColumnName("purchase_date");
            entity.Property(e => e.TenantId).HasColumnName("tenant_id");

            entity.HasOne(d => d.Modules).WithMany(p => p.TenModules)
                .HasForeignKey(d => d.ModulesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ten_mod_modules_id");

            entity.HasOne(d => d.Tenant).WithMany(p => p.TenModules)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ten_mod_tenant_id");
        });

        modelBuilder.Entity<TenSubMap>(entity =>
        {
            entity.HasKey(e => e.IdtenSubMap).HasName("PRIMARY");

            entity.ToTable("ten_sub_map");

            entity.HasIndex(e => e.SubPlanId, "fk_ten_sub_map_subplan_id_idx");

            entity.HasIndex(e => e.TenantId, "fk_ten_sub_map_tenant_id_idx");

            entity.HasIndex(e => new { e.TenantId, e.SubPlanId, e.IsActive }, "idx_tenid_subid_active");

            entity.Property(e => e.IdtenSubMap).HasColumnName("idten_sub_map");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.SubPlanId).HasColumnName("sub_plan_id");
            entity.Property(e => e.TenantId).HasColumnName("tenant_id");

            entity.HasOne(d => d.SubPlan).WithMany(p => p.TenSubMap)
                .HasForeignKey(d => d.SubPlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ten_sub_map_subplan_id");

            entity.HasOne(d => d.Tenant).WithMany(p => p.TenSubMap)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ten_sub_map_tenant_id");
        });

        modelBuilder.Entity<TenTenants>(entity =>
        {
            entity.HasKey(e => e.IdtenTenants).HasName("PRIMARY");

            entity.ToTable("ten_tenants");

            entity.HasIndex(e => new { e.EmailId, e.TenPass }, "idx_email_pass");

            entity.HasIndex(e => new { e.TenantName, e.EmailId, e.Domain }, "idx_ser_name_email_domain").HasAnnotation("MySql:FullTextIndex", true);

            entity.Property(e => e.IdtenTenants).HasColumnName("idten_tenants");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Domain)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("domain");
            entity.Property(e => e.EmailId)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("email_Id");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.LogoPath)
                .IsRequired()
                .HasMaxLength(1000)
                .HasColumnName("logo_path");
            entity.Property(e => e.TenPass)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("ten_pass");
            entity.Property(e => e.TenantName)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("tenant_name");
        });

        modelBuilder.Entity<TenUsers>(entity =>
        {
            entity.HasKey(e => e.IdtenUsers).HasName("PRIMARY");

            entity.ToTable("ten_users");

            entity.HasIndex(e => e.ModulesId, "fk_ten_user_mod_id_idx");

            entity.HasIndex(e => e.PrincipalId, "fk_ten_user_principal_id_idx");

            entity.HasIndex(e => new { e.EmailId, e.TenUserPass }, "idx_email_pass");

            entity.HasIndex(e => new { e.TenUserName, e.EmailId }, "idx_ser_name_email").HasAnnotation("MySql:FullTextIndex", true);

            entity.Property(e => e.IdtenUsers).HasColumnName("idten_users");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.EmailId)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("email_id");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.IsAdmin)
                .HasDefaultValueSql("'0'")
                .HasColumnName("is_admin");
            entity.Property(e => e.ModulesId).HasColumnName("modules_id");
            entity.Property(e => e.PrincipalId).HasColumnName("principal_id");
            entity.Property(e => e.TenUserName)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("ten_user_name");
            entity.Property(e => e.TenUserPass)
                .IsRequired()
                .HasMaxLength(64)
                .HasColumnName("ten_user_pass");
            entity.Property(e => e.TenantId).HasColumnName("tenant_id");

            entity.HasOne(d => d.Modules).WithMany(p => p.TenUsers)
                .HasForeignKey(d => d.ModulesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ten_user_mod_id");

            entity.HasOne(d => d.Principal).WithMany(p => p.InversePrincipal)
                .HasForeignKey(d => d.PrincipalId)
                .HasConstraintName("fk_ten_user_principal_id");
        });

        modelBuilder.Entity<UserClient>(entity =>
        {
            entity.HasKey(e => e.IduserClient).HasName("PRIMARY");

            entity.ToTable("user_client");

            entity.HasIndex(e => e.ModuleId, "fk_user_client_module_id_idx");

            entity.HasIndex(e => e.UserId, "fk_user_client_user_id_idx");

            entity.HasIndex(e => e.ClientName, "idx_client_name_ser").HasAnnotation("MySql:FullTextIndex", true);

            entity.Property(e => e.IduserClient).HasColumnName("iduser_client");
            entity.Property(e => e.ClientName)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("client_name");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.FirebaseToken)
                .HasMaxLength(1000)
                .HasColumnName("firebase_token");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.ModuleId).HasColumnName("module_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Module).WithMany(p => p.UserClient)
                .HasForeignKey(d => d.ModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_client_module_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserClient)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_client_user_id");
        });

        modelBuilder.Entity<UserPermissions>(entity =>
        {
            entity.HasKey(e => e.IduserPermissions).HasName("PRIMARY");

            entity.ToTable("user_permissions");

            entity.Property(e => e.IduserPermissions).HasColumnName("iduser_permissions");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("description");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.PermissionName)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("permission_name");
        });

        modelBuilder.Entity<UserRoleMap>(entity =>
        {
            entity.HasKey(e => e.IduserRoleMap).HasName("PRIMARY");

            entity.ToTable("user_role_map");

            entity.HasIndex(e => e.RoleId, "fk_user_role_map_role_id_idx");

            entity.HasIndex(e => e.TenUserId, "fk_user_role_map_user_id_idx");

            entity.HasIndex(e => new { e.TenUserId, e.RoleId }, "idx_ten_user_role_id");

            entity.Property(e => e.IduserRoleMap).HasColumnName("iduser_role_map");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.TenUserId).HasColumnName("ten_user_id");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoleMap)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_role_map_role_id");

            entity.HasOne(d => d.TenUser).WithMany(p => p.UserRoleMap)
                .HasForeignKey(d => d.TenUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_role_map_user_id");
        });

        modelBuilder.Entity<UserRolePermissions>(entity =>
        {
            entity.HasKey(e => e.IduserRolePermissions).HasName("PRIMARY");

            entity.ToTable("user_role_permissions");

            entity.HasIndex(e => e.ModuleId, "fk_us_rol_per_module_id_idx");

            entity.HasIndex(e => e.PermissionId, "fk_us_rol_per_pem_id_idx");

            entity.HasIndex(e => e.RoleId, "fk_us_rol_per_role_id_idx");

            entity.HasIndex(e => new { e.RoleId, e.PermissionId, e.ModuleId }, "idx_role_per_mod_id");

            entity.Property(e => e.IduserRolePermissions).HasColumnName("iduser_role_permissions");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.ModuleId).HasColumnName("module_id");
            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Module).WithMany(p => p.UserRolePermissions)
                .HasForeignKey(d => d.ModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_us_rol_per_module_id");

            entity.HasOne(d => d.Permission).WithMany(p => p.UserRolePermissions)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_us_rol_per_pem_id");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRolePermissions)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_us_rol_per_role_id");
        });

        modelBuilder.Entity<UserRoles>(entity =>
        {
            entity.HasKey(e => e.IduserRoles).HasName("PRIMARY");

            entity.ToTable("user_roles");

            entity.HasIndex(e => e.ModuleId, "fk_us_role_module_id_idx");

            entity.HasIndex(e => e.TenantId, "fk_us_role_ten_id_idx");

            entity.Property(e => e.IduserRoles).HasColumnName("iduser_roles");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.IsForAdmin).HasColumnName("is_for_admin");
            entity.Property(e => e.ModuleId).HasColumnName("module_id");
            entity.Property(e => e.RoleName)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("role_name");
            entity.Property(e => e.TenantId).HasColumnName("tenant_id");

            entity.HasOne(d => d.Module).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.ModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_us_role_module_id");

            entity.HasOne(d => d.Tenant).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_us_tenant_id");
        });

        modelBuilder.Entity<Wf>(entity =>
        {
            entity.HasKey(e => e.Idwf).HasName("PRIMARY");

            entity.ToTable("wf");

            entity.HasIndex(e => e.ModuleId, "fk_wf_module_id_idx");

            entity.HasIndex(e => e.TenantId, "fk_wf_ten_id_idx");

            entity.HasIndex(e => new { e.ModuleId, e.TenantId }, "idx_mod_ten_id");

            entity.Property(e => e.Idwf).HasColumnName("idwf");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.ModuleId).HasColumnName("module_id");
            entity.Property(e => e.TenantId).HasColumnName("tenant_id");
            entity.Property(e => e.WfName)
                .IsRequired()
                .HasMaxLength(60)
                .HasColumnName("wf_name");

            entity.HasOne(d => d.Module).WithMany(p => p.Wf)
                .HasForeignKey(d => d.ModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_wf_module_id");

            entity.HasOne(d => d.Tenant).WithMany(p => p.Wf)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_wf_ten_id");
        });

        modelBuilder.Entity<WfSteps>(entity =>
        {
            entity.HasKey(e => e.IdwfSteps).HasName("PRIMARY");

            entity.ToTable("wf_steps");

            entity.HasIndex(e => e.WorkFlowId, "fk_wf_step_wf_id_idx");

            entity.HasIndex(e => e.RoleId, "fk_wf_steps_rol_id_idx");

            entity.HasIndex(e => new { e.WorkFlowId, e.RoleId, e.PermissionId }, "idx_steps_wf_role_per_id");

            entity.HasIndex(e => e.PermissionId, "k_wf_steps_per_id_idx");

            entity.Property(e => e.IdwfSteps).HasColumnName("idwf_steps");
            entity.Property(e => e.IsFinalStep).HasColumnName("is_final_step");
            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.StepName)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("step_name");
            entity.Property(e => e.StepOrder).HasColumnName("step_order");
            entity.Property(e => e.WorkFlowId).HasColumnName("work_flow_id");

            entity.HasOne(d => d.Permission).WithMany(p => p.WfSteps)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("k_wf_steps_per_id");

            entity.HasOne(d => d.Role).WithMany(p => p.WfSteps)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_wf_steps_rol_id");

            entity.HasOne(d => d.WorkFlow).WithMany(p => p.WfSteps)
                .HasForeignKey(d => d.WorkFlowId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_wf_step_wf_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
