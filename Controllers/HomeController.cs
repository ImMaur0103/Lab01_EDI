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

        public IActionResult Upload(string option)
        {
            return View();
        }

        public IActionResult Add(string nombre, string apellido, string posicion, int salario, string club, string option)
        {
            if((nombre != null) && (apellido!=null) && (posicion!=null) && (salario != 0) && (club != null))
            {
                Jugador nuevoJugador = new Jugador();
                nuevoJugador.Nombre = nombre;
                nuevoJugador.Apellido = apellido;
                nuevoJugador.Posicion = posicion;
                nuevoJugador.Salario = salario;
                nuevoJugador.Club = club;
                Singleton.Instance.listaDoble.InsertarInicio(nuevoJugador);
                ViewBag.Mensaje = "Agregado";
            }
            return View();
        }

        public IActionResult Search()
        {
            return View(Singleton.Instance.listaDoble);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]

        public IActionResult Upload(ListaDoble<Jugador> ListaJugador = null)
        {
            if(ListaJugador.inicio != null)
                Singleton.Instance.listaDoble = ListaJugador; //es funcional            
            return View(Singleton.Instance.listaDoble); // es funcional 
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
            //Singleton.Instance.listaDoble = jugadores;
            //return Upload(Singleton.Instance.listaDoble);
            return Upload(jugadores);
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


        public IActionResult EliminarJugador(string Nombre, string Apellido, string Posicion, string Salario, string Club)
        {
            for (int i = 0; i < Singleton.Instance.listaDoble.contador; i++)
            {
                if (Nombre == Convert.ToString(Singleton.Instance.listaDoble.ObtenerValor(i).Nombre) && Apellido == Convert.ToString(Singleton.Instance.listaDoble.ObtenerValor(i).Apellido) && Posicion == Convert.ToString(Singleton.Instance.listaDoble.ObtenerValor(i).Posicion) && Salario == Convert.ToString(Singleton.Instance.listaDoble.ObtenerValor(i).Salario) && Club == Convert.ToString(Singleton.Instance.listaDoble.ObtenerValor(i).Club))
                {
                    Singleton.Instance.listaDoble.ExtraerEnPosicion(i);
                    break;
                }
            }

            return View("Privacy");
        }

        public int Editar(string Nombre, string Apellido, string Posicion, string Salario, string Club)
        {
            for (int i = 0; i < Singleton.Instance.listaDoble.contador; i++)
            {
                if (Nombre == Convert.ToString(Singleton.Instance.listaDoble.ObtenerValor(i).Nombre) && Apellido == Convert.ToString(Singleton.Instance.listaDoble.ObtenerValor(i).Apellido) && Posicion == Convert.ToString(Singleton.Instance.listaDoble.ObtenerValor(i).Posicion) && Salario == Convert.ToString(Singleton.Instance.listaDoble.ObtenerValor(i).Salario) && Club == Convert.ToString(Singleton.Instance.listaDoble.ObtenerValor(i).Club))
                {
                    Singleton.Instance.listaDoble.ExtraerEnPosicion(i);
                }
            }

            return 0;
        }
    }
}
