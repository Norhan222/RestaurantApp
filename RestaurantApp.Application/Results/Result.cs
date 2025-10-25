using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.Results
{
    public class Result
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public static Result OK(string? message)
        {
            return new Result { Success = true, Message = message };
        }
        public static Result Fail(string? message)
        {
            return new Result() { Success = false, Message = message };
        }
    }
}
