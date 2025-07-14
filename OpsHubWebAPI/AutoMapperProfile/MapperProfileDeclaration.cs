using AutoMapper;
using OpsHubDAL.DataModel;
using OpsHubDTOModel.Client;
using OpsHubDTOModel.Tenant;
using OpsHubDTOModel.TenantUser;

namespace OpsHubWebAPI.AutoMapperProfile
{
    public class MapperProfileDeclaration : Profile
    {
        public MapperProfileDeclaration()
        {
            #region Tenant
            CreateMap<AddTenantsRequest_DTO,TenTenants>();
            #endregion

            #region TenantUser
            CreateMap<AddTenantSuperAdminRequest_DTO, TenUsers>();
            CreateMap<AddTenantUserRequest_DTO, TenUsers>();
            #endregion

            #region Client
            CreateMap<AddClientRequest_DTO,UserClient>();
            #endregion
        }
    }
}
