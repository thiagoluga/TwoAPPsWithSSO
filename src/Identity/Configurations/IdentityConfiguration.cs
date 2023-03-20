using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Identity.Services;
using Identity.Context;
using NetDevPack.Identity.Jwt;
using NetDevPack.Identity.User;
using NetDevPack.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Configurations
{
    public static class IdentityConfiguration
    {
        public static void ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationSection section = configuration.GetSection(nameof(AppJwtSettings));
            services.Configure<AppJwtSettings>(section);
            AppJwtSettings appSettings = section.Get<AppJwtSettings>();

            services.AddDbContext<MyIdentityDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("SQLiteConnection")
            ));

            services
                .AddCustomIdentity<IdentityUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false; //set to true when in prod
                    options.User.RequireUniqueEmail = true;
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 1;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = true;
                })
                .AddDefaultRoles()
                .AddCustomEntityFrameworkStores<MyIdentityDbContext>()
                .AddDefaultTokenProviders();

            //This key will be used in other apps so they can Single Sign On(SSO)
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
            services.AddAspNetUserConfiguration();
            services.AddScoped<IAccountService, AccountService>();
        }

        public static void ConfigureInitialDatabase(this IApplicationBuilder app)
        {
            MyIdentityDbContext dbcontext = app.ApplicationServices.GetRequiredService<MyIdentityDbContext>();
            dbcontext.Database.EnsureCreated();
        }
    }
}
