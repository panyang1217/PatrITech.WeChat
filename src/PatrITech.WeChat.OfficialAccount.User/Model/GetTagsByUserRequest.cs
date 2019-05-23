using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.OfficialAccount.Model
{
    internal class GetTagsByUserRequest
    {
        [JsonProperty(PropertyName = "openid")]
        public string OpenId { get; set; }

        public GetTagsByUserRequest(string openId)
        {
            OpenId = openId;
        }
    }
}
