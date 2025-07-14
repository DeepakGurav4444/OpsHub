using OpsHubCommonUtility.Response;
using OpsHubDTOModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubBAL.Client
{
    public interface IClientService
    {
        Task<ResultWithDataDTO<int>> RegisterClient(AddClientRequest_DTO request_DTO);
        Task<ResultWithDataDTO<GetClientInfoResponse_DTO>> GetClientInfo(int ClientId);
        Task<(bool, int, string)> ValidateStep(int ClientId);
        Task<ResultWithDataDTO<List<GetClientInfoResponse_DTO>>> GetClientsByUserId(int userId,int isActive);
        Task<ResultWithDataDTO<int>> ApproveClient(ApproveClientRequest_DTO request_DTO);
        Task<ResultWithDataDTO<int>> SendNotificationToClient(SendClientNotificationRequest_DTO request_DTO);

    }
}
