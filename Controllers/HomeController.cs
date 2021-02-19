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
using Lab01_EDI.ListaDobleArtesanal;


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
            return View(Singleton.Instance.listaDoble);
        }

        public IActionResult Search()
         {
            // Muestra la tabla donde se encuentra el listado de jugadores
            return View();
        }

        public IActionResult Add(string nombre, string apellido, string posicion, int salario, string club)
        {
            if((nombre != null) && (apellido!=null) && (posicion!=null) && (salario != 0) && (club != null))
            {
                Jugador nuevoJugador = new Jugador();
                nuevoJugador.Nombre = nombre;
                nuevoJugador.Apellido = apellido;
                nuevoJugador.Posicion = posicion;
                nuevoJugador.Salario = salario;
                nuevoJugador.Club = club;
                ViewBag.Mensaje = "Agregado";
                Singleton.Instance.listaDoble.InsertarInicio(nuevoJugador);
            
            }
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]

        public IActionResult Upload(ListaDoble<Jugador> ListaJugador = null)
        {
            ListaJugador = ListaJugador == null ? new  ListaDoble<Jugador>() : ListaJugador;
            Singleton.Instance.listaDoble = ListaJugador;
            return View(Singleton.Instance.listaDoble);
        }

         
        [HttpPost]
        public  IActionResult Upload(IFormFile file, [FromServices] IHostingEnvironment hostingEnvironment)
        {
            string filename = $"{hostingEnvironment.WebRootPath}\\files\\{file.FileName}";
            using (FileStream fileStream = System.IO.File.Create(filename))  
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }

            var jugadores = this.GetJugadoresList(file.FileName);
            Singleton.Instance.listaDoble = jugadores;
            return Upload(Singleton.Instance.listaDoble);
        }

        private ListaDoble<Jugador> GetJugadoresList(string filename) 
        {
            ListaDoble<Jugador> jugadores = new ListaDoble<Jugador>();
         
            var path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + filename; // guarda archivo
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var jugador = csv.GetRecord<Jugador>();
                    jugadores.InsertarInicio(jugador);
                }
            }

            path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\FilesTo"}";
            using (var write = new StreamWriter(path + "\\Archivo.csv"))
            using (var csv = new CsvWriter(write, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(jugadores);
            }

            return jugadores; 
        }
    }
}
