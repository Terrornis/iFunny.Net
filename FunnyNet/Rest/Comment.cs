using FunnyNet.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnyNet.Rest
{
    public class CommentStats
    {
        [JsonProperty("smiles")]
        public int Smiles { get; set; }

        [JsonProperty("unsmiles")]
        public int Unsmiles { get; set; }

        [JsonProperty("replies")]
        public int Replies { get; set; }
    }

    public class Comment : RestObject
    {
        internal AuthUser Fetcher { get; set; }

        [JsonProperty("num")]
        public CommentStats Stats { get; set; }

        [JsonProperty("user")]
        public User Creator { get; private set; }

        [JsonProperty("state")]
        public string State { get; private set; }

        public bool IsTopComment { get { return State == "top"; } }

        [JsonProperty("is_reply")]
        public bool Reply { get; private set; }

        [JsonProperty("is_deleted")]
        public bool Deleted { get; internal set; }

        [JsonProperty("is_edited")]
        public bool Edited { get; internal set; }

        [JsonProperty("cid")]
        public string ContentId { get; private set; }

        [JsonProperty("text")]
        public string Text { get; private set; }
    }
}
