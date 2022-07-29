using System.Text.Json;
using System.Text.Json.Serialization;

namespace MusicPF4AWSLambda.Resources
{
    public class Request
    {

        [JsonPropertyName("resource")]
        public string? Resource { get; set; }

        [JsonPropertyName("path")]
        public string? Path { get; set; }

        [JsonPropertyName("httpMethod")]
        public string? HttpMethod { get; set; }

        [JsonPropertyName("headers")]
        public JsonElement Headers { get; set; }

        [JsonPropertyName("multiValueHeaders")]
        public JsonElement MultiValueHeaders { get; set; }

        [JsonPropertyName("queryStringParameters")]
        public JsonElement QueryStringParameters { get; set; }

        [JsonPropertyName("multiValueQueryStringParameters")]
        public JsonElement MultiValueQueryStringParameters { get; set; }

        [JsonPropertyName("pathParameters")]
        public JsonElement PathParameters { get; set; }

        [JsonPropertyName("stageVariables")]
        public JsonElement StageVariables { get; set; }

        [JsonPropertyName("requestContext")]
        public JsonElement RequestContext { get; set; }

        [JsonPropertyName("body")]
        public string? Body { get; set; }

        [JsonPropertyName("isBase64Encoded")]
        public Boolean IsBase64Encoded { get; set; }
    }
}
