using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PatrITech.WeChat.OfficialAccount.Model;
using Refit;

namespace PatrITech.WeChat.OfficialAccount.Api
{
    interface ITemplateMessageClient
    {
        [Post("/cgi-bin/template/api_add_template")]
        Task<HttpResponseMessage> AddTemplate(
            [AliasAs("access_token")][Query] string accessToken,
            [Body(BodySerializationMethod.Serialized)] TemplateIdShortRequest templateIdShort);

        [Post("/cgi-bin/message/template/send")]
        Task<HttpResponseMessage> Send(
            [AliasAs("access_token")][Query] string accessToken,
            [Body(BodySerializationMethod.Serialized)]SendInput input);

        /// <summary>
        /// 获取模板列表
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        /// <seealso cref="https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1433751277"/>
        [Get("/cgi-bin/template/get_all_private_template")]
        Task<HttpResponseMessage> GetAllPrivateTemplate([AliasAs("access_token")][Query] string accessToken);

        /// <summary>
        /// 设置所属行业
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <seealso cref="https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1433751277"/>
        [Post("/cgi-bin/template/api_set_industry")]
        Task<HttpResponseMessage> SetIndustry(
            [AliasAs("access_token")][Query] string accessToken,
            [Body(BodySerializationMethod.Serialized)] SetIndustryRequest input);

        /// <summary>
        /// 获取设置的行业信息
        /// </summary>
        /// <returns></returns>
        /// <seealso cref="https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1433751277"/>
        [Get("/cgi-bin/template/get_industry")]
        Task<HttpResponseMessage> GetIndustry([AliasAs("access_token")][Query] string accessToken);

        [Post("/cgi-bin/template/del_private_template")]
        Task<HttpResponseMessage> DeletePrivateTemplate(
            [AliasAs("access_token")][Query] string accessToken,
            [Body(BodySerializationMethod.Serialized)] TemplateIdRequest templateId);
    }
}
