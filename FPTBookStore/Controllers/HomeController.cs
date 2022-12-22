using FPTBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPTBookStore.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index(string search)
        {
            var books = from b in db.Books
                        select b;
            if (!string.IsNullOrEmpty(search))
            {
                ViewBag.Search = search;
                books = books.Where(b => b.Name.Contains(search));
            }

            return View(books.OrderByDescending(b => b.CreatedDateTime).ToList());
        }

        public ActionResult Help()
        {
            return View();
        }
    }
}