using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Areas.Shelves.Data
{
    public class mShelves
    {
        public int selfId { get; set; }
        public string selfCode { get; set; }
        public int selfRackId { get; set; }
        public string selfStatus { get; set; }
        public string rackCode { get; set; }
        public string rackStatus { get; set; }
    }
}