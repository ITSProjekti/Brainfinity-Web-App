using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BrainfinityWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;
using BrainfinityWebApp.ViewModels;
using System.Globalization;
using System.Net.Http.Headers;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Collections;

namespace BrainfinityWebApp.Controllers
{
    [Authorize(Roles = "Supervizor")]
    public class TakmicenjeController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHostingEnvironment _environment;

        public TakmicenjeController(IHttpClientFactory client, IHostingEnvironment environment)
        {
            _clientFactory = client;
            _environment = environment;
        }

        [Breadcrumb("Takmičenja", FromAction = ("Index"), FromController = typeof(HomeController))]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Takmicenje> takmicenja = null;

            var request = new HttpRequestMessage(HttpMethod.Get, "takmicenje");
            //request.Headers.Add("Authorization", "Bearer " + HttpContext.User.Claims.SingleOrDefault(c => c.Type == "token").Value);
            var client = _clientFactory.CreateClient("takmicenje");
            var response = await client.SendAsync(request);

            //if (string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            //{
            //    return RedirectToAction("Login", "Account");
            //}

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<IList<Takmicenje>>();
                takmicenja = result;
            }
            else
            {
                takmicenja = Enumerable.Empty<Takmicenje>();
                ModelState.AddModelError(string.Empty, "Greska");
            }

            NovoTakmicenjeSvaTakmicenja viewModel = new NovoTakmicenjeSvaTakmicenja()
            {
                SvaTakmicenja = takmicenja,
            };

            if (TempData["noviGreska"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["noviGreska"].ToString());
            }

            return View(viewModel);
        }

        //[Breadcrumb("Novo takmicenje", FromAction = "Index", FromController = typeof(TakmicenjeController))]
        //public IActionResult Create()
        //{
        //    return View();
        //}

        [HttpPost]
        public async Task<IActionResult> Create(Takmicenje takmicenje)
        {
            var client = _clientFactory.CreateClient("takmicenje");
            //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + HttpContext.User.Claims.SingleOrDefault(c => c.Type == "token").Value);
            var newFileName = string.Empty;

            if (HttpContext.Request.Form.Files != null)
            {
                var fileName = string.Empty;

                var files = HttpContext.Request.Form.Files;

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                        var FileExtension = Path.GetExtension(fileName);

                        newFileName = myUniqueFileName + FileExtension;

                        fileName = Path.Combine(_environment.WebRootPath, "images") + $@"\{newFileName}";

                        takmicenje.Slika = "images/" + newFileName;

                        using (FileStream fs = System.IO.File.Create(fileName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                    }
                }
            }
            takmicenje.StatusId = Status.Nastupajuce;
            var post = await client.PostAsJsonAsync<Takmicenje>("takmicenje", takmicenje);

            if (post.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            TempData["noviGreska"] = post.Content.ReadAsStringAsync().Result;

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "takmicenje/" + id);
            var client = _clientFactory.CreateClient("takmicenje");
            await client.SendAsync(request);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Takmicenje takmicenje, int id)
        {
            var client = _clientFactory.CreateClient("takmicenje");

            //kod vezan za sliku treba da se prebaci u zasebnu funkciju
            var newFileName = string.Empty;

            if (HttpContext.Request.Form.Files != null)
            {
                var fileName = string.Empty;

                var files = HttpContext.Request.Form.Files;

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                        var FileExtension = Path.GetExtension(fileName);

                        newFileName = myUniqueFileName + FileExtension;

                        fileName = Path.Combine(_environment.WebRootPath, "images") + $@"\{newFileName}";
                        takmicenje.Slika = "images/" + newFileName;

                        using (FileStream fs = System.IO.File.Create(fileName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                    }
                }
            }

            var response = await client.PutAsJsonAsync("takmicenje/" + id, takmicenje);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode(500, response.Content.ReadAsStringAsync().Result);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> TakmicenjeTabelaIndex(int takmicenjeId, string takmicenjeNaziv)
        {
            ViewData["naziv"] = takmicenjeNaziv;
            //IEnumerable<GrupaZadataka> listaGrupaZadataka = null;
            //var client = _clientFactory.CreateClient("takmicenje");
            //var request = new HttpRequestMessage(HttpMethod.Get, "grupaZadataka/" + takmicenjeId);
            //request.Headers.Add("Authorization", "Bearer " + HttpContext.User.Claims.SingleOrDefault(c => c.Type == "token").Value);

            //var response = await client.SendAsync(request);

            //if (response.IsSuccessStatusCode)
            //{
            //    var odg = await response.Content.ReadAsAsync<IList<GrupaZadataka>>();
            //    listaGrupaZadataka = odg;
            //}
            //else
            //{
            //    listaGrupaZadataka = Enumerable.Empty<GrupaZadataka>();
            //}

            //TakmicenjeTabelaViewModel viewModel = new TakmicenjeTabelaViewModel()
            //{
            //    GrupeZadataka = listaGrupaZadataka
            //};

            return View("TakmicenjeTabela");
        }
    }
}