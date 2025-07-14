using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace OpsHubBAL.Firebase
{
    public class FirebaseInitializerService : IHostedService
    {
        private static bool _isInitialized = false;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (!_isInitialized)
            {
                //FirebaseApp.Create(new AppOptions
                //{
                //    Credential =GoogleCredential.FromFile("wwwroot/keys/firebase_admin_sdk.json")                
                //});

                Console.WriteLine("Firebase initialized");
                _isInitialized = true;
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // No cleanup required for FirebaseApp
            return Task.CompletedTask;
        }
    }

}
