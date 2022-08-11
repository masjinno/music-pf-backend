using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPF4AWSLambda.Resources
{
    public class ErrorConstants
    {
        public string Id { get; set; }
        public string Message { get; set; }

        private ErrorConstants(string id, string message)
        {
            Id = id;
            Message = message;
        }

        public static readonly ErrorConstants AbsentQueryId = new ErrorConstants("absetn_query_id", "クエリIDの指定がありません");
        public static readonly ErrorConstants InvalidQueryId = new ErrorConstants("invalid_query_id", "クエリIDが不正な値です");

        public static readonly ErrorConstants InvalidRequestBody = new ErrorConstants("invalid_reqbody", "リクエストボディが不正です");

        public static readonly ErrorConstants IdAlreadyExists = new ErrorConstants("already_exists_id", "IDが既に存在します");
        public static readonly ErrorConstants EditProhibited = new ErrorConstants("edit_prohibited", "編集が禁止された項目です");
    }
}
