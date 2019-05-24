using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatrITech.WeChat.OfficialAccount.Model
{
    internal class BatchGetUsersRequest
    {
        internal class ListItem
        {
            [JsonProperty(PropertyName = "openid")]
            public string OpenId { get; set; }

            [JsonProperty(PropertyName = "lang", NullValueHandling = NullValueHandling.Ignore)]
            public string Lang { get; set; }

            public ListItem(string openId, string lang)
            {
                OpenId = openId;
                Lang = lang;
            }
        }

        [JsonProperty(PropertyName = "user_list")]
        public ListItem[] UserList { get; set; }

        public BatchGetUsersRequest(string[] openIds, string lang)
        {
            UserList = openIds.Select(id => new ListItem(id, lang)).ToArray();
        }
    }
}
