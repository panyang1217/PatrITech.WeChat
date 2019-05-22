using Microsoft.Extensions.DependencyInjection;

namespace PatrITech.WeChat.OfficialAccount.DependencyInjection
{
    public static class AccountServiceBuilderExtension
    {
        public static IOfficialAccountServiceBuilder WithUserService(this IOfficialAccountServiceBuilder builder)
        {
            builder.Services.AddTransient<UserService>();

            return builder;
        }
    }
}
