using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PatrITech.WeChat.OfficialAccount.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PatrITech.WeChat.OfficialAccount.Tests
{
    public abstract class TestBase
    {
        protected readonly IServiceProvider Provider;
        //protected readonly iconfi
        public TestBase()
        {
            var config = BuildConfiguration();

            IServiceCollection services = new ServiceCollection();

            services.AddDistributedMemoryCache();
            ConfigureService(services, config);

            Provider = services.BuildServiceProvider();
        }

        protected IConfiguration BuildConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("settings.json")
                .Build();
        }

        protected abstract void ConfigureService(IServiceCollection services, IConfiguration config);
    }
}
