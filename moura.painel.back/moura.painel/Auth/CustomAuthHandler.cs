using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using moura.painel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace moura.painel.Auth
{
    public class CustomAuthHandler : AuthenticationHandler<CustomAuthOptions>
    {
        
        private LoginTokenService tokenService;

        public CustomAuthHandler(IOptionsMonitor<CustomAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, LoginTokenService tokenService) : base(options, logger, encoder, clock)
        {
            this.tokenService = tokenService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!this.Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.NoResult();

            string bearer = this.Request.Headers["Authorization"];

            if (bearer == null  || !bearer.StartsWith("Bearer"))
                return AuthenticateResult.NoResult();

            bearer = bearer.Remove(0, 6).Trim();

            return this.tokenService.GetAutenticacao(bearer);
        }
    }
}
