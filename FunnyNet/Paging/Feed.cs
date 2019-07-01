using FunnyNet.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnyNet.Paging
{
    public class PagingCursors
    {
        [JsonProperty("next")]
        public string Next { get; set; }

        [JsonProperty("prev")]
        public string Previous { get; set; }
    }

    public class FeedPaging
    {
        [JsonProperty("cursors")]
        public PagingCursors Cursors { get; private set; }

        [JsonProperty("hasNext")]
        public bool HasNext { get; private set; }

        [JsonProperty("hasPrev")]
        public bool HasPrevious { get; private set; }
    }

    public class Feed<T>
    {
        internal AuthUser Getter { get; set; }
        internal string Url { get; set; }

        [JsonProperty("items")]
        public List<T> Items { get; private set; }

        [JsonProperty("paging")]
        private FeedPaging Paging { get; set; }

        public bool HasNext => Paging.HasNext;
        public bool HasPrevious => Paging.HasPrevious;

        public async Task<Feed<T>> GetNextAsync(int limit = 30)
            => await Funny.GetFeedAsync<T>(Url, limit, Paging.Cursors.Next, Getter);

        public async Task<List<T>> GetAllItemsAsync()
        {
            List<T> items = new List<T>();
            items.AddRange(Items);

            Feed<T> feed = this;
            do
            {
                feed = await feed.GetNextAsync(100);
                items.AddRange(feed.Items);
            } while (feed.HasNext);

            return items;
        }

        public async IAsyncEnumerable<T> EnumerateAsync()
        {
            Feed<T> feed = this;
            do
            {
                for (int i = 0; i < feed.Items.Count; i++)
                    yield return feed.Items[i];

                feed = await feed.GetNextAsync();
            }
            while (feed.HasNext);
        }
    }
}
