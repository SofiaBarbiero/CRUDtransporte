using CRUDtransporte.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDtransporte.Datos.Interfaz
{
    interface IDaoCamiones
    {
        int ObtenerProximo();
        List<TipoCarga> ObtenerTipos();
    }
}
