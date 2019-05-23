using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.OfficialAccount.Model
{
    internal class DeleteTagRequest
    {
        [JsonProperty(PropertyName = "tag")]
        public TagEntity Tag { get; set; }

        public DeleteTagRequest(int id)
        {
            Tag = new TagEntity() { Id = id };
        }
    }
}
