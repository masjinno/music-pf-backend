using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;

namespace MusicPF4AWSLambda.Models
{
    public abstract class DynamoDB
    {
        protected AmazonDynamoDBClient client = new AmazonDynamoDBClient();

        private Table productCatalog;

        /// <summary>
        /// DynamoDB使用の準備
        /// </summary>
        /// <param name="tableName">テーブル名</param>
        protected DynamoDB(string tableName)
        {
            this.productCatalog = Table.LoadTable(this.client, tableName);
        }

        /// <summary>
        /// アイテム登録
        /// </summary>
        /// <param name="item"></param>
        /// <returns>アイテム登録が成功したか</returns>
        public bool PutItem(object item)
        {
            Task<Document> response = this.productCatalog.PutItemAsync(this.GenerateDocument(item));
            return response.IsCompletedSuccessfully;
        }

        /// <summary>
        /// Documentインスタンス生成
        /// 対象となるDBごとにカラムに対応する値設定を行う
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected abstract Document GenerateDocument(object item);
    }
}
