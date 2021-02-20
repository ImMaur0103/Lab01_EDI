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
        static ListaDoble<Jugador> ListaAux = new ListaDoble<Jugador>();

        public void BuscarElemento(string OBuscar, ListaDoble<Jugador> JugadorLista)
        {
            int contador = JugadorLista.contador;

            for (int i = 0; i < contador; i++)
            {
                Jugador jugador = new Jugador();
                jugador = JugadorLista.ObtenerValor(i);
                

                if (jugador.Nombre == OBuscar)
                {
                    ListaAux.InsertarInicio(jugador);
                }
            }
        }
    }

   
}
