using MusicPF4AWSLambda.Models.Database;
using MusicPF4AWSLambda.Resources;
using System.Net;

namespace MusicPF4AWSLambda.Models
{
    public class InstrumentLogic
    {
        /// <summary>
        /// 楽器登録処理
        /// </summary>
        /// <param name="request"></param>
        /// <param name="db"></param>
        /// <returns>
        /// status: レスポンスのHTTPステータス
        /// body: レスポンスボディオブジェクト
        /// </returns>
        public (int status, object body) PostInstrument(InstrumentPost request, DynamoDBInstrument db)
        {
            try
            {
                db.PutItem(request.Instrument);
                return ((int)HttpStatusCode.OK, request);
            }
            catch (NullReferenceException ex)
            {
                return (
                    (int)HttpStatusCode.BadRequest,
                    new ErrorResponse(
                        "instrument.invalid_parameter",
                        ex.Message)
                    );
            }
            catch (Exception ex)
            {
                return (
                    (int)HttpStatusCode.BadRequest,
                    new ErrorResponse(
                            ex.GetType().FullName,
                            ex.Message)
                    );
            }
        }

        /// <summary>
        /// 楽器分類一覧取得処理
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public (int status, object respBody) GetInstrumentCategories(DynamoDBInstrumentCategory db)
        {
            try
            {
                List<InstrumentCategory> allCategories = db.GetAllItems().Select(i => (InstrumentCategory)i).ToList();
                return ((int)HttpStatusCode.OK, allCategories);
            }
            catch (Exception ex)
            {
                return (
                    (int)HttpStatusCode.BadRequest,
                    new ErrorResponse(
                            ex.GetType().FullName,
                            ex.Message)
                    );
            }
        }
    }
}
