using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDtransporte.Dominio
{
    class TipoCarga
    {
        public int IdTipo { get; set; }
        public string NombreTipo { get; set; }

        public TipoCarga()
        {
            IdTipo = 0;
            NombreTipo = string.Empty;
        }

        public TipoCarga(int id, string nombre)
        {
            IdTipo = id;
            NombreTipo = nombre;
        }

        public override string ToString()
        {
            return NombreTipo;
        }
    }
}
