using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edupocket.Application.ResponseModels
{
    public class BaseResponse
    {
        public string? Message { get; set; }
        public bool IsSuccessful { get; set; }
        public object? Data { get; set; }   
    }
}
