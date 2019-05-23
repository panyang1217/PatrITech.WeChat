using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.OfficialAccount.Model
{

    public class UsersResult
    {
        public class DataPayload
        {
            [JsonProperty(PropertyName = "openid")]
            public string[] OpenId { get; set; }
        }

        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }

        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "data")]
        public DataPayload Data { get; set; }

        [JsonProperty(PropertyName = "next_openid")]
        public string NextOpenId { get; set; }
    }
}
