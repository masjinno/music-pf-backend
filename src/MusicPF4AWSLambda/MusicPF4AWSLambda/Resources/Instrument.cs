using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MusicPF4AWSLambda.Resources
{
    public class Instrument
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("jaJp")]
        public string? NameJaJp { get; set; }

        [JsonPropertyName("enUs")]
        public string? NmaeEnUs { get; set; }

        [JsonPropertyName("itIt")]
        public string? NmaeItIt { get; set; }

        [JsonPropertyName("abbreviation")]
        public string? Abbreviation { get; set; }

        [JsonPropertyName("categoryId")]
        public string? CategoryId { get; set; }

        [JsonPropertyName("isUsual")]
        public bool? IsUsual { get; set; }
    }
}
