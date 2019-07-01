using FunnyNet.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnyNet.Rest
{
    public class RestObject
    {
        internal AuthUser Getter { get; set; }

        [JsonProperty("id")]
        public string Id { get; internal set; }
    }
}
