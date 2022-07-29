using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using MusicPF4AWSLambda.Resources;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace MusicPF4AWSLambda;

public class Function
{
    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public APIGatewayProxyResponse FunctionHandler(Request request, ILambdaContext context)
    {
        Console.WriteLine("resource:" + request.Resource);
        Console.WriteLine("path:" + request.Path);
        Console.WriteLine("httpMethod:" + request.HttpMethod);
        Console.WriteLine("headers:" + request.Headers.ToString());
        Console.WriteLine("multiValueHeaders:" + request.MultiValueHeaders.ToString());
        Console.WriteLine("queryStringParameters:" + request.QueryStringParameters);
        Console.WriteLine("multiValueQueryStringParameters:" + request.MultiValueQueryStringParameters);
        Console.WriteLine("pathParameters:" + request.PathParameters);
        Console.WriteLine("stageVariables:" + request.StageVariables);
        Console.WriteLine("requestContext:" + request.RequestContext);
        Console.WriteLine("body:" + request.Body);
        Console.WriteLine("isBase64Encoded:" + request.IsBase64Encoded);
        return new APIGatewayProxyResponse
        {
            StatusCode = (int)HttpStatusCode.OK,
            Body = JsonSerializer.Serialize(new object())
        };
    }
}
