using System.Text.Json.Serialization;

namespace MusicPF4AWSLambda.Resources
{
    public class InstrumentPost
    {
        [JsonPropertyName("instrument")]
        public Instrument Instrument { get; set; }
    }
}
