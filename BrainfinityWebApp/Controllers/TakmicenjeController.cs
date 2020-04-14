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
        private readonly IHttpClientFactory _clientFactory;

        public TakmicenjeController(IHttpClientFactory client)
        {
            _clientFactory = client;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<TakmicenjeViewModel> takmicenja = null;

            var request = new HttpRequestMessage(HttpMethod.Get, "takmicenje");
            var client = _clientFactory.CreateClient("takmicenje");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<IList<TakmicenjeViewModel>>();
                takmicenja = result;
            }
            else
            {
                takmicenja = Enumerable.Empty<TakmicenjeViewModel>();
                ModelState.AddModelError(string.Empty, "Greska");
            }

            return View(takmicenja);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TakmicenjeViewModel takmicenje)
        {
            var client = _clientFactory.CreateClient("takmicenje");
            var post = await client.PostAsJsonAsync<TakmicenjeViewModel>("takmicenje", takmicenje);

            if (post.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Greska");

            return View(takmicenje);
        }
    }
}