using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PatrITech.WeChat.OfficialAccount.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.OfficialAccount.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IOfficialAccountServiceBuilder AddOfficialAccountModule(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<OfficialAccountModuleOptions>(config);
            services.AddTransient<TokenService>();
            services.AddTransient<ITokenService, TokenService>();

            return new OfficialAccountServiceBuilder(services, config);
        }

        public static IOfficialAccountServiceBuilder AddOfficialAccountModule(this IServiceCollection services, Action<OfficialAccountModuleOptions> configureOptions)
        {
            services.Configure(configureOptions);
            services.AddTransient<TokenService>();
            services.AddTransient<ITokenService, TokenService>();

            return new OfficialAccountServiceBuilder(services, null);
        }
    }
}
