using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;

namespace MusicPF4AWSLambda.Models.Database
{
    internal abstract class DynamoDB
    {
        private AmazonDynamoDBClient client = new AmazonDynamoDBClient();

        protected Table table;

        /// <summary>
        /// DynamoDB使用の準備
        /// </summary>
        /// <param name="tableName">テーブル名</param>
        protected DynamoDB(string tableName)
        {
            this.table = Table.LoadTable(client, tableName);
        }

        /// <summary>
        /// アイテム登録
        /// </summary>
        /// <param name="item"></param>
        /// <returns>アイテム登録が成功したか</returns>
        internal virtual bool PutItem(object item)
        {
            Task<Document> response = this.table.PutItemAsync(this.GenerateDocument(item));
            return response.IsCompletedSuccessfully;
        }

        /// <summary>
        /// アイテム全件取得
        /// テーブルのサイズが大きい場合には処理が重くなるため使用しないこと。
        /// </summary>
        /// <param name="item"></param>
        /// <returns>アイテム全件</returns>
        internal virtual List<object> RetrieveAllItems()
        {
            Search response = this.table.Scan(new ScanFilter());
            return response.GetNextSetAsync().Result.Select(doc => this.GenerateObject(doc)).ToList();
        }

        /// <summary>
        /// IDのアイテムを取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">引数が不正なnull</exception>
        /// <exception cref="ArgumentException">引数が不正</exception>
        /// <exception cref="NullReferenceException">idに該当する楽器なし</exception>
        internal virtual object RetrieveItemById(Primitive id)
        {
            if (id == null) throw new ArgumentNullException();
            if (id == String.Empty) throw new ArgumentException();
            return this.GenerateObject(this.table.GetItemAsync(id).Result);
        }

        /// <summary>
        /// IDのアイテムを削除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal virtual bool DeleteItemById(Primitive id)
        {
            if (id == null) throw new ArgumentNullException();
            if (id == String.Empty) throw new ArgumentException();
            return this.table.DeleteItemAsync(id).IsCompleted;
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

        /// <summary>
        /// Booleanが期待されるアイテムの値を取得する。
        /// PutItemメソッドでbool値を格納した場合、DB内部では数値(0 or 1)で格納される。
        /// しかし、DynamoDBとしてはbool値で保持することも可能(AWSコンソール上で操作する)なので、どちらの場合にも対応する処理を行う。
        /// </summary>
        /// <param name="targetValue"></param>
        /// <returns></returns>
        protected bool ReadBooleanValue(DynamoDBEntry targetValue)
        {
            Console.WriteLine("★targetValue=" + targetValue);
            Console.WriteLine("★return: " + ((targetValue.GetType() == typeof(bool)) ? (bool)targetValue : (targetValue == "1")));
            return (targetValue.GetType() == typeof(bool)) ? (bool)targetValue : (targetValue == "1");
        }
    }
}
