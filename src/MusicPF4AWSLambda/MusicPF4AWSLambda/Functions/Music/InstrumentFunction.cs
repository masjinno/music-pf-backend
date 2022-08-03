using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using MusicPF4AWSLambda.Models;
using MusicPF4AWSLambda.Models.Database;
using MusicPF4AWSLambda.Resources;
using System.Text.Json;

namespace MusicPF4AWSLambda.Functions.Music
{
    /// <summary>
    /// /music/instrumentation/instruments 系のLambda関数
    /// </summary>
    public class InstrumentFunction
    {
        private static DynamoDBInstrument dynamoDBInstrument;
        private static DynamoDBInstrumentCategory dynamoDBInstrumentCategory;
        private InstrumentLogic logic;

        public InstrumentFunction()
        {
            InstrumentFunction.dynamoDBInstrument = new DynamoDBInstrument();
            InstrumentFunction.dynamoDBInstrumentCategory = new DynamoDBInstrumentCategory();
            this.logic = new InstrumentLogic();
        }

        /// <summary>
        /// 楽器編成に使われる楽器を新規登録する
        /// POST: /v1/music/instrumentation/instrument
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public APIGatewayProxyResponse PostInstrument(Request request, ILambdaContext context)
        {
            Console.WriteLine("Request=" + JsonSerializer.Serialize<Request>(request));
            InstrumentPost reqBody = JsonSerializer.Deserialize<InstrumentPost>(request.Body);
            (int status, object respBody) = this.logic.PostInstrument(reqBody, InstrumentFunction.dynamoDBInstrument);
            return new APIGatewayProxyResponse
            {
                StatusCode = status,
                Body = JsonSerializer.Serialize(respBody)
            };
        }

        /// <summary>
        /// 楽器分類一覧を取得する
        /// GET: /v1/music/instrumentation/instrument/categories
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public APIGatewayProxyResponse GetInstrumentCategories(Request request, ILambdaContext context)
        {
            Console.WriteLine("Request=" + JsonSerializer.Serialize<Request>(request));
            (int status, object respBody) = this.logic.GetInstrumentCategories(InstrumentFunction.dynamoDBInstrumentCategory);
            return new APIGatewayProxyResponse
            {
                StatusCode = status,
                Body = JsonSerializer.Serialize(respBody)
            };
        }
    }
}
