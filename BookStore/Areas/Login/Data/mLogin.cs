using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Areas.Login.Data
{
    public class mLogin
    {
        public int BSID { get; set; }
        public string BS_USER { get; set; }
        public string BS_PASS { get; set; }
        public string BS_NAME { get; set; }
        public string BS_DESIG { get; set; }
    }
}