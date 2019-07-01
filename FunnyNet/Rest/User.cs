using FunnyNet.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace FunnyNet.Rest
{
    public class User : IUser
    {
        internal AuthUser Getter { get; set; }

        [JsonProperty("id")]
        public string Id { get; private set; }

        [JsonProperty("nick")]
        public string Name { get; private set; }

        [JsonProperty("cover_url")]
        public string CoverUrl { get; private set; }

        [JsonProperty("photo")]
        public Photo Photo { get; private set; }

        [JsonProperty("is_banned")]
        public bool Banned { get; private set; }

        [JsonProperty("is_verified")]
        public bool Verified { get; private set; }

        [JsonProperty("is_deleted")]
        public bool Deleted { get; private set; }
    }
}
