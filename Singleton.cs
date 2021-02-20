using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab01_EDI.Models;
using Lab01_EDI.ListaDobleArtesanal;

namespace Lab01_EDI
{
    public sealed class Singleton
    {
        private readonly static Singleton instance = new Singleton();
        public ListaDoble<Jugador> listaDoble;
        public List<Jugador> ListaCSharp;
        public bool CSharpListaActiva = false;
        public bool ListaArtesanalActiva = false;

        private Singleton()
        {
            listaDoble = new ListaDoble<Jugador>();
            ListaCSharp = new List<Jugador>();
        }

        public static Singleton Instance
        {
            get
            {
                return instance; 
            }
        }

        public void CambioEstructura()
        {
            if (CSharpListaActiva)
            {
                for (int i = ListaCSharp.Count-1; i >= 0; i--)
                {
                    listaDoble.InsertarInicio(ListaCSharp[i]);
                }
                ListaCSharp.RemoveRange(0, ListaCSharp.Count);
                ActivarArtesanal();
                ActivarCSharp();
            }
            else
            {
                for (int i = 0; i < listaDoble.contador; i++)
                {
                    ListaCSharp.Add(listaDoble.ObtenerValor(i));
                }
                ActivarCSharp();
                ActivarArtesanal();
                while (listaDoble.contador > 0)
                {
                    listaDoble.ExtraerEnPosicion(0);
                }
            }
        }

        private void ActivarCSharp()
        {
            CSharpListaActiva = !CSharpListaActiva;
        }
        private void ActivarArtesanal()
        {
            ListaArtesanalActiva = !ListaArtesanalActiva;
        }
    }
}
