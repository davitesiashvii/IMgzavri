using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Shared.Domain.Models
{
    public class Result
    {
        public string Message { get; set; }

        //public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();

        public object Response { get; set; }

        public HttpResultStatus Status { get; set; } = HttpResultStatus.Success;

        public static Result Success()
        {
            return new Result(HttpResultStatus.Success);
        }

        public static Result Error(string message)
        {
            return new Result(message, HttpResultStatus.Error);
        }

        public Result()
        {
        }

        public Result(HttpResultStatus status)
        {
            Status = status;
        }

        public Result(string message, HttpResultStatus status)
        {
            Message = message;
            Status = status;
        }
    }
}
