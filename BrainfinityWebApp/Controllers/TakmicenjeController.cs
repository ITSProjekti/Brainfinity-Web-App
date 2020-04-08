using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BrainfinityWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BrainfinityWebApp.Controllers
{
    public class TakmicenjeController : Controller
    {
        public IActionResult Index()
        {
            IEnumerable<TakmicenjeViewModel> takmicenja = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5001/api/");

                var response = client.GetAsync("takmicenje");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadAsAsync<IList<TakmicenjeViewModel>>();
                    read.Wait();

                    takmicenja = read.Result;
                }
                else
                {
                    takmicenja = Enumerable.Empty<TakmicenjeViewModel>();

                    ModelState.AddModelError(string.Empty, "Greska");
                }
            }

            return View(takmicenja);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TakmicenjeViewModel takmicenje)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5001/api/");

                var postTask = client.PostAsJsonAsync<TakmicenjeViewModel>("takmicenje", takmicenje);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Greska");

            return View(takmicenje);
        }
    }
}