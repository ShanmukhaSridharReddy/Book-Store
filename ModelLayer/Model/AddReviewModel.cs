using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Sessions
{
    public  class AddReviewModel
    {
        public string review { get; set; }
        public int star { get; set; }
        public int bookId { get; set; }
    }
}
