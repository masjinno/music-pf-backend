using MusicPF4AWSLambda.Models.Database;
using MusicPF4AWSLambda.Resources;
using System.Net;
using System.Text.Json;

namespace MusicPF4AWSLambda.Models
{
    internal class InstrumentLogic
    {
        /// <summary>
        /// 楽器登録処理
        /// </summary>
        /// <param name="instrument"></param>
        /// <param name="db"></param>
        /// <returns>
        /// status: レスポンスのHTTPステータス
        /// body: レスポンスボディオブジェクト
        /// </returns>
        internal (int status, object body) PostInstrument(Instrument instrument, DynamoDBInstrument db)
        {
            try
            {
                db.RetrieveItemById(instrument.Id);
                // 例外を出さずに取得した場合は既存のIDが存在するので、新規登録は失敗
                return ((int)HttpStatusCode.BadRequest, new ErrorResponse(ErrorConstants.IdAlreadyExists));
            }
            catch (Exception ex) when (ex is ArgumentNullException or ArgumentException)
            {
                return ((int)HttpStatusCode.BadRequest, new ErrorResponse(ErrorConstants.InvalidRequestBody));
            }
            catch (NullReferenceException)
            {
                // 【正常系】instrument.Idに該当する楽器なし
                // do nothing
            }

            try
            {
                db.PutItem(instrument);
                return ((int)HttpStatusCode.OK, instrument);
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
        /// 楽器詳細取得処理
        /// </summary>
        /// <param name="instrumentId">楽器ID</param>
        /// <param name="dynamoDBInstrument"></param>
        /// <returns>
        /// status: レスポンスのHTTPステータス
        /// body: レスポンスボディオブジェクト
        /// </returns>
        internal (int status, object respBody) GetInstrument(List<string> instrumentIds, DynamoDBInstrument db)
        {
            try
            {
                List<Instrument> instrumentList = new List<Instrument>();
                instrumentIds.ForEach(instrumentId =>
                {
                    Instrument instrument = (Instrument)db.RetrieveItemById(instrumentId);
                    instrumentList.Add(instrument);
                });
                return ((int)HttpStatusCode.OK, instrumentList);
            }
            catch (ArgumentNullException)
            {
                // ArgumentNullException : id 指定なし
                return ((int)HttpStatusCode.BadRequest, new ErrorResponse(ErrorConstants.AbsentQueryId));
            }
            catch (Exception ex) when (ex is ArgumentException or NullReferenceException)
            {
                // ArgumentException : idが空 (クエリにて`id=`とだけ書かれていて値なし)
                // NullReferenceException : idに該当する楽器なし
                return ((int)HttpStatusCode.BadRequest, new ErrorResponse(ErrorConstants.InvalidQueryId));
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
        /// 楽器情報更新処理
        /// </summary>
        /// <param name="instrumentPut"></param>
        /// <param name="dynamoDBInstrument"></param>
        /// <returns>
        /// status: レスポンスのHTTPステータス
        /// body: レスポンスボディオブジェクト
        /// </returns>
        internal (int status, object respBody) PutInstrument(string targetInstrumentId, Instrument newInstrument, DynamoDBInstrument db)
        {
            Instrument targetItem;
            try
            {
                targetItem = (Instrument)db.RetrieveItemById(targetInstrumentId);
                Console.WriteLine("★targetItem.Editable=" + targetItem.Editable);
                // 編集禁止項目であればその旨を返す
                if (!targetItem.Editable)
                {
                    return ((int)HttpStatusCode.Forbidden, new ErrorResponse(ErrorConstants.EditProhibited));
                }
            }
            catch (ArgumentNullException)
            {
                // ArgumentNullException : idクエリの指定なし
                return ((int)HttpStatusCode.BadRequest, new ErrorResponse(ErrorConstants.AbsentQueryId));
            }
            catch (Exception ex) when (ex is ArgumentException or NullReferenceException)
            {
                // ArgumentException : idクエリが空
                // NullReferenceException : idに該当する楽器なし
                return ((int)HttpStatusCode.BadRequest, new ErrorResponse(ErrorConstants.InvalidQueryId));
            }

            // Editableはユーザー指定ではないので、古いデータを引き継ぐ
            newInstrument.Editable = targetItem.Editable;
            if (targetItem.Id.Equals(newInstrument.Id))
            {
                db.PutItem(newInstrument);
            }
            else
            {
                db.DeleteItemById(targetInstrumentId);
                db.PutItem(newInstrument);
            }

            return ((int)HttpStatusCode.OK, newInstrument);
        }

        /// <summary>
        /// 楽器分類一覧取得処理
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="db"></param>
        /// <returns>
        /// status: レスポンスのHTTPステータス
        /// body: レスポンスボディオブジェクト
        /// </returns>
        internal (int status, object respBody) GetInstrumentCategories(string? categoryId, DynamoDBInstrumentCategory db)
        {
            try
            { 
                List<InstrumentCategory> categories;
                if (String.IsNullOrEmpty(categoryId))
                {
                    categories = db.RetrieveAllItems().Select(i => (InstrumentCategory)i).ToList();
                }
                else
                {
                    categories = new List<InstrumentCategory>() { (InstrumentCategory)db.RetrieveItemById(categoryId) };
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
