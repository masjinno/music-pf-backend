using Amazon.DynamoDBv2;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
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

        public InstrumentFunction()
        {
            InstrumentFunction.dynamoDBInstrument = new DynamoDBInstrument();
        }

        /// <summary>
        /// 楽器編成に使われる楽器を新規登録する
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public APIGatewayProxyResponse PostInstrument(Request request, ILambdaContext context)
        {
            InstrumentPost? reqBody;
            try
            {
                reqBody = JsonSerializer.Deserialize<InstrumentPost>(request.Body);

                InstrumentFunction.dynamoDBInstrument.PutItem(reqBody.Instrument);
            }
            catch (NullReferenceException ex)
            {
                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = JsonSerializer.Serialize(
                        new ErrorResponse(
                            "instrument.invalid_parameter",
                            ex.Message))
                };
            }
            catch (Exception ex)
            {
                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = JsonSerializer.Serialize(
                        new ErrorResponse(
                            ex.GetType().FullName,
                            ex.Message))
                };
            }

            return new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = JsonSerializer.Serialize(reqBody)
            };
        }
    }
}
