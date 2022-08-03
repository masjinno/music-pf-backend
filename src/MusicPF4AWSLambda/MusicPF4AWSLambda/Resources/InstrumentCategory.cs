using System.Text.Json.Serialization;

namespace MusicPF4AWSLambda.Resources
{
    public class InstrumentCategory
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("jaJp")]
        public string NameJaJp { get; set; }

        [JsonPropertyName("enUs")]
        public string NameEnUs { get; set; }

        [JsonPropertyName("index")]
        public string Index { get; set; }
    }
}
