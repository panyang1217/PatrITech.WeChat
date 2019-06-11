using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PatrITech.WeChat.Work.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.Work.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IWorkModuleServiceBuilder AddWorkModule(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<WorkModuleOptions>(config);
            services.AddTransient<TokenService>();
            services.AddTransient<ITokenService, TokenService>();

            return new WorkModuleServiceBuilder(services, config);
        }

        public static IWorkModuleServiceBuilder AddWorkModule(this IServiceCollection services, Action<WorkModuleOptions> configOptions)
        {
            services.Configure(configOptions);
            services.AddTransient<TokenService>();
            services.AddTransient<ITokenService, TokenService>();

            return new WorkModuleServiceBuilder(services, null);
        }
    }
}
