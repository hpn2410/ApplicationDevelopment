using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FPTBookStore.Models;

namespace FPTBookStore.Controllers
{
    [Authorize(Roles = "Store Owner")]
    public class BooksController : BaseController
    {
        // GET: Books
        public ActionResult Index(string search)
        {
            var books = from b in db.Books
                        select b;
            if (!string.IsNullOrEmpty(search))
            {
                ViewBag.Search = search;
                books = books.Where(b => b.Name.Contains(search));
            }

            return View(books
                .Include(b => b.Category)
                .OrderByDescending(b => b.CreatedDateTime)
                .ToList());
        }

        // GET: Books/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Author,Description,CategoryId,CoverUrl,Price,StockedQuantity,CreatedDateTime,UpdatedDateTime")] Book book, HttpPostedFileBase coverImg)
        {
            if (ModelState.IsValid)
            {
                // automatically update created and updated datetime
                book.CreatedDateTime = DateTime.Now;
                book.UpdatedDateTime = DateTime.Now;

                // save cover image to server and update cover url
                book.CoverUrl = "book0.jpg"; // default book cover
                if (coverImg != null)
                {
                    var fileName = Guid.NewGuid().ToString() + ".jpg";
                    var sysPath = Path.Combine(Server.MapPath("~/Content/Images/Books"), fileName);

                    coverImg.SaveAs(sysPath);
                    book.CoverUrl = fileName;
                }

                db.Books.Add(book);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", book.CategoryId);
            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", book.CategoryId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Author,Description,CategoryId,CoverUrl,Price,StockedQuantity,CreatedDateTime,UpdatedDateTime")] Book book, HttpPostedFileBase coverImg)
        {
            // automatically update updated datetime
            book.UpdatedDateTime = DateTime.Now;

            // save cover image to server and update cover url
            if (coverImg != null)
            {
                var fileName = book.CoverUrl;
                // generate new file name if book0.jpg (no cover image)
                if (book.CoverUrl == "book0.jpg")
                    fileName = Guid.NewGuid().ToString() + ".jpg";

                var sysPath = Path.Combine(Server.MapPath("~/Content/Images/Books"), fileName);

                coverImg.SaveAs(sysPath);
                book.CoverUrl = fileName;
            }

            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", book.CategoryId);
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            if (book.CoverUrl != "book0.jpg")
            {
                var path = Path.Combine(Server.MapPath("~/Content/Images/Books"), book.CoverUrl);
                System.IO.File.Delete(path);
            }

            db.Books.Remove(book);
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
