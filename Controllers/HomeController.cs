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
                if (Singleton.Instance.CSharpListaActiva)
                    Singleton.Instance.ListaCSharp.Add(nuevoJugador);
                else
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
            {
                if (Singleton.Instance.ListaArtesanalActiva)
                    Singleton.Instance.listaDoble = ListaJugador; //es funcional   
                else
                    Singleton.Instance.ListaCSharp = ListaJugador.ToList();
            }         
            return View(Singleton.Instance.listaDoble); // es funcional 
        }
    
        [HttpPost]
        public  IActionResult Upload(IFormFile file, [FromServices] IHostingEnvironment hostingEnvironment)
        {
            try
            {
                string filename = $"{hostingEnvironment.WebRootPath}\\files\\{file.FileName}";
                using (FileStream fileStream = System.IO.File.Create(filename))
                {
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                }

            }
            catch (Exception)
            {

                throw;
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
            if (Singleton.Instance.ListaArtesanalActiva)
            {
                for (int i = 0; i < Singleton.Instance.listaDoble.contador; i++)
                {
                    if (Nombre == Convert.ToString(Singleton.Instance.listaDoble.ObtenerValor(i).Nombre) && Apellido == Convert.ToString(Singleton.Instance.listaDoble.ObtenerValor(i).Apellido) && Posicion == Convert.ToString(Singleton.Instance.listaDoble.ObtenerValor(i).Posicion) && Salario == Convert.ToString(Singleton.Instance.listaDoble.ObtenerValor(i).Salario) && Club == Convert.ToString(Singleton.Instance.listaDoble.ObtenerValor(i).Club))
                    {
                        Singleton.Instance.listaDoble.ExtraerEnPosicion(i);
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < Singleton.Instance.ListaCSharp.Count; i++)
                {
                    if (Nombre == Convert.ToString(Singleton.Instance.ListaCSharp[i].Nombre) && Apellido == Convert.ToString(Singleton.Instance.ListaCSharp[i].Apellido) && Posicion == Convert.ToString(Singleton.Instance.ListaCSharp[i].Posicion) && Salario == Convert.ToString(Singleton.Instance.ListaCSharp[i].Salario) && Club == Convert.ToString(Singleton.Instance.ListaCSharp[i].Club))
                    {
                        Singleton.Instance.listaDoble.ExtraerEnPosicion(i);
                        break;
                    }
                }
            }
            return View("Privacy");
        }

        public IActionResult Editar(string Nombre, string Apellido, string Posicion, string Salario, string Club)
        {
            if (Singleton.Instance.ListaArtesanalActiva)
            {
                for (int i = 0; i < Singleton.Instance.listaDoble.contador; i++)
                {
                    if (Nombre == Convert.ToString(Singleton.Instance.listaDoble.ObtenerValor(i).Nombre) && Apellido == Convert.ToString(Singleton.Instance.listaDoble.ObtenerValor(i).Apellido) && Posicion == Convert.ToString(Singleton.Instance.listaDoble.ObtenerValor(i).Posicion) && Salario == Convert.ToString(Singleton.Instance.listaDoble.ObtenerValor(i).Salario) && Club == Convert.ToString(Singleton.Instance.listaDoble.ObtenerValor(i).Club))
                    {
                        Singleton.Instance.listaDoble.PosEditar = i;
                        Singleton.Instance.listaDoble.Editar = true;
                        return View("Privacy");
                    }
                }
            }
            else
            {
                for (int i = 0; i < Singleton.Instance.ListaCSharp.Count; i++)
                {
                    if (Nombre == Convert.ToString(Singleton.Instance.ListaCSharp[i].Nombre) && Apellido == Convert.ToString(Singleton.Instance.ListaCSharp[i].Apellido) && Posicion == Convert.ToString(Singleton.Instance.ListaCSharp[i].Posicion) && Salario == Convert.ToString(Singleton.Instance.ListaCSharp[i].Salario) && Club == Convert.ToString(Singleton.Instance.ListaCSharp[i].Club))
                    {
                        Singleton.Instance.listaDoble.PosEditar = i;
                        Singleton.Instance.listaDoble.Editar = true;
                        return View("Privacy");
                    }
                }
            }

            return View("Privacy");
        }
        [HttpPost]
        public IActionResult GuardarEdicion(string nombre, string apellido, string posicion, int salario, string club)
        {
            Singleton.Instance.listaDoble.Editar = false;
            Jugador nuevoJugador = new Jugador();
            try
            {
                if (Singleton.Instance.ListaArtesanalActiva)
                {
                    if (nombre == null)
                        nuevoJugador.Nombre = Convert.ToString(Singleton.Instance.listaDoble.ObtenerValor(Singleton.Instance.listaDoble.PosEditar).Nombre);
                    else
                        nuevoJugador.Nombre = nombre;


                    if (apellido == null)
                        nuevoJugador.Apellido = Convert.ToString(Singleton.Instance.listaDoble.ObtenerValor(Singleton.Instance.listaDoble.PosEditar).Apellido);
                    else
                        nuevoJugador.Apellido = apellido;


                    if (posicion == null)
                        nuevoJugador.Posicion = Convert.ToString(Singleton.Instance.listaDoble.ObtenerValor(Singleton.Instance.listaDoble.PosEditar).Posicion);
                    else
                        nuevoJugador.Posicion = posicion;


                    if (salario == 0)
                        nuevoJugador.Salario = Convert.ToInt16(Singleton.Instance.listaDoble.ObtenerValor(Singleton.Instance.listaDoble.PosEditar).Salario);
                    else
                        nuevoJugador.Salario = salario;


                    if (club == null)
                        nuevoJugador.Club = Convert.ToString(Singleton.Instance.listaDoble.ObtenerValor(Singleton.Instance.listaDoble.PosEditar).Club);
                    else
                        nuevoJugador.Club = club;
                }
                else
                {
                    if (nombre == null)
                        nuevoJugador.Nombre = Convert.ToString(Singleton.Instance.ListaCSharp[Singleton.Instance.listaDoble.PosEditar].Nombre);
                    else
                        nuevoJugador.Nombre = nombre;


                    if (apellido == null)
                        nuevoJugador.Apellido = Convert.ToString(Singleton.Instance.ListaCSharp[Singleton.Instance.listaDoble.PosEditar].Apellido);
                    else
                        nuevoJugador.Apellido = apellido;


                    if (posicion == null)
                        nuevoJugador.Posicion = Convert.ToString(Singleton.Instance.ListaCSharp[Singleton.Instance.listaDoble.PosEditar].Posicion);
                    else
                        nuevoJugador.Posicion = posicion;


                    if (salario == 0)
                        nuevoJugador.Salario = Convert.ToInt16(Singleton.Instance.ListaCSharp[Singleton.Instance.listaDoble.PosEditar].Salario);
                    else
                        nuevoJugador.Salario = salario;


                    if (club == null)
                        nuevoJugador.Club = Convert.ToString(Singleton.Instance.ListaCSharp[Singleton.Instance.listaDoble.PosEditar].Club);
                    else
                        nuevoJugador.Club = club;
                }
            }
            catch (Exception)
            {
                throw;
            }
            if (Singleton.Instance.ListaArtesanalActiva)
                Singleton.Instance.listaDoble.CambiarEnPosicion(Singleton.Instance.listaDoble.PosEditar, nuevoJugador);
            else
                Singleton.Instance.ListaCSharp[Singleton.Instance.listaDoble.PosEditar] = nuevoJugador;
            return View("Privacy");
        }

        public IActionResult CambioEstructuraArtesanal()
        {
            if (Singleton.Instance.CSharpListaActiva && Singleton.Instance.ListaCSharp.Count != 0)
            {
                Singleton.Instance.CambioEstructura();
            }
            else
            {
                Singleton.Instance.ListaArtesanalActiva = true;
                Singleton.Instance.CSharpListaActiva = false;
            }
            ViewBag.Mensaje = "Actualizado";
            return View("Index");
        }

        public IActionResult CambioEstructuraCsharp()
        {
            if (Singleton.Instance.ListaArtesanalActiva && Singleton.Instance.listaDoble.contador != 0)
            {
                Singleton.Instance.CambioEstructura();
            }
            else
            {
                Singleton.Instance.CSharpListaActiva = true;
                Singleton.Instance.ListaArtesanalActiva = false;
            }
            ViewBag.Mensaje = "Actualizado";
            return View("Index");
        }
    }
}
