using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using QuotesProject_ASP.Models;
using System.Data.Entity.Migrations;

namespace QuotesProject_ASP.DAL
{
    public class QuotesInitializer : System.Data.Entity.DropCreateDatabaseAlways<QuoteContext>
    {
        protected override void Seed(QuoteContext context)
        {
            context.Authors.AddOrUpdate(a => a.LastName,
            new Author { FirstName = "Pablo", LastName = "Picasso", Country = "Italy" },
            new Author { FirstName = "Wisława", LastName = "Szymborska", Country = "Poland" },
            new Author { FirstName = "Carlos", LastName = "Zafon", Country = "Spain" }
                );
            context.SaveChanges();
            context.Quotes.AddOrUpdate(q => q.Content,
            new Quote { Content = "You can do anything, but not everything", Source = "", AuthorID = 1, Category = QuoteCategory.Happiness },
            new Quote { Content = "You must be the change you wish to see in the world.", Source = "Cień Wiatru", AuthorID = 2, Category = QuoteCategory.Life }
                );
            context.SaveChanges();
        }
    }
}