using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using MusicPF4AWSLambda.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MusicPF4AWSLambda.Functions.Music
{
    /// <summary>
    /// /music/instrumentation/instruments 系のLambda関数
    /// </summary>
    public class InstrumentFunction
    {
        /// <summary>
        /// 楽器編成に使われる楽器を新規登録する
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public APIGatewayProxyResponse PostInstrument(Request request, ILambdaContext context)
        {
            InstrumentPost? reqBody;
            reqBody = JsonSerializer.Deserialize<InstrumentPost>(request.Body);

            return new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = JsonSerializer.Serialize(reqBody)
            };
        }
    }
}
