using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MusicPF4AWSLambda.Resources
{
    public class InstrumentPost
    {
        [JsonPropertyName("instrument")]
        public Instrument? Instrument { get; set; }
    }
}
