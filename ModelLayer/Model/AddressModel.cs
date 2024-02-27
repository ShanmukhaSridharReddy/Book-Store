using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelLayer.Model
{
    public class AddressModel
    {
        //public int Aid {  get; set; }
        public string fullName {  get; set; }
        public string fullAddress {  get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string phoneNo { get; set; }
        public int Type { get; set; }

    }
}
