using Amazon.DynamoDBv2.DocumentModel;
using MusicPF4AWSLambda.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPF4AWSLambda.Models.Database
{
    /// <summary>
    /// Instrumentsテーブルを操作するためのクラス
    /// </summary>
    public class DynamoDBInstrument : DynamoDB
    {
        private const string tableName = "instruments";

        public DynamoDBInstrument() : base(tableName)
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
            ret["en_us"] = instrument.NmaeEnUs;
            ret["it_it"] = instrument.NmaeItIt;
            ret["abbreviation"] = instrument.Abbreviation;
            ret["category_id"] = instrument.CategoryId;
            ret["is_usual"] = instrument.IsUsual;

            return ret;
        }
    }
}
