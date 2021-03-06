﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Web.UI.Models;
using Web.UI.Services;

namespace Web.UI.Controllers
{
   // [Route(template: "[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IBookService _IbookService;
        public HomeController(IBookService bookService)
        {
            _IbookService = bookService;
        }

        [HttpGet]
        public async Task<ViewResult> Index()
        {
            return View(model: await _IbookService.GetBooksAsync());
        }


        [HttpGet]
        public async Task<ViewResult> Edit(long id)
        {
            return View(model: await _IbookService.EditAsync(id));
        }

        [HttpGet]
        public ViewResult Save()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(Book book)
        {
            if (ModelState.IsValid)
            {
                if (!(await _IbookService.SaveAsync(book: book)).Contains(value: "Bad Request"))
                {
                    TempData[key: "Message"] = $@"<div class=""alert alert-success alert-dismissable fade show shadow-sm""><button type=""button"" class=""close"" data-dismiss=""alert"">&times;</button>Book saved successfully.</div>";
                }
                else
                {
                    TempData[key: "Message"] = $@"<div class=""alert alert-danger alert-dismissable fade show shadow-sm""><button type=""button"" class=""close"" data-dismiss=""alert"">&times;</button>Book could not saved successfully.</div>";
                }
                return RedirectToAction(actionName: "Index");
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Book book)
        {
            if (ModelState.IsValid)
            {
                if (!(await _IbookService.UpdateAsync(book: book)).Contains(value: "Not Found"))
                {
                    TempData[key: "Message"] = $@"<div class=""alert alert-success alert-dismissable fade show shadow-sm""><button type=""button"" class=""close"" data-dismiss=""alert"">&times;</button>Book updated successfully.</div>";
                }
                else
                {
                    TempData[key: "Message"] = $@"<div class=""alert alert-danger alert-dismissable fade show shadow-sm""><button type=""button"" class=""close"" data-dismiss=""alert"">&times;</button>Book could not updated.</div>";
                }
                return RedirectToAction(actionName: "Index");
            }
            return View(viewName: "Edit", model: book);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<RedirectToActionResult> Delete(long id)
        {
            if (!(await _IbookService.DeleteAsync(id: id)).Contains(value: "Not Found"))
            {
                TempData[key: "Message"] = $@"<div class=""alert alert-success alert-dismissable fade show shadow-sm""><button type=""button"" class=""close"" data-dismiss=""alert"">&times;</button>Book Deleted successfully.</div>";
            }
            else
            {
                TempData[key: "Message"] = $@"<div class=""alert alert-danger alert-dismissable fade show shadow-sm""><button type=""button"" class=""close"" data-dismiss=""alert"">&times;</button>Book could not be Deleted.</div>";
            }
            return RedirectToAction(actionName: "Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
