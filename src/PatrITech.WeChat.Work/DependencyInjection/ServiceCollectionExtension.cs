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
        public static IWorkModuleBuilder AddWorkModule(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<WorkOptions>(config);
            services.AddTransient<TokenService>();
            services.AddTransient<ITokenService, TokenService>();

            return new WorkModuleBuilder(services, config);
        }
    }
}
