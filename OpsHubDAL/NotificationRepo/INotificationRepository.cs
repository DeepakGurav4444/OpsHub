using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDAL.NotificationRepo
{
    public interface INotificationRepository
    {
        Task<bool> SendNotification(String token, string title, string body);
    }
}
