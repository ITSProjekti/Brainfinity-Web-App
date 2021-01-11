using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BrainfinityWebApp.Models;
using BrainfinityWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrainfinityWebApp.Controllers
{
    [Authorize(Roles = "Supervizor")]
    public class ZadatakController : Controller
    {
        private readonly IHttpClientFactory _client;

        public ZadatakController(IHttpClientFactory client)
        {
            _client = client;
        }

        public async Task<IActionResult> CreateZadatak(Zadatak zadatak, int takmicenjeId)
        {
            var client = _client.CreateClient("takmicenje");
            var request = await client.PostAsJsonAsync<Zadatak>("zadatak", zadatak);

            return RedirectToAction("Index", "GrupaZadataka", new { takmicenjeId });
        }

        public async Task<IActionResult> DeleteZadatak(int zadatakId, int takmicenjeId)
        {
            var client = _client.CreateClient("takmicenje");
            var request = await client.DeleteAsync("zadatak/" + zadatakId);

            return RedirectToAction("Index", "GrupaZadataka", new { takmicenjeId });
        }

        public async Task<IActionResult> EditZadatak(Zadatak zadatak, int takmicenjeId, int zadatakId)
        {
            var client = _client.CreateClient("takmicenje");
            var request = await client.PutAsJsonAsync<Zadatak>("zadatak/" + zadatakId, zadatak);

            return RedirectToAction("Index", "GrupaZadataka", new { takmicenjeId });
        }
    }
}