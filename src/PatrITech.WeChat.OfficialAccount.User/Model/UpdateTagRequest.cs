using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.OfficialAccount.Model
{
    public class UpdateTagRequest
    {
        [JsonProperty(PropertyName = "tag")]
        public TagEntity Tag { get; set; }

        public UpdateTagRequest(int id, string name)
        {
            Tag = new TagEntity()
            {
                Id = id,
                Name = name
            };
        }
    }
}
