using ElasticSearchDemo.Models;
using ElasticSearchDemo.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearchDemo.Controllers
{
    public class BooksController : Controller
    {
        public IBookService BookService { get; }

        public BooksController(IBookService bookService)
        {
            BookService = bookService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(InputBook book)
        {
            await BookService.Create(book);
            return RedirectToAction("Index", "Home");
        }
    }
}
