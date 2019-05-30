using Newtonsoft.Json;
using PatrITech.WeChat.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.Work.Model
{
    public class ResultState
    {
        public static ResultState OK { get => new ResultState() { ErrorCode = 0, ErrorMessage = "" }; }
        public static ResultState OkFromCache { get => new ResultState() { Cached = true }; }

        [JsonProperty(PropertyName = "errcode")]
        public int ErrorCode { get; set; }
        [JsonProperty(PropertyName = "errmsg")]
        public string ErrorMessage { get; set; }
        [JsonIgnore]
        public bool Successed { get => ErrorCode == 0; }
        public bool Cached { get; private set; }
    }
}
