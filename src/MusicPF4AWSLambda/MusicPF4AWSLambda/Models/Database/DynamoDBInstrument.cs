using Amazon.DynamoDBv2.DocumentModel;
using MusicPF4AWSLambda.Resources;

namespace MusicPF4AWSLambda.Models.Database
{
    /// <summary>
    /// Instrumentsテーブルを操作するためのクラス
    /// </summary>
    internal class DynamoDBInstrument : DynamoDB
    {
        private const string tableName = "instruments";

        internal DynamoDBInstrument() : base(DynamoDBInstrument.tableName)
        {
            // do nothing
        }

        /// <summary>
        /// Documentインスタンス生成
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override Document GenerateDocument(object item)
        {
            if (item is not Instrument)
            {
                throw new ArgumentException("Argument " + nameof(item) + " is not " + typeof(Instrument).FullName,
                    nameof(item));
            }
            Instrument instrument = (Instrument)item;

            Document ret = new Document();
            ret["id"] = instrument.Id;
            ret["ja_jp"] = instrument.NameJaJp;
            ret["en_us"] = instrument.NameEnUs;
            ret["it_it"] = instrument.NameItIt;
            ret["abbreviation"] = instrument.Abbreviation;
            ret["category_id"] = instrument.CategoryId;
            ret["is_usual"] = instrument.IsUsual;

            return ret;
        }

        /// <summary>
        /// Documentをオブジェクトに変換
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override object GenerateObject(Document item)
        {
            return new Instrument
            {
                Id = item["id"],
                NameJaJp = item["ja_jp"],
                NameEnUs = item["en_us"],
                NameItIt = item["it_it"],
                Abbreviation = item["abbreviation"],
                CategoryId = item["category_id"],
                IsUsual = item["is_usual"].Equals("1")
            };
        }
    }
}
