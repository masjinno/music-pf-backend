using System.Text.Json.Serialization;

namespace MusicPF4AWSLambda.Resources
{
    public class InstrumentCategoriesResp
    {
        [JsonPropertyName("instrumentCategories")]
        public List<InstrumentCategory> InstrumentCategories { get; set; }
    }
}
