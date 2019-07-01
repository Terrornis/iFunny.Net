using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace FunnyNet.Rest
{
    public class Thumbnail
    {
        [JsonProperty("small_url")]
        public string SmallUrl { get; private set; }

        [JsonProperty("medium_url")]
        public string MediumUrl { get; private set; }

        [JsonProperty("large_url")]
        public string LargeUrl { get; private set; }
    }

    public class Photo
    {
        [JsonProperty("thumb")]
        public Thumbnail Thumbnail { get; private set; }

        [JsonProperty("url")]
        public string Url { get; private set; }

        [JsonProperty("bg_color")]
        private string BgColor { get; set; }

        [JsonIgnore]
        public Color BackgroundColor
        {
            get
            {
                return Color.FromArgb(int.Parse(BgColor, System.Globalization.NumberStyles.HexNumber));
            }
        }
    }
}
