using Microsoft.EntityFrameworkCore;
using OpsHubCommonUtility.Logger;
using OpsHubDAL.DataModel;
using OpsHubDTOModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDAL.ClientRepo
{
    public class ClientRepository : IClientRepository
    {
        private readonly IOpsHubContext _opsHubContext;
        private readonly ILoggerManager _loggerManager;
        public ClientRepository(IOpsHubContext opsHubContext,ILoggerManager loggerManager) 
        {
            _opsHubContext = opsHubContext;
            _loggerManager = loggerManager;
        }

        public async Task<UserClient> AddClient(UserClient client)
        {
            _loggerManager.LogInfo("Entry ClientRepository => AddClient");
            var dataResult = await _opsHubContext.UserClient.AddAsync(client);
            _loggerManager.LogInfo("Exit ClientRepository => AddClient");
            await _opsHubContext.SaveChangesAsync();
            return dataResult.Entity;
        }

        public async Task<UserClient> GetClientById(int clientId)
        {
            _loggerManager.LogInfo("Entry ClientRepository => GetClientById");
            var dataResult  = await _opsHubContext.UserClient.FirstOrDefaultAsync(x=>x.IduserClient == clientId);
            _loggerManager.LogInfo("Exit ClientRepository => GetClientById");
            return dataResult;
        }

        public async Task<List<GetClientInfoResponse_DTO>> GetClientByUserId(int userId, int isActive)
        {
            _loggerManager.LogInfo("Entry ClientRepository => GetClientByUserId");
            var dataResult = await (from c in _opsHubContext.UserClient
                       join u in _opsHubContext.TenUsers on c.UserId equals u.IdtenUsers
                       join m in _opsHubContext.ModModules on c.ModuleId equals m.IdmodModules
                       where c.UserId == userId && c.IsActive == isActive
                       select new GetClientInfoResponse_DTO()
                       {
                           ClientId = c.IduserClient,
                           ClientName = c.ClientName,
                           CreatedAt = c.CreatedAt,
                           ModuleId = c.ModuleId,
                           ModuleName = m.ModuleName,
                           UserId = c.UserId,
                           UserName = u.TenUserName
                       }
                       ).ToListAsync();                
            _loggerManager.LogInfo("Exit ClientRepository => GetClientByUserId");
            return dataResult;
        }

        public async Task<GetClientInfoResponse_DTO> GetClientInfo(int clientId)
        {
            _loggerManager.LogInfo("Entry ClientRepository => GetClientInfo");
            var dataResult = await (from c in _opsHubContext.UserClient
                                    join u in _opsHubContext.TenUsers on c.UserId equals u.IdtenUsers
                                    join m in _opsHubContext.ModModules on c.ModuleId equals m.IdmodModules
                                    where c.IduserClient == clientId
                                    select new GetClientInfoResponse_DTO()
                                    {
                                        ClientId = c.IduserClient,
                                        ClientName = c.ClientName,
                                        CreatedAt = c.CreatedAt,
                                        ModuleId = c.ModuleId,
                                        ModuleName = m.ModuleName,
                                        UserId = c.UserId,
                                        UserName = u.TenUserName
                                    }
                       ).FirstOrDefaultAsync();
            _loggerManager.LogInfo("Exit ClientRepository => GetClientInfo");
            return dataResult;
        }

        public async Task<UserClient> UpdateClient(UserClient client)
        {
            _loggerManager.LogInfo("Entry ClientRepository => UpdateClient");
            var dataResult = _opsHubContext.UserClient.Update(client);
            _loggerManager.LogInfo("Exit ClientRepository => UpdateClient");
            await _opsHubContext.SaveChangesAsync();
            return dataResult.Entity;
        }
    }
}
