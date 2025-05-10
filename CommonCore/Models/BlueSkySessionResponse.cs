using System.Text.Json.Serialization;

namespace CommonCore.Models
{
    public class BlueSkySessionResponse
    {
        public string AccessJwt { get; set; }
        public string Did { get; set; }
        public string Handle { get; set; }
    }
}
