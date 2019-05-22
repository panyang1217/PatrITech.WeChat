using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.OfficialAccount.Model
{

    public class UsersResult
    {
        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }

        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "data")]
        public DataItem Data { get; set; }

        [JsonProperty(PropertyName = "next_openid")]
        public string NextOpenId { get; set; }

        public class DataItem
        {
            [JsonProperty(PropertyName = "openid")]
            public string[] OpenId { get; set; }
        }
    }

    

}
