using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab01_EDI.Controllers
{
    public class ControladorTablas : Controller
    {

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
