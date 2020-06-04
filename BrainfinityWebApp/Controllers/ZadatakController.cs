using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BrainfinityWebApp.Models;
using BrainfinityWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BrainfinityWebApp.Controllers
{
    public class ZadatakController : Controller
    {
        private readonly IHttpClientFactory _client;

        public ZadatakController(IHttpClientFactory client)
        {
            _client = client;
        }

        public async Task<IActionResult> Index(int grupaId)
        {
            var client = _client.CreateClient("takmicenje");
            var response = client.GetAsync("zadatak/" + grupaId);

            var viewModel = new ZadatakIndexViewModel();

            if (response.IsCompletedSuccessfully)
            {
                viewModel.SviZadaci = await response.Result.Content.ReadAsAsync<IEnumerable<Zadatak>>();
            }
            else
            {
                viewModel.SviZadaci = Enumerable.Empty<Zadatak>();
            }

            return View("IndexPartial", viewModel);
        }
    }
}