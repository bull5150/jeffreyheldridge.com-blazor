using System.Text.Json.Serialization;

namespace CommonCore.Models.BlueSky
{
    public class BlueSkyFeedResponse
    {
        public List<BlueSkyFeedItem> Feed { get; set; }
        public string Cursor { get; set; }
    }
    public class BlueSkyFeedItem
    {
        public BlueSkyPost Post { get; set; }
    }
    public class BlueSkyPost
    {
        public string uri { get; set; }
        public string cid { get; set; }
        public Author author { get; set; }
        public Record record { get; set; }
        public Embed1 embed { get; set; }
        public int replyCount { get; set; }
        public int repostCount { get; set; }
        public int likeCount { get; set; }
        public int quoteCount { get; set; }
        public DateTime indexedAt { get; set; }
        public Viewer1 viewer { get; set; }
        public object[] labels { get; set; }
    }

    public class Author
    {
        public string did { get; set; }
        public string handle { get; set; }
        public string displayName { get; set; }
        public string avatar { get; set; }
        public Viewer viewer { get; set; }
        public object[] labels { get; set; }
        public DateTime createdAt { get; set; }
    }

    public class Viewer
    {
        public bool muted { get; set; }
        public bool blockedBy { get; set; }
    }

    public class Record
    {
        public string type { get; set; }
        public DateTime createdAt { get; set; }
        public Embed embed { get; set; }
        public Facet[] facets { get; set; }
        public string[] langs { get; set; }
        public string text { get; set; }
    }

    public class Embed
    {
        public string type { get; set; }
        public Image[] images { get; set; }
    }

    public class Image
    {
        public string alt { get; set; }
        public Aspectratio aspectRatio { get; set; }
        public Image1 image { get; set; }
    }

    public class Aspectratio
    {
        public int height { get; set; }
        public int width { get; set; }
    }

    public class Image1
    {
        public string type { get; set; }
        public Ref _ref { get; set; }
        public string mimeType { get; set; }
        public int size { get; set; }
    }

    public class Ref
    {
        public string link { get; set; }
    }

    public class Facet
    {
        public Feature[] features { get; set; }
        public Index index { get; set; }
    }

    public class Index
    {
        public int byteEnd { get; set; }
        public int byteStart { get; set; }
    }

    public class Feature
    {
        public string type { get; set; }
        public string tag { get; set; }
    }

    public class Embed1
    {
        public string type { get; set; }
        public Image2[] images { get; set; }
    }

    public class Image2
    {
        public string thumb { get; set; }
        public string fullsize { get; set; }
        public string alt { get; set; }
        public Aspectratio1 aspectRatio { get; set; }
    }

    public class Aspectratio1
    {
        public int height { get; set; }
        public int width { get; set; }
    }

    public class Viewer1
    {
        public bool threadMuted { get; set; }
        public bool embeddingDisabled { get; set; }
    }
}
