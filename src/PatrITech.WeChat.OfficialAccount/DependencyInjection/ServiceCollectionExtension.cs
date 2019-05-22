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
        public static IOfficialAccountServiceBuilder AddOfficialAccount(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<OfficialAccountOptions>(config);
            services.AddTransient<TokenService>();
            services.AddTransient<ITokenService, TokenService>();

            return new OfficialAccountServiceBuilder(services, config);
        }
    }
}
