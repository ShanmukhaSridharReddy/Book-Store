using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.Model
{
    public  class ReviewResponseModel
    {
        public string name { get; set; }
        public string review { get; set; }
        public int star { get; set; }
        public int bookId { get; set; }
    }
}
