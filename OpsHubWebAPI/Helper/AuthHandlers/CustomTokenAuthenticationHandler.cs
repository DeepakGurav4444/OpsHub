using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpsHubCommonUtility.Logger;
using OpsHubDAL.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace OpsHubDAL.Authentication
{
    public class CustomTokenAuthenticationHandler : AuthenticationHandler<CustomTokenAuthenticationOptions>
    {
        private readonly ILoggerManager _loggerManager;
        private readonly IConfiguration _configuration;
        public CustomTokenAuthenticationHandler(
            IOptionsMonitor<CustomTokenAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IConfiguration configuration, ILoggerManager loggerManager)
            : base(options, logger, encoder, clock)
        {
            _loggerManager = loggerManager;
            _configuration = configuration;
        }


        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                if (!Request.Headers.TryGetValue("Authorization", out var authHeader))
                {
                    return AuthenticateResult.Fail("Authorization header not found.");
                }

                var token = authHeader.ToString().Replace("Bearer ", "").Trim();
                var expectedToken = _configuration["CustomToken"];

                if (token != expectedToken)
                {
                    return AuthenticateResult.Fail("Invalid token.");
                }

                var identity = new ClaimsIdentity(Scheme.Name);
                identity.AddClaim(new Claim(ClaimTypes.Name, "InternalService"));
                identity.AddClaim(new Claim("Schema", "Tenant"));

                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"CustomTokenAuthenticationHandler => {ex.Message}");
                return AuthenticateResult.Fail("An unexpected error occurred.");
            }
        }

    }
}