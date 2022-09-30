using CRUDtransporte.Datos.Implementacion;
using CRUDtransporte.Datos.Interfaz;
using CRUDtransporte.Dominio;
using CRUDtransporte.Servicios.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDtransporte.Servicios.Implementacion
{
    class Servicio : IServicio
    {
        private IDaoCamiones dao;

        public Servicio()
        {
            dao = new DaoCamiones();
        }
        public int ObtenerProximo()
        {
            return dao.ObtenerProximo();   
        }

        public List<TipoCarga> ObtenerTipos()
        {
            return dao.ObtenerTipos();
        }
    }
}
