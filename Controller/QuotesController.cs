using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuotingDojoC.Models;
using System.Collections.Generic;
using DbConnection;

namespace QuotingDojoC.Controllers
{
    public class QuotesController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/quotes")]
        public IActionResult QuotesGet()
        {
            // grabs everything we got from the quoting_dojo_base 
            List<Dictionary<string, object>> AllQuotes = DbConnector.Query("SELECT * FROM users");
            // puts it in viewbag
            ViewBag.Quotes = AllQuotes;
            return View();
        }

        [HttpPost("/quotes")]
        public IActionResult QuotesPost(Quote newQuote)
        {
            // Post the data in newQuote to the DB
            string query = $"INSERT INTO users (Name, UserQuote) VALUES ('{newQuote.Name}', '{newQuote.UserQuote}')";
            DbConnector.Execute(query);
            // return the /quotes page
            return RedirectToAction("QuotesGet");
        }

    }
}