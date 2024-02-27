using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.Model
{
    public class ResponseModel<T>
    {
        public Boolean IsSuccess {  get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

    }
}
