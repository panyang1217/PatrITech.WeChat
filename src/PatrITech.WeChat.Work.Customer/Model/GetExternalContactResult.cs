using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.Work.Model
{
    public class GetExternalContactResult
    {
        [JsonProperty(PropertyName = "external_contact")]
        public ExternalContact ExternalContact { get; set; }
        [JsonProperty(PropertyName = "follow_user")]
        public FollowUser[] FollowUsers { get; set; }
    }
}
