using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Areas.Books.Data
{
    public class mBooks
    {
        public string selfCode { get; set; }
        public string selfStatus { get; set; }
        public string rackCode { get; set; }
        public int BookId { get; set; }
        public string BookCode { get; set; }
        public string BookName { get; set; }
        public string BookAuthor { get; set; }
        public string BookIsAvail { get; set; }
        public string BookStatus { get; set; }
        public decimal BookPrice { get; set; }
        public int BookselfId { get; set; }
    }
}