using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.OfficialAccount.Model
{
    internal class CreateTagRequest
    {
        [JsonProperty(PropertyName = "tag")]
        public TagEntity Tag { get; set; }

        public CreateTagRequest(string tagName)
        {
            Tag = new TagEntity() { Name = tagName };
        }
    }
}
