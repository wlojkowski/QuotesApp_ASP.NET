using QuotesProject_ASP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QuotesProject_ASP.DAL
{
    public class QuoteContext : DbContext
    {
        public QuoteContext() : base("QuoteContext")
        {
        }

        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Author> Authors { get; set; }

    }
}