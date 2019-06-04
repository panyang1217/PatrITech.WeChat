using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PatrITech.WeChat.OfficialAccount.Api;
using PatrITech.WeChat.OfficialAccount.Configuration;
using PatrITech.WeChat.OfficialAccount.Extensions;
using PatrITech.WeChat.OfficialAccount.Model;
using Refit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PatrITech.WeChat.OfficialAccount
{
    public class TemplateMessageService : SecuredServiceBase
    {
        private readonly ITemplateMessageClient _templateServiceClient;

        public TemplateMessageService(IOptionsMonitor<OfficialAccountModuleOptions> optionsAccessor, ITokenService tokenService) : base(optionsAccessor, tokenService)
        {
            _templateServiceClient = RestService.For<ITemplateMessageClient>(OfficialAccountConsts.ServiceUrl);
        }

        protected virtual async Task<(string TemplateId, ResultState ResultState)> DoAddTemplate(IToken token, string templateIdShort)
        {
            var resp = await _templateServiceClient.AddTemplate(token.Token, new TemplateIdShortRequest(templateIdShort));

            return await resp.ReadAsResult<string>("$.template_id");
        }
        public Task<(string TemplateId, ResultState ResultState)> AddTemplate(string templateIdShort)
            => Invoke(token => DoAddTemplate(token, templateIdShort));
        public Task<(string TemplateId, ResultState ResultState)> AddTemplate(string templateIdShort, string accountName)
            => Invoke(token => DoAddTemplate(token, templateIdShort), accountName);
        public Task<(string TemplateId, ResultState ResultState)> AddTemplate(string templateIdShort, string appId, string secret)
            => Invoke(token => DoAddTemplate(token, templateIdShort), appId, secret);

        protected virtual async Task<(Template[] Templates, ResultState ResultState)> DoGetAllPrivateTemplate(IToken token)
        {
            var resp = await _templateServiceClient.GetAllPrivateTemplate(token.Token);

            return await resp.ReadAsResults<Template>("$.template_list[*]");
        }
        public Task<(Template[] Templates, ResultState ResultState)> GetAllPrivateTemplate()
            => Invoke(token => DoGetAllPrivateTemplate(token));
        public Task<(Template[] Templates, ResultState ResultState)> GetAllPrivateTemplate(string accountName)
            => Invoke(token => DoGetAllPrivateTemplate(token), accountName);
        public Task<(Template[] Templates, ResultState ResultState)> GetAllPrivateTemplate(string appId, string secret)
            => Invoke(token => DoGetAllPrivateTemplate(token), appId, secret);

        protected virtual async Task<ResultState> DoSetIndustry(IToken token, int primaryIndustryId, int deputyIndustryId)
        {
            var resp = await _templateServiceClient.SetIndustry(token.Token,
                new SetIndustryRequest()
                {
                    PrimaryIndustry = primaryIndustryId,
                    DeputyIndustry = deputyIndustryId
                });

            return await resp.ReadAsResult();
        }
        public Task<ResultState> SetIndustry(int primaryIndustryId, int deputyIndustryId)
            => Invoke(token => DoSetIndustry(token, primaryIndustryId, deputyIndustryId));
        public Task<ResultState> SetIndustry(int primaryIndustryId, int deputyIndustryId, string accountName)
            => Invoke(token => DoSetIndustry(token, primaryIndustryId, deputyIndustryId), accountName);
        public Task<ResultState> SetIndustry(int primaryIndustryId, int deputyIndustryId, string appId, string secret)
            => Invoke(token => DoSetIndustry(token, primaryIndustryId, deputyIndustryId), appId, secret);

        protected virtual async Task<(IndustryInfo IndustryInfo, ResultState ResultState)> DoGetIndustry(IToken token)
        {
            var resp = await _templateServiceClient.GetIndustry(token.Token);

            return await resp.ReadAsResult<IndustryInfo>();
        }
        public Task<(IndustryInfo IndustryInfo, ResultState ResultState)> GetIndustry()
            => Invoke(token => DoGetIndustry(token));
        public Task<(IndustryInfo IndustryInfo, ResultState ResultState)> GetIndustry(string accountName)
            => Invoke(token => DoGetIndustry(token), accountName);
        public Task<(IndustryInfo IndustryInfo, ResultState ResultState)> GetIndustry(string appId, string secret)
            => Invoke(token => DoGetIndustry(token), appId, secret);

        protected virtual async Task<(long MessageId, ResultState ResultState)> DoSend(IToken token, SendInput input)
        {
            var resp = await _templateServiceClient.Send(token.Token, input);

            var jobj = JObject.Load(new JsonTextReader(new StreamReader(await resp.Content.ReadAsStreamAsync())));

            var jtoken = jobj.SelectToken("msgid");
            if (jtoken != null)
                return (jtoken.Value<long>(), jobj.ToObject<ResultState>());
            else
                return (0, jobj.ToObject<ResultState>());
        }
        public Task<(long MessageId, ResultState ResultState)> Send(SendInput input)
            => Invoke(token => DoSend(token, input));
        public Task<(long MessageId, ResultState ResultState)> Send(SendInput input, string accountName)
           => Invoke(token => DoSend(token, input), accountName);
        public Task<(long MessageId, ResultState ResultState)> Send(SendInput input, string appId, string secret)
           => Invoke(token => DoSend(token, input), appId, secret);

        protected virtual async Task<ResultState> DoDeletePrivateTemplate(IToken token, string templateId)
        {
            var resp = await _templateServiceClient.DeletePrivateTemplate(token.Token, new TemplateIdRequest()
            {
                Value = templateId
            });

            return await resp.ReadAsResult();
        }

        public Task<ResultState> DeletePrivateTemplate(string templateId)
            => Invoke(token => DoDeletePrivateTemplate(token, templateId));
        public Task<ResultState> DeletePrivateTemplate(string templateId, string accountName)
            => Invoke(token => DoDeletePrivateTemplate(token, templateId), accountName);
        public Task<ResultState> DeletePrivateTemplate(string templateId, string appId, string secret)
            => Invoke(token => DoDeletePrivateTemplate(token, templateId), appId, secret);
    }
}
