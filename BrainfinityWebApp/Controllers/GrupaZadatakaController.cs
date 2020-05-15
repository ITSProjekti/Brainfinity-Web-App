using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BrainfinityWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;
using SmartBreadcrumbs.Nodes;

namespace BrainfinityWebApp.Controllers
{
    public class GrupaZadatakaController : Controller
    {
        private readonly IHttpClientFactory _client;

        public GrupaZadatakaController(IHttpClientFactory client)
        {
            _client = client;
        }

        public async Task<IActionResult> Index(int takmicenjeId)
        {
            IEnumerable<GrupaZadatakaViewModel> sveGrupeZadataka = null;

            var request = new HttpRequestMessage(HttpMethod.Get, "grupaZadataka/" + takmicenjeId);
            var client = _client.CreateClient("takmicenje");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                sveGrupeZadataka = await response.Content.ReadAsAsync<IList<GrupaZadatakaViewModel>>();
            }
            else
            {
                sveGrupeZadataka = Enumerable.Empty<GrupaZadatakaViewModel>();
                ModelState.AddModelError(string.Empty, "Greska");
            }

            return View(sveGrupeZadataka);
        }
    }
}