using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dojodachi.Models;
using Microsoft.AspNetCore.Http;


namespace Dojodachi.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            int? Fullness = HttpContext.Session.GetInt32("fullness");
            int? Happiness = HttpContext.Session.GetInt32("happiness");
            int? Meals = HttpContext.Session.GetInt32("meals");
            int? Energy = HttpContext.Session.GetInt32("energy");
            string Message = HttpContext.Session.GetString("message");
            int? Image = HttpContext.Session.GetInt32("image");
            if (Fullness == null)
            {
                HttpContext.Session.SetInt32("fullness", 20);
            }
            if (Happiness == null)
            {
                HttpContext.Session.SetInt32("happiness", 20);
            }
            if (Meals == null)
            {
                HttpContext.Session.SetInt32("meals", 3);
            }
            if (Energy == null)
            {
                HttpContext.Session.SetInt32("energy", 50);
            }
            if (Fullness >= 100 && Happiness >= 100 && Energy >= 100)
            {
                string Won = "Congratulations, You Won!";
                HttpContext.Session.SetString("message", Won);

            }
            if (Fullness <= 0 || Happiness <= 0)
            {
                string GameOver = "Sorry, your Dojodachi died. GAME OVER!";
                HttpContext.Session.SetString("message", GameOver);

            }
            ViewBag.Fullness = HttpContext.Session.GetInt32("fullness");
            ViewBag.Happiness = HttpContext.Session.GetInt32("happiness");
            ViewBag.Meals = HttpContext.Session.GetInt32("meals");
            ViewBag.Energy = HttpContext.Session.GetInt32("energy");
            ViewBag.Display = HttpContext.Session.GetString("message");
            ViewBag.Image = HttpContext.Session.GetInt32("image");
            return View("Index");
        }

        [HttpGet("feed")]
        public IActionResult Feed()
        {
            int? Meals = HttpContext.Session.GetInt32("meals");
            if (Meals <= 0)
            {
                string message1 = "You have no meals left to feed your pet";
                HttpContext.Session.SetString("message", message1);
                HttpContext.Session.SetInt32("image", 1);
                return RedirectToAction("Index");
            }
            Meals = Meals - 1;
            HttpContext.Session.SetInt32("meals", (int)Meals);
            Random rand = new Random();
            int? chance = rand.Next(1, 5);
            if (chance == 1)
            {
                string noFeed = "You fed your Dojodachi, she did not like the food";
                HttpContext.Session.SetString("message", noFeed);
                HttpContext.Session.SetInt32("image", 2);
            }
            if (chance >= 2)
            {
                int? randFull = rand.Next(5, 11);
                int? Fullness = HttpContext.Session.GetInt32("fullness");
                Fullness = Fullness + randFull;
                HttpContext.Session.SetInt32("fullness", (int)Fullness);
                string message = "You fed your Dojodachi, Fullness +" + @randFull;
                HttpContext.Session.SetString("message", message);
                HttpContext.Session.SetInt32("image", 2);
            }
            return RedirectToAction("Index");
        }

        [HttpGet("play")]
        public IActionResult Play()
        {
            int? Energy = HttpContext.Session.GetInt32("energy");
            Energy = Energy - 5;
            HttpContext.Session.SetInt32("energy", (int)Energy);
            Random rand = new Random();
            int? chance = rand.Next(1, 5);
            if (chance == 1)
            {
                string noPlay = "You played with your Dojodachi, she did not like it";
                HttpContext.Session.SetString("message", noPlay);
                HttpContext.Session.SetInt32("image", 3);

            }
            if (chance >= 2)
            {
                int? randHappy = rand.Next(5, 11);
                int? Happiness = HttpContext.Session.GetInt32("happiness");
                Happiness = Happiness + randHappy;
                HttpContext.Session.SetInt32("happiness", (int)Happiness);
                string message = "You played with your Dojodachi, Happiness +" + @randHappy;
                HttpContext.Session.SetString("message", message);
                HttpContext.Session.SetInt32("image", 3);

            }
            return RedirectToAction("Index");
        }

        [HttpGet("work")]
        public IActionResult Work()
        {
            int? Energy = HttpContext.Session.GetInt32("energy");
            Energy = Energy - 5;
            Random rand = new Random();
            int? randMeal = rand.Next(1, 4);
            int? Meals = HttpContext.Session.GetInt32("meals");
            Meals = Meals + randMeal;
            HttpContext.Session.SetInt32("meals", (int)Meals);
            string message2 = "You worked today. You earned " + @randMeal + " meal(s) for your Dojodachi";
            HttpContext.Session.SetString("message", message2);
            HttpContext.Session.SetInt32("image", 4);
            return RedirectToAction("Index");
        }

        [HttpGet("sleep")]
        public IActionResult Sleep()
        {
            int? Energy = HttpContext.Session.GetInt32("energy");
            Energy = Energy + 15;
            int? Fullness = HttpContext.Session.GetInt32("fullness");
            Fullness = Fullness - 5;
            int? Happiness = HttpContext.Session.GetInt32("happiness");
            Happiness = Happiness - 5;
            HttpContext.Session.SetInt32("energy", (int)Energy);
            HttpContext.Session.SetInt32("fullness", (int)Fullness);
            HttpContext.Session.SetInt32("happiness", (int)Happiness);
            string message3 = "Your Dojodachi went to sleep. Energy +15";
            HttpContext.Session.SetString("message", message3);
            HttpContext.Session.SetInt32("image", 5);
            return RedirectToAction("Index");
        }
        [HttpGet("reset")]
        public IActionResult Reset()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.SetInt32("fullness", 20);
            HttpContext.Session.SetInt32("happiness", 20);
            HttpContext.Session.SetInt32("meals", 3);
            HttpContext.Session.SetInt32("energy", 50);
            return RedirectToAction("Index");
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
