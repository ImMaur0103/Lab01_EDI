using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab01_EDI.ListaDobleArtesanal
{
    public class Nodo <T>
    {
        private T valor { get; set; }

        private Nodo<T> siguiente;
        private Nodo<T> anterior;

        public T Valor
        {

            get { return valor; }
            set { valor = value; }
        }

        public Nodo<T> Siguiente
        {
            get { return siguiente; }
            set { siguiente = value; }
        }

        public Nodo<T> Anterior
        {
            get { return anterior; }
            set { anterior = value; }
        }
    }
}
