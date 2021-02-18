using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab01_EDI
{
    public class Singleton
    {
        private readonly static Singleton instance = new Singleton();
        public ListaDobleArtesanal.ListaDoble<Jugador> listaDoble;

        private Singleton()
        {
            listaDoble = new ListaDobleArtesanal.ListaDoble<Jugador>();
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
