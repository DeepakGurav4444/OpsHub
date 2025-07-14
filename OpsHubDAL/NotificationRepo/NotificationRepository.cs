using FirebaseAdmin.Messaging;
using OpsHubCommonUtility.Logger;
using OpsHubDAL.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OpsHubDAL.NotificationRepo
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ILoggerManager _loggerManager;
        public NotificationRepository(ILoggerManager loggerManager) 
        {
            _loggerManager = loggerManager;
        }
        public async Task<bool> SendNotification(string token, string title, string body)
        {
            _loggerManager.LogInfo("Entry NotificationRepository => SendNotification");
            var message = new Message()
            {
                Token = token,
                Notification = new Notification
                {
                    Title = title,
                    Body = body
                },
                Android = new AndroidConfig
                {
                    Priority = Priority.High,
                },
                Apns = new ApnsConfig
                {
                    Aps = new Aps
                    {
                        ContentAvailable = true
                    }
                }
            };

            string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            Console.WriteLine("Successfully sent message: " + response);
            using var doc = JsonDocument.Parse(response);
            bool success = doc.RootElement.GetProperty("success").GetBoolean();
            _loggerManager.LogInfo("Exit NotificationRepository => SendNotification");
            return success;

        }
    }
}
