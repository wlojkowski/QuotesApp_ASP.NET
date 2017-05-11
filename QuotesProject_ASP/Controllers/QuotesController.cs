using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuotesProject_ASP.DAL;
using QuotesProject_ASP.Models;

namespace QuotesProject_ASP.Controllers
{
    [Authorize]
    public class QuotesController : Controller
    {
        private QuoteContext db = new QuoteContext();

        // GET: Quotes
        [AllowAnonymous]
        public ActionResult Index(string sourceString,string searchString, string searchCategory)
        {
            var sourceList = new List<string>();

            var sourceQuery = from d in db.Quotes
                           orderby d.Source
                           select d.Source;

            sourceList.AddRange(sourceQuery.Distinct());
            ViewBag.sourceString = new SelectList(sourceList);
            ViewBag.searchCategory = new SelectList(Enum.GetNames(typeof(QuoteCategory)).ToList());

            var quotes = db.Quotes.Include(q => q.Author);

            if (!String.IsNullOrEmpty(searchString))
            {
                quotes = quotes.Where(s => s.Author.FirstName.Contains(searchString) || s.Author.LastName.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(sourceString))
            {
                quotes = quotes.Where(x => x.Source == sourceString);
            }

            if (!String.IsNullOrEmpty(searchCategory))
            {
                quotes = quotes.Where(x => x.Category.ToString() == searchCategory);
            }

            return View(quotes.ToList());
        }

        // GET: Quotes/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quote quote = db.Quotes.Find(id);
            DetailsQuoteViewModel model = new DetailsQuoteViewModel {
                ID = quote.ID,
                Category = quote.Category,
                Content = quote.Content,
                Source = quote.Source,
                FullName = quote.Author.FirstName + " " + quote.Author.LastName
            };
            if (quote == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: Quotes/Create
        [Authorize(Roles = "Administrator,User")]
        public ActionResult Create()
        {
            var authors = db.Authors.Select(s => new
            {
                AuthorID = s.ID,
                FullName = s.FirstName + " " + s.LastName
            })
            .ToList();
            ViewBag.AuthorID = new SelectList(authors, "AuthorID", "FullName");
            return View();
        }

        // POST: Quotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,AuthorID,Content,Source,Category")] Quote quote)
        {
            if (ModelState.IsValid)
            {
                db.Quotes.Add(quote);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorID = new SelectList(db.Authors, "ID", "FirstName", quote.AuthorID);
            return View(quote);
        }

        // GET: Quotes/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quote quote = db.Quotes.Find(id);
            if (quote == null)
            {
                return HttpNotFound();
            }
            var authors = db.Authors.Select(s => new
            {
                AuthorID = s.ID,
                FullName = s.FirstName + " " + s.LastName
            })
            .ToList();
            ViewBag.AuthorID = new SelectList(authors, "AuthorID", "FullName");

            return View(quote);
        }

        // POST: Quotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,AuthorID,Content,Source,Category")] Quote quote)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quote).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "ID", "FirstName", quote.AuthorID);
            return View(quote);
        }

        // GET: Quotes/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quote quote = db.Quotes.Find(id);
            if (quote == null)
            {
                return HttpNotFound();
            }
            return View(quote);
        }

        // POST: Quotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quote quote = db.Quotes.Find(id);
            db.Quotes.Remove(quote);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
