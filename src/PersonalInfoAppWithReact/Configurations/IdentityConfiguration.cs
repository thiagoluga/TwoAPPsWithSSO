using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NetDevPack.Identity.Jwt;

namespace AppReact.Configurations
{
    public static class IdentityConfiguration
    {
        public static void ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationSection section = configuration.GetSection(nameof(AppJwtSettings));
            services.Configure<AppJwtSettings>(section);
            AppJwtSettings appSettings = section.Get<AppJwtSettings>();

            //This is the same key used in Identity API
            byte[] key = Encoding.ASCII.GetBytes(appSettings.SecretKey);
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(delegate (JwtBearerOptions options)
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = appSettings.Audience,
                        ValidIssuer = appSettings.Issuer
                    };
                });
            //services.AddJwtConfiguration(configuration);

            services.AddAuthorization();
        }
    }
}
