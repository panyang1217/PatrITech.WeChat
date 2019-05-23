using PatrITech.WeChat.OfficialAccount.Model;
using Shouldly;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using PatrITech.WeChat.OfficialAccount.DependencyInjection;

namespace PatrITech.WeChat.OfficialAccount.Tests
{
    public class TemplateTest : TestBase
    {
        public TemplateTest()
        {
        }

        protected override void ConfigureService(IServiceCollection services, IConfiguration config)
        {
            services.AddOfficialAccount(config)
                .WithMessageService();
        }
        protected TemplateMessageService TemplateService { get => Provider.GetService<TemplateMessageService>(); }

        [Fact]
        public async void AddTemplate()
        {
            var result = await TemplateService.AddTemplate("TM00161");

            result.ResultState.Successed.ShouldBeTrue();
            result.TemplateId.ShouldNotBeNullOrEmpty();
        }

        [Fact]
        public async void GetAllPrivateTemplate()
        {
            var result = await TemplateService.GetAllPrivateTemplate();

            result.ResultState.Successed.ShouldBeTrue();
            result.Templates.ShouldNotBeEmpty();

            var template = result.Templates[0];
            template.Content.ShouldNotBeNullOrEmpty();
            template.Title.ShouldNotBeNullOrEmpty();
        }

        [Fact]
        public async void SetIndustry()
        {
            var result = await TemplateService.SetIndustry(16, 38);


            (result.Successed || result.ErrorCode == 43100).ShouldBeTrue();
        }

        [Fact]
        public async void GetIndustry()
        {
            var result = await TemplateService.GetIndustry();

            result.ResultState.Successed.ShouldBeTrue();

            result.IndustryInfo.PrimaryIndstry.ShouldNotBeNull();
            result.IndustryInfo.DeputyIndustry.ShouldNotBeNull();
        }

        [Fact]
        public async void Send()
        {
            var input = new SendInput()
            {
                ToUser = "oYUL-54BgAKpjk_bmtwSeFtKs_Sc",
                TemplateId = "yFOwW8WaBXObHaowdbpVSL6MzMxpKpIP774U1wpeGv0"
            };
            input.Data.First = new SendInput.DataItem("First", "#173177");
            input.Data.Remark = new SendInput.DataItem("Remark", "#173177");
            input.Data.Add("accountType", new SendInput.DataItem("AccountType", "#173177"));
            input.Data.Add("account", new SendInput.DataItem("123456", "#173177"));
            input.Data.Add("amount", new SendInput.DataItem("1.3", "#173177"));
            input.Data.Add("result", new SendInput.DataItem("OK", "#173177"));

            var result = await TemplateService.Send(input);

            result.ResultState.Successed.ShouldBeTrue();
            result.MessageId.ShouldNotBe(0);
        }
    }
}
