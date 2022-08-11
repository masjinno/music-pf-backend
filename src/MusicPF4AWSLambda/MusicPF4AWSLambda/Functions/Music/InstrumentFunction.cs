using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using MusicPF4AWSLambda.Models;
using MusicPF4AWSLambda.Models.Database;
using MusicPF4AWSLambda.Resources;
using System.Net;
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
            Console.WriteLine("Request=" + JsonSerializer.Serialize(request));
            Instrument reqBody = JsonSerializer.Deserialize<Instrument>(request.Body);
            (int status, object respBody) = this.logic.PostInstrument(reqBody, InstrumentFunction.dynamoDBInstrument);
            return new APIGatewayProxyResponse
            {
                StatusCode = status,
                Body = JsonSerializer.Serialize(respBody)
            };
        }

        /// <summary>
        /// 編成情報に紐づいていない楽器をすべて削除する
        /// DELETE: /v1/music/instrumentation/instrument
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public APIGatewayProxyResponse DeleteInstrument(Request request, ILambdaContext context)
        {
            Console.WriteLine("Request=" + JsonSerializer.Serialize(request));
            // 未実装メソッド
            return new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Body = JsonSerializer.Serialize(new ErrorResponse("NotImplementedError", "未実装です"))
            };
        }

        /// <summary>
        /// 楽器の詳細を取得する
        /// GET: /v1/music/instrumentation/instrument?id={id}
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public APIGatewayProxyResponse GetInstrument(Request request, ILambdaContext context)
        {
            Console.WriteLine("Request=" + JsonSerializer.Serialize(request));
            Console.WriteLine("multiValueQueryStringParameters:" + request.MultiValueQueryStringParameters);
            List<string> instrumentIds;
            try
            {
                JsonElement je1 = request.MultiValueQueryStringParameters;
                Console.WriteLine("★je1: " + je1);
                JsonElement je2 = je1.GetProperty("id");
                Console.WriteLine("★je2: " + je2);
                JsonElement.ArrayEnumerator jea1 = je2.EnumerateArray();
                Console.WriteLine("★jea1: " + jea1);
                JsonElement.ArrayEnumerator jea2 = jea1.GetEnumerator();
                Console.WriteLine("★jea2: " + jea2);
                IEnumerable<string?> ls = jea2.Select(e => e.GetString());
                Console.WriteLine("★ls: " + ls);
                instrumentIds = ls.ToList();
                Console.WriteLine("★instrumentIds.ToString(): " + instrumentIds.ToString());
                Console.WriteLine("★instrumentIds.Count: " + instrumentIds.Count);
                instrumentIds.ForEach(i => Console.WriteLine("★instrumentId: " + i));

                //instrumentIds = request
                //    .MultiValueQueryStringParameters
                //    .GetProperty("id")
                //    .EnumerateArray()
                //    .GetEnumerator()
                //    .Select(e => e.GetString())
                //    .ToList();
            }
            catch (InvalidOperationException ex)
            {
                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = JsonSerializer.Serialize(new ErrorResponse("absent_query_id", "クエリIDが指定されていません"))
                };
            }
            (int status, object respBody) = this.logic.GetInstrument(instrumentIds, InstrumentFunction.dynamoDBInstrument);
            return new APIGatewayProxyResponse
            {
                StatusCode = status,
                Body = JsonSerializer.Serialize(respBody)
            };
        }

        /// <summary>
        /// 楽器情報を更新する
        /// PUT: /v1/music/instrumentation/instrument?id={id}
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public APIGatewayProxyResponse PutInstrument(Request request, ILambdaContext context)
        {
            Console.WriteLine("Request=" + JsonSerializer.Serialize(request));
            JsonElement targetInstrumentIdJsonElem;
            string? targetInstrumentId = null;
            try
            {
                targetInstrumentId = request.QueryStringParameters.GetProperty("id").GetString();
            }
            catch (InvalidOperationException ex)
            {
                /// request.QueryStringParameters の ValueKind が Object ではない
                // targetInstrumentIdにnullを代入すればよいが、代入済み
                // targetInstrumentId = null;
            }
            Instrument reqBody = JsonSerializer.Deserialize<Instrument>(request.Body);
            
            (int status, object respBody) = this.logic.PutInstrument(targetInstrumentId, reqBody, InstrumentFunction.dynamoDBInstrument);
            return new APIGatewayProxyResponse
            {
                StatusCode = status,
                Body = JsonSerializer.Serialize(respBody)
            };
        }

        /// <summary>
        /// 楽器分類一覧を取得する
        /// GET: /v1/music/instrumentation/instrument/categories?id={id}
        /// クエリのid指定は任意。指定ない場合は全てのカテゴリを取得。
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public APIGatewayProxyResponse GetInstrumentCategories(Request request, ILambdaContext context)
        {
            Console.WriteLine("Request=" + JsonSerializer.Serialize(request));
            JsonElement categoryIdJsonElem;
            string? categoryId = null;
            try
            {
                if (request.QueryStringParameters.TryGetProperty("id", out categoryIdJsonElem))
                {
                    categoryId = categoryIdJsonElem.GetString();
                }
            }
            catch (InvalidOperationException ex)
            {
                /// request.QueryStringParameters の ValueKind が Object ではない
                // categoryIdにnullを代入すればよいが、代入済み
                // categoryId = null;
            }

            (int status, object respBody) = this.logic.GetInstrumentCategories(categoryId, InstrumentFunction.dynamoDBInstrumentCategory);
            return new APIGatewayProxyResponse
            {
                StatusCode = status,
                Body = JsonSerializer.Serialize(respBody)
            };
        }
    }
}
