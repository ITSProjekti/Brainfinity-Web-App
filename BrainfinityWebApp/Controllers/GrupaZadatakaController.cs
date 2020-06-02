using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BrainfinityWebApp.Models;
using BrainfinityWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
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
            //IEnumerable<GrupaZadatakaViewModel> sveGrupeZadataka = null;
            var viewModel = new NovaGrupaSveGrupeZadataka();

            var request = new HttpRequestMessage(HttpMethod.Get, "grupaZadataka/" + takmicenjeId);
            var client = _client.CreateClient("takmicenje");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                viewModel = await response.Content.ReadAsAsync<NovaGrupaSveGrupeZadataka>();
            }
            else
            {
                viewModel.SveGrupe = Enumerable.Empty<GrupaZadataka>();
                ModelState.AddModelError(string.Empty, "Greska");
            }

            ViewData["TakmicenjeNaziv"] = viewModel.NazivTakmicenja;
            //ViewData["TakmicenjeId"] = takmicenjeId;
            return View(viewModel);
        }

        public IActionResult GetRazredi(int nivoSkoleId, string sviRazredi)
        {
            //IEnumerable<GrupaZadataka> sveGrupe = JsonConvert.DeserializeObject<IEnumerable<GrupaZadataka>>(sveGrupeZadataka);

            IEnumerable<Razred> sviRazrediLista = JsonConvert.DeserializeObject<IEnumerable<Razred>>(sviRazredi);
            List<Razred> razredi = new List<Razred>();
            razredi = sviRazrediLista.Where(r => r.NivoSkoleId == nivoSkoleId).ToList();
            //foreach (var grupa in sveGrupe)
            //{
            //    foreach (var razred in razredi)
            //    {
            //        if (grupa.RazredId == razred.Id)
            //        {
            //            razredi.Remove(razred);
            //        }
            //    }
            //}

            return Json(new SelectList(razredi, "Id", "RazredNaziv"));
        }

        public async Task<IActionResult> CreateGrupaZadataka(GrupaZadataka grupaZadataka)
        {
            var client = _client.CreateClient("takmicenje");

            var post = await client.PostAsJsonAsync<GrupaZadataka>("grupaZadataka", grupaZadataka);

            return RedirectToAction("Index", new { takmicenjeId = grupaZadataka.TakmicenjeId });
        }
        }
    }
}