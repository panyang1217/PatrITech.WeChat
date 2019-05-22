using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PatrITech.WeChat.OfficialAccount.DependencyInjection
{
    public class OfficialAccountServiceBuilder : IOfficialAccountServiceBuilder
    {
        public OfficialAccountServiceBuilder(IServiceCollection services, IConfiguration config)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
            Config = config;
        }

        public IServiceCollection Services { get; }
        public IConfiguration Config { get; }
    }
}
