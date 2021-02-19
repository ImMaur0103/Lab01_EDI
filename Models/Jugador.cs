using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab01_EDI.Models
{
    public class Jugador
    {
        [Index(0)]
        public string Nombre { get; set; } = "";

        [Index(1)]
        public string Apellido { get; set; } = "";

        [Index(2)]
        public string Posicion { get; set; } = "";

        [Index(3)]
        public int Salario { get; set; } = 0;

        [Index(4)]
        public string Club { get; set; } = "";
    }
}
