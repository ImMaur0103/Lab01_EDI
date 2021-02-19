using System;

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web; 
using Microsoft.Extensions.Logging;
using Lab01_EDI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using CsvHelper;
using System.Globalization;

namespace Lab01_EDI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string option)
        {
            switch (option)
            {
                case "ListaArtesanal":
                    // redirige hacia la vista upload en donde los elementos que se suban se deben guardar dentro de la lista artesanal.
                    break;
                case "ListaC":
                    break;
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Upload()
        {
            return View();
        }

        public IActionResult Search()
        {
            // Muestra la tabla donde se encuentra el listado de jugadores
            return View(Singleton.Instance.listaDoble);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        [HttpGet]

        public IActionResult Upload(ListaDobleArtesanal.ListaDoble<Jugador> ListaJugador = null)
        {
            ListaJugador = ListaJugador == null ? new ListaDobleArtesanal.ListaDoble<Jugador>() : ListaJugador;
            return View(ListaJugador);
        }




        [HttpPost]
        public  IActionResult Upload(IFormFile archivo, [FromServices] IHostingEnvironment hostingEnvironment)
        {
            string nombreArchivo = $"{hostingEnvironment.WebRootPath} { archivo.FileName}";
            using (FileStream fileStream = System.IO.File.Create(nombreArchivo))  
            {
                archivo.CopyTo(fileStream);
                fileStream.Flush();
            }

            //ListaDobleArtesanal.ListaDoble<Jugador> ListaJugadores = new ListaDobleArtesanal.ListaDoble<Jugador>();

            var path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + nombreArchivo; // guarda archivo
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var jugador = csv.GetRecord<Jugador>();
                    Singleton.Instance.listaDoble.InsertarInicio(jugador);   
                }
            }

            path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\FilesTo"}";
            using (var write = new StreamWriter(path + "\\Archivo.csv"))
            using(var csv = new CsvWriter(write, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords((System.Collections.IEnumerable)Singleton.Instance.listaDoble);
            }

            return Upload(Singleton.Instance.listaDoble);
        }
    }
}
