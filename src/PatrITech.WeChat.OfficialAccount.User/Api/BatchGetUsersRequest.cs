using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.OfficialAccount.User.Api
{
    public class BatchGetUsersRequest
    {
        public class ListItem
        {
            [JsonProperty(PropertyName = "openid")]
            public string OpenId { get; set; }

            [JsonProperty(PropertyName = "lang", NullValueHandling = NullValueHandling.Ignore)]
            public string Lang { get; set; }
        }

        [JsonProperty(PropertyName = "user_list")]
        public ListItem[] UserList { get; set; }
    }
}
