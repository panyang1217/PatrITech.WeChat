using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using PatrITech.WeChat.Core;

namespace PatrITech.WeChat.Work.Model
{
    public class GetExternalContactResult : IRawResult
    {
        [JsonProperty(PropertyName = "external_contact")]
        public ExternalContact ExternalContact { get; set; }
        [JsonProperty(PropertyName = "follow_user")]
        public FollowUser[] FollowUsers { get; set; }
        public string Content { get; set; }
        public byte[] SHA1 { get; set; }
    }
}
