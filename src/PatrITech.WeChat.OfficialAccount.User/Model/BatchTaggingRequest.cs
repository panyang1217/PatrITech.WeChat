using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.OfficialAccount.Model
{
    internal class BatchTaggingRequest
    {

        [JsonProperty(PropertyName = "openid_list")]
        public string[] OpenIds { get; set; }
        [JsonProperty(PropertyName = "tagid")]
        public int TagId { get; set; }

        public BatchTaggingRequest(int tagId, string[] openIds)
        {
            TagId = tagId;
            OpenIds = openIds;
        }
    }
}
