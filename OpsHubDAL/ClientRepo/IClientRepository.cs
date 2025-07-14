using OpsHubDAL.DataModel;
using OpsHubDTOModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDAL.ClientRepo
{
    public interface IClientRepository
    {
        Task<UserClient> AddClient(UserClient client);
        Task<UserClient> GetClientById(int clientId);
        Task<List<GetClientInfoResponse_DTO>> GetClientByUserId(int userId,int isActive);
        Task<GetClientInfoResponse_DTO> GetClientInfo(int clientId);
        Task<UserClient> UpdateClient(UserClient client);
    }
}
