using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WorkoutTracker.Infrastructure.Identity
{
    public static class AuthConfiguration
    {
        public static void AddAuthConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    IConfigurationSection googleAuthNSection =
                    config.GetSection("Authentication:Google");
                    options.ClientId = googleAuthNSection["ClientId"];
                    options.ClientSecret = googleAuthNSection["ClientSecret"];
                });
        }
    }
}
