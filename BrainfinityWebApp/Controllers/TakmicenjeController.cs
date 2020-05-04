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

namespace BrainfinityWebApp.Controllers
{
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
        public async Task<IActionResult> Create(TakmicenjeViewModel takmicenje)
        {
            var client = _clientFactory.CreateClient("takmicenje");

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
            takmicenje.Status = Status.Nastupajuce;
            var post = await client.PostAsJsonAsync<TakmicenjeViewModel>("takmicenje", takmicenje);

            if (post.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            TempData["noviGreska"] = post.Content.ReadAsStringAsync().Result;

            return RedirectToAction("Index");
        }
            return RedirectToAction("Index");
        }
    }
}