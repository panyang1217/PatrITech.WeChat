using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.OfficialAccount.Model
{
    internal class GetUsersWithTagRequest
    {
        [JsonProperty(PropertyName = "tagid")]
        public int TagId { get; set; }

        [JsonProperty(PropertyName = "next_openid")]
        public string NextOpenId { get; set; }

        public GetUsersWithTagRequest(int tagId, string nextOpenId)
        {
            TagId = tagId;
            NextOpenId = nextOpenId;
        }
    }
}
