using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.OfficialAccount.Model
{
    public class SendInput
    {
        [JsonProperty(PropertyName = "touser")]
        public string ToUser { get; set; }
        [JsonProperty(PropertyName = "template_id")]
        public string TemplateId { get; set; }
        [JsonProperty(PropertyName = "url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }
        [JsonProperty(PropertyName = "miniprogram", NullValueHandling = NullValueHandling.Ignore)]
        public Miniprogram MiniProgram { get; set; }
        [JsonProperty(PropertyName = "data")]
        public DataCollection Data { get; set; } = new DataCollection();

        public class Miniprogram
        {
            [JsonProperty(PropertyName = "appid")]
            public string AppId { get; set; }
            [JsonProperty(PropertyName = "pagepath")]
            public string PagePath { get; set; }
        }

        public class DataItem
        {
            [JsonProperty(PropertyName = "value")]
            public string Value { get; set; }
            [JsonProperty(PropertyName = "color")]
            public string Color { get; set; }

            public DataItem() { }
            public DataItem(string value, string color)
            {
                Value = value;
                Color = color;
            }
        }

        public class DataCollection : Dictionary<string, DataItem>
        {
            const string Key_First = "first";
            const string Key_Remark = "remark";
            [JsonIgnore]
            public DataItem First
            {
                get => this[Key_First];
                set => this[Key_First] = value;
            }
            [JsonIgnore]
            public DataItem Remark
            {
                get => this[Key_Remark];
                set => this[Key_Remark] = value;
            }

            public DataCollection()
            {
                Add(Key_First, new DataItem());
                Add(Key_Remark, new DataItem());
            }

            //class DataCollectionJsonConverter : JsonConverter
            //{
            //    public override bool CanConvert(Type objectType)
            //    {
            //        return objectType == typeof(DataItem);
            //    }

            //    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            //    {
            //        var dataCollection = (DataCollection)value;
            //        writer.WriteStartObject();
            //        foreach (var item in dataCollection)
            //        {
            //            writer.WriteStartObject();
            //            writer.WritePropertyName(item.Key);
            //            writer.write(item.Value);
            //        }
            //    }
            //}
        }
    }


}
