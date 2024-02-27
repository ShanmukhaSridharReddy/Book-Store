using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.Model
{
    public class OrderModel
    {
        public int userId { get; set; }
        public int Pid { get; set; }
        public int totalprice { get; set; }
        public int quantity { get; set; }
    }
}
