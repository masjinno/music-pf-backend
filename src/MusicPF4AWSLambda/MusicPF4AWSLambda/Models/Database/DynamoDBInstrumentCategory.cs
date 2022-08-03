using Amazon.DynamoDBv2.DocumentModel;
using MusicPF4AWSLambda.Resources;
using System.Text.Json;

namespace MusicPF4AWSLambda.Models.Database
{
    internal class DynamoDBInstrumentCategory : DynamoDB
    {
        private const string tableName = "instrument_categories";

        internal DynamoDBInstrumentCategory() : base(DynamoDBInstrumentCategory.tableName)
        {
            // do nothing
        }

        /// <summary>
        /// アイテム登録メソッド
        /// InstrumentCategoryテーブルは新規のアイテム登録を受け付けないため、InvalidOperationExceptionを投げる
        /// </summary>
        /// <exception cref="InvalidOperationException">呼び出し禁止メソッド</exception>
        internal override bool PutItem(object item)
        {
            throw new InvalidOperationException("Prohibit to call PutItem method for instrument_category table.");
        }

        /// <summary>
        /// Documentインスタンス生成
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override Document GenerateDocument(object item)
        {
            if (item is not InstrumentCategory)
            {
                throw new ArgumentException("Argument " + nameof(item) + " is not " + typeof(InstrumentCategory).FullName,
                    nameof(item));
            }
            InstrumentCategory instrumentCategory = (InstrumentCategory)item;

            Document ret = new Document();
            ret["id"] = instrumentCategory.Id;
            ret["ja_jp"] = instrumentCategory.NameJaJp;
            ret["en_us"] = instrumentCategory.NameEnUs;
            ret["index"] = instrumentCategory.Index;

            return ret;
        }

        /// <summary>
        /// Documentをオブジェクトに変換
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override object GenerateObject(Document item)
        {
            return new InstrumentCategory
            {
                Id = item["id"],
                NameJaJp = item["ja_jp"],
                NameEnUs = item["en_us"],
                Index = item["index"]
            };
        }
    }
}
