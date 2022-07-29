using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using System.Text.Json;
using MusicPF4AWSLambda.Resources;
using System.Net;

namespace MusicPF4AWSLambda.Tests;

public class FunctionTest
{
    [Fact]
    public void TestToUpperFunction()
    {

        // Invoke the lambda function and confirm the string was upper cased.
        var function = new Function();
        var context = new TestLambdaContext();
        Request req = new Request();
        
        var resp = function.FunctionHandler(req, context);

        Assert.Equal((int)HttpStatusCode.OK, resp.StatusCode);
    }
}
