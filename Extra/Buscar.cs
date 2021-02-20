using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab01_EDI.Models;
using Lab01_EDI.ListaDobleArtesanal;

namespace Lab01_EDI.Extra
{

    public class Buscar
    {
        /*public delegate void Busqueda(IEnumerable<Jugador> jugador, string buscar, int contador);

        ListaDoble<Jugador> ListaAux = new ListaDoble<Jugador>();

        ListaDoble<Jugador> BusquedaNombre = new Busqueda(BuscarNombre);

        public void BuscarNombre(IEnumerable<Jugador> jugador, string nombre, int contador)
        {
            nombre = nombre.ToLower();

            for (int i = 0; i < contador; i++)
            {
                if(jugador.ElementAt(i).Nombre == nombre)
                {
                    ListaAux.InsertarInicio(jugador.ElementAt(i));
                }
            }
        }

        public void BuscarApellido(IEnumerable<Jugador> jugador, string apellido, int contador)
        {
            apellido.ToLower();

            for (int i = 0; i < contador; i++)
            {
                if (jugador.ElementAt(i).Apellido == apellido)
                {
                    ListaAux.InsertarInicio(jugador.ElementAt(i));
                }
            }
        }

        public void BuscarPosicion(IEnumerable<Jugador> jugador, string posicion, int contador)
        {
            posicion.ToLower();

            for (int i = 0; i < contador; i++)
            {
                if (jugador.ElementAt(i).Posicion == posicion)
                {
                    ListaAux.InsertarInicio(jugador.ElementAt(i));
                }
            }
        }

        public void BuscarClub(IEnumerable<Jugador> jugador, string Club, int contador)
        {
            Club.ToLower();

            for (int i = 0; i < contador; i++)
            {
                if (jugador.ElementAt(i).Club == Club)
                {
                    ListaAux.InsertarInicio(jugador.ElementAt(i));
                }
            }
        }

        public void BuscarSalario(IEnumerable<Jugador> jugador, int salario, string opcion, int contador)
        {
            switch (opcion)
            {
                case "menor":
                    for (int i = 0; i < contador; i++)
                    {
                        if (jugador.ElementAt(i).Salario < salario)
                        {
                            ListaAux.InsertarInicio(jugador.ElementAt(i));
                        }
                    }
                    break;
                case "mayor":
                    for (int i = 0; i < contador; i++)
                    {
                        if (jugador.ElementAt(i).Salario > salario)
                        {
                            ListaAux.InsertarInicio(jugador.ElementAt(i));
                        }
                    }
                    break;
                case "igual":
                    for (int i = 0; i < contador; i++)
                    {
                        if (jugador.ElementAt(i).Salario == salario)
                        {
                            ListaAux.InsertarInicio(jugador.ElementAt(i));
                        }
                    }
                    break;
            }
        }
    }*/
}
