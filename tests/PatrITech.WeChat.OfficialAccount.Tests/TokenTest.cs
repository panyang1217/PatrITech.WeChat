using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using PatrITech.WeChat.OfficialAccount.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace PatrITech.WeChat.OfficialAccount.Tests
{
    public class TokenTest : TestBase
    {
        public TokenService TokenService { get => Provider.GetService<TokenService>(); }

        public TokenTest() { }

        protected override void ConfigureService(IServiceCollection services, IConfiguration config)
        {
            services.AddOfficialAccount(config)
                .WithUserService();
        }

        [Fact]
        public async void GetAccessToken()
        {
            //var tokenService = new TokenService();

            var result1 = await TokenService.GetAccessToken(null);

            result1.ResultState.Successed.ShouldBeTrue();
            result1.ResultState.Cached.ShouldBeFalse();
            result1.AccessToken.Token.ShouldNotBeNull();
            result1.AccessToken.ExpiresIn.TotalSeconds.ShouldBe(7200);

            var result2 = await TokenService.GetAccessToken(null);
            result2.ResultState.Successed.ShouldBeTrue();
            result2.ResultState.Cached.ShouldBeTrue();
            result2.AccessToken.Token.ShouldBe(result1.AccessToken.Token);
            result2.AccessToken.ExpiresIn.ShouldBe(result1.AccessToken.ExpiresIn);
        }
    }
}
