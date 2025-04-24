using Loovi.Test.Common.Auth.Interfaces;
using Loovi.Test.Common.Auth.Jwt;
using Loovi.Test.Common.Auth.Services;
using Loovi.Test.Common.Auth;

namespace Loovi.Test.WebApi.Configuration
{
    public static class AuthProviderDependencyInjection
    {
        public static IServiceCollection AddAuthenticationProvider(this IServiceCollection services, IConfiguration configuration)
        {
            var provider = configuration["Authentication:Provider"];

            switch (provider)
            {
                case AuthenticationProvider.Fake:
                    services.AddScoped<IAuthenticationService, UserFakeService>();
                    break;

                case AuthenticationProvider.AzureAdB2C:
                    throw new NotImplementedException("AzureAdB2C provider is not implemented yet.");

                default:
                    throw new InvalidOperationException($"Unknown authentication provider: {provider}");
            }

            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            return services;
        }
    }
}
