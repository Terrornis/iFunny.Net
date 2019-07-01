using FunnyNet.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnyNet.Rest
{
    public class ContentStats
    {
        [JsonProperty("comments")]
        public int Comments { get; set; }

        [JsonProperty("republished")]
        public int Republishes { get; set; }

        [JsonProperty("smiles")]
        public int Smiles { get; set; }

        [JsonProperty("unsmiles")]
        public int Unsmiles { get; set; }

        [JsonProperty("Views")]
        public int Views { get; set; }
    }

    public class Content
    {
        [JsonProperty("id")]
        public string Id { get; private set; }

        [JsonProperty("is_featured")]
        public bool IsFeatured { get; private set; }

        public bool IsRepublished { get; private set; }

        [JsonProperty("date_create")]
        private double _dateCreated = 0;
        public DateTime DateCreated
        {
            get
            {
                return DateTime.UnixEpoch.AddSeconds(_dateCreated).ToLocalTime();
            }
        }

        [JsonProperty("issue_at")]
        private double _dateFeatured = 0;
        public DateTime DateFeatured
        {
            get
            {
                return DateTime.UnixEpoch.AddSeconds(_dateFeatured).ToLocalTime();
            }
        }

        [JsonProperty("tags")]
        public List<string> Tags { get; private set; }

        [JsonProperty("num")]
        public ContentStats Stats { get; private set; }

        [JsonProperty("link")]
        public string SiteUrl { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        private Content _source;
        [JsonProperty("source", ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public Content Source
        {
            get { return _source ?? this; }
            private set
            {
                _source = (Content)this.MemberwiseClone();
                IsRepublished = true;
                _source.Creator = value.Creator;
                _source.Id = value.Id;
            }
        }

        private User _creator;
        [JsonProperty("creator")]
        public User Creator
        {
            get { return _creator; }
            private set
            {
                _creator = value;
                if (_creator != null)
                    _creator.Getter = Getter;
            }
        }

        private AuthUser _getter;
        internal AuthUser Getter
        {
            get { return _getter; }
            set
            {
                _getter = value;
                _creator.Getter = value;
            }
        }
    }
}
