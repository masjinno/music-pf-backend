using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MusicPF4AWSLambda.Resources
{
    public class ErrorResponse
    {
        public ErrorResponse(string errorId, string errorMessage = null)
        {
            this.Error = new Error
            {
                Id = errorId,
                Message = String.IsNullOrEmpty(errorMessage) ? String.Empty : errorMessage
            };
        }

        public ErrorResponse(ErrorConstants error)
        {
            this.Error = new Error
            {
                Id = error.Id,
                Message = error.Message
            };
        }

        [JsonPropertyName("error")]
        public Error Error { get; set; }
    }

    public class Error
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
