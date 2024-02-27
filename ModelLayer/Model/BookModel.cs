using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.Model
{
    public class BookModel
    {
        public int id {  get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public string image { get; set; }
        public string quantity { get; set; }
        public string price { get; set; }
        public string description { get; set; }
    }
}
