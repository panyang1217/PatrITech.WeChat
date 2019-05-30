using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PatrITech.WeChat.Test;
using PatrITech.WeChat.Work.DependencyInjection;
using System;
using Xunit;
using Shouldly;

namespace PatrITech.WeChat.Work.Tests
{
    public class TokenTests : TestBase
    {
        public TokenService TokenService { get => Provider.GetService<TokenService>(); }

        protected override void ConfigureService(IServiceCollection services, IConfiguration config)
        {
            services.AddDistributedMemoryCache();
            services.AddWorkModule(config);
        }

        [Fact]
        public async void GetAccessToken_Test()
        {
            const string corpId = "ww7f8374ebb6024d2f";
            const string secret = "XfUlvv-lYjWJoY2RT9B_FVPBSogiOV675Z14ZmnvXy0";
            (var token, var resultState) = await TokenService.GetAccessToken(corpId, secret);

            resultState.Successed.ShouldBeTrue();
            token.ExpiresIn.TotalSeconds.ShouldBe(7200);
            token.Token.ShouldNotBeNullOrEmpty();

            (token, resultState) = await TokenService.GetAccessToken(corpId, secret);
            resultState.Cached.ShouldBeTrue();
            resultState.Successed.ShouldBeTrue();
            token.ExpiresIn.TotalSeconds.ShouldBe(7200);
            token.Token.ShouldNotBeNullOrEmpty();
        }

        [Fact]
        public async void GetAccessToken_WithAccountName_Test()
        {
            var token = await (TokenService as ITokenService).GetAccessToken(AccessTokenType.Customer, null);

            token.ExpiresIn.TotalSeconds.ShouldBe(7200);
            token.Token.ShouldNotBeNullOrEmpty();

            token = await (TokenService as ITokenService).GetAccessToken("Test", AccessTokenType.Customer, null);

            token.ExpiresIn.TotalSeconds.ShouldBe(7200);
            token.Token.ShouldNotBeNullOrEmpty();

            var ex = await Assert.ThrowsAsync<Exception>(async ()=> await (TokenService as ITokenService).GetAccessToken("Invaid", AccessTokenType.Customer, null));
            ex.Message.ShouldContain("Invalid AccountName");

            ex = await Assert.ThrowsAsync<Exception>(async () => await (TokenService as ITokenService).GetAccessToken(AccessTokenType.CustomizeApp, "test"));
            ex.Message.ShouldContain("Invalid AgentId");

            ex = await Assert.ThrowsAsync<Exception>(async () => await (TokenService as ITokenService).GetAccessToken(AccessTokenType.CustomizeApp, null));
            ex.Message.ShouldContain("AgentId can not be null");
        }
    }
}
