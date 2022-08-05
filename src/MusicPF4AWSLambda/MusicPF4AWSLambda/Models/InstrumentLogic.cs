using MusicPF4AWSLambda.Models.Database;
using MusicPF4AWSLambda.Resources;
using System.Net;

namespace MusicPF4AWSLambda.Models
{
    internal class InstrumentLogic
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
        internal (int status, object body) PostInstrument(InstrumentPost request, DynamoDBInstrument db)
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
        /// <param name="categoryId"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        internal (int status, object respBody) GetInstrumentCategories(string? categoryId, DynamoDBInstrumentCategory db)
        {
            try
            {
                List<InstrumentCategory> categories;
                if (String.IsNullOrEmpty(categoryId))
                {
                    categories = db.GetAllItems().Select(i => (InstrumentCategory)i).ToList();
                }
                else
                {
                    categories = new List<InstrumentCategory>() { (InstrumentCategory)db.GetItemById(categoryId) };
                }
                return ((int)HttpStatusCode.OK, categories);
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
