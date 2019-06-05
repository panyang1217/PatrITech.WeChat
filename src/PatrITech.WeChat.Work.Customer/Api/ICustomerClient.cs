using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace PatrITech.WeChat.Work.Api
{
    interface ICustomerClient
    {
        /// <summary>
        /// 获取配置了客户联系功能的成员列表
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        /// <seealso cref="https://work.weixin.qq.com/api/doc#90000/90135/91554"/>
        [Get("/cgi-bin/externalcontact/get_follow_user_list")]
        Task<HttpResponseMessage> ListFollowUser([AliasAs("access_token")][Query] string accessToken);

        /// <summary>
        /// 获取外部联系人列表
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <seealso cref="https://work.weixin.qq.com/api/doc#90000/90135/91555"/>
        [Get("/cgi-bin/externalcontact/list")]
        Task<HttpResponseMessage> ListExternalContact(
            [AliasAs("access_token")][Query] string accessToken,
            [AliasAs("userid")][Query] string userId);

        /// <summary>
        /// 获取外部联系人详情
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="externalUserId"></param>
        /// <returns></returns>
        /// <seealso cref="https://work.weixin.qq.com/api/doc#90000/90135/91556"/>
        [Get("/cgi-bin/externalcontact/get")]
        Task<HttpResponseMessage> GetExternalContact(
            [AliasAs("access_token")][Query] string accessToken,
            [AliasAs("external_userid")][Query] string externalUserId);
    }
}
