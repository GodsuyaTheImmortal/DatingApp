using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        // Extension method to add identity services to the IServiceCollection
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            // Add JWT Bearer authentication to the service collection
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => 
            {
                // Configure JWT Bearer token validation parameters
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true, // Validate the signing key of the token
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])), // Signing key to validate against, obtained from configuration
                    ValidateIssuer = false, // Do not validate the issuer of the token
                    ValidateAudience = false // Do not validate the audience of the token
                };
            });

            // Return the service collection to allow for method chaining
            return services;
        }
    }
}