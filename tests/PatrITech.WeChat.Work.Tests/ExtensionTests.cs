using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PatrITech.WeChat.Test;
using PatrITech.WeChat.Work.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PatrITech.WeChat.Work.Tests
{
    public class ExtensionTests : TestBase
    {
        protected override void ConfigureService(IServiceCollection services, IConfiguration config)
        {
            services.AddWorkModule(opt =>
            {
                opt.DefaultAccountName = "Default";
                opt.AddAccount("Default", clientOpt => {
                    clientOpt.CorpId = config["accounts:default:corpId"];
                    clientOpt.CustomerServiceSecret = config["accounts:default:customerServiceSecret"];
                });
            });
        }

        [Fact()]
        public async void AddOfficialAccount_With_ConfigureOptions_Test()
        {
            var tokenService = Provider.GetService<ITokenService>();
            var corpId = tokenService.GetCorpId("Default");
            corpId.ShouldNotBeNullOrEmpty();
            corpId.ShouldBe("ww7f8374ebb6024d2f");

            var token = await tokenService.GetAccessToken(AccessTokenType.Customer, null);
            token.Token.ShouldNotBeNullOrEmpty();
        }
    }
}
