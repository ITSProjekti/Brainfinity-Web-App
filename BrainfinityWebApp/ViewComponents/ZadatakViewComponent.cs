using BrainfinityWebApp.Models;
using BrainfinityWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BrainfinityWebApp.ViewComponents
{
    public class ZadatakViewComponent : ViewComponent
    {
        private readonly IHttpClientFactory _client;

        public ZadatakViewComponent(IHttpClientFactory client)
        {
            _client = client;
        }

        public async Task<IViewComponentResult> InvokeAsync(int grupaId)
        {
            ZadatakIndexViewModel viewModel = new ZadatakIndexViewModel();
            var request = new HttpRequestMessage(HttpMethod.Get, "zadatak/" + grupaId);
            var client = _client.CreateClient("takmicenje");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                viewModel.SviZadaci = await response.Content.ReadAsAsync<IList<Zadatak>>();
            }
            else
            {
                viewModel.SviZadaci = Enumerable.Empty<Zadatak>();
            }

            return View("IndexPartial", viewModel);
        }
    }
}