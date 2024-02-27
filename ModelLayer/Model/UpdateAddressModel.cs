using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.Model
{
    public class UpdateAddressModel
    {
        public int Uid { get; set; }
        public int Aid {  get; set; }
        public string fullAddress { get; set; }
        public string city { get; set; }
        public string state { get; set; }

    }
}
