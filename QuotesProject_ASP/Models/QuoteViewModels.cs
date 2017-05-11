using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuotesProject_ASP.Models
{
    public class DetailsQuoteViewModel
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Content { get; set; }
        public string Source { get; set; }
        public QuoteCategory Category { get; set; }
    }
}