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

        private Singleton()
        {
            listaDoble = new ListaDoble<Jugador>();
        }

        public static Singleton Instance
        {
            get
            {
                return instance; 
            }
        }
    }
}
