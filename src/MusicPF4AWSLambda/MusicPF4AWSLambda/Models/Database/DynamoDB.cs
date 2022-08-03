using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;

namespace MusicPF4AWSLambda.Models.Database
{
    internal abstract class DynamoDB
    {
        private AmazonDynamoDBClient client = new AmazonDynamoDBClient();

        protected Table productCatalog;

        /// <summary>
        /// DynamoDB使用の準備
        /// </summary>
        /// <param name="tableName">テーブル名</param>
        protected DynamoDB(string tableName)
        {
            productCatalog = Table.LoadTable(client, tableName);
        }

        /// <summary>
        /// アイテム登録
        /// </summary>
        /// <param name="item"></param>
        /// <returns>アイテム登録が成功したか</returns>
        internal virtual bool PutItem(object item)
        {
            Task<Document> response = productCatalog.PutItemAsync(GenerateDocument(item));
            return response.IsCompletedSuccessfully;
        }

        /// <summary>
        /// アイテム全件取得
        /// テーブルのサイズが大きい場合には処理が重くなるため使用しないこと。
        /// </summary>
        /// <param name="item"></param>
        /// <returns>アイテム全件</returns>
        internal virtual List<object> GetAllItems()
        {
            Search response = productCatalog.Scan(new ScanFilter());
            return response.GetNextSetAsync().Result.Select(doc => GenerateObject(doc)).ToList();
        }

        /// <summary>
        /// Documentインスタンス生成
        /// 対象となるDBごとにカラムに対応する値設定を行う
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected abstract Document GenerateDocument(object item);

        /// <summary>
        /// Documentをオブジェクトに変換する
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected abstract object GenerateObject(Document item);
    }
}
