using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using LeaveWizard.WeatherForecast.Api.Specs.Context;

namespace LeaveWizard.WeatherForecast.Api.Specs.Core
{
    public class TestAuthenticationHandler : AuthenticationHandler<TestAuthenticationOptions>
    {
        private readonly UserContext _userContext;

        public TestAuthenticationHandler(
            IOptionsMonitor<TestAuthenticationOptions> options,    
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            UserContext userContext) : base(options, logger, encoder, clock)
        {
            _userContext = userContext;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            TestAuthenticationHandler authenticationHandler = this;

            if (string.IsNullOrWhiteSpace(_userContext.Email) )
            {
                return AuthenticateResult.NoResult();
            }
            
            return await Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(new ClaimsIdentity(new Claim[1]
            {
                new (ClaimTypes.Name, _userContext.Email)
            }, authenticationHandler.Scheme.Name)), authenticationHandler.Scheme.Name)));
        }
    }
}