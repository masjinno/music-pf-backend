using System.Text.Json.Serialization;

namespace MusicPF4AWSLambda.Resources
{
    public class Instrument
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("jaJp")]
        public string NameJaJp { get; set; }

        [JsonPropertyName("enUs")]
        public string NameEnUs { get; set; }

        [JsonPropertyName("itIt")]
        public string? NameItIt { get; set; }

        [JsonPropertyName("abbreviation")]
        public string Abbreviation { get; set; }

        [JsonPropertyName("categoryId")]
        public string CategoryId { get; set; }

        [JsonPropertyName("isUsual")]
        public Boolean IsUsual { get; set; }

        // Editableは編集可否を指す。
        [JsonIgnore]
        public Boolean Editable { get; set; } = true;
    }
}
