using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PatrITech.WeChat.OfficialAccount.DependencyInjection;
using PatrITech.WeChat.Test;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PatrITech.WeChat.OfficialAccount.Tests
{
    public class ExtensionsTests : TestBase
    {
        protected override void ConfigureService(IServiceCollection services, IConfiguration config)
        {
            services.AddOfficialAccountModule(opt =>
            {
                opt.DefaultAccountName = "Default";
                opt.AddAccount("Default","wx0977d07647b7c055", "5dfc0a101eb53d112979cf3f37cb3d91");
            });
        }

        [Fact()]
        public void AddOfficialAccount_With_ConfigureOptions_Test()
        {
            var tokenService = Provider.GetService<ITokenService>();
            var appId = tokenService.GetAppId("Default");
            appId.ShouldNotBeNullOrEmpty();
            appId.ShouldBe("wx0977d07647b7c055");
        }
    }
}
