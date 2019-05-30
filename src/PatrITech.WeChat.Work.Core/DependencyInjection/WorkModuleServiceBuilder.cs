using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PatrITech.WeChat.Work.DependencyInjection
{
    public class WorkModuleServiceBuilder : IWorkModuleServiceBuilder
    {
        public WorkModuleServiceBuilder(IServiceCollection services, IConfiguration config)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
            Config = config;
        }

        public IServiceCollection Services { get; }
        public IConfiguration Config { get; }
    }
}
