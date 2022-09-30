using CRUDtransporte.Datos.Interfaz;
using CRUDtransporte.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUDtransporte.Datos.Implementacion
{
    class DaoCamiones : IDaoCamiones
    {
        public int ObtenerProximo()
        {
            string sp = "sp_proximo";
            string pOut = "@next";
            return Helper.ObtenerInstancia().ObtenerProximo(sp, pOut);
        }

        public List<TipoCarga> ObtenerTipos()
        {
            List<TipoCarga> lst = new List<TipoCarga>();
            string sp = "sp_cargar_tipos";
            DataTable table = Helper.ObtenerInstancia().CargarCombo(sp);
            foreach(DataRow dr in table.Rows)
            {
                int id = Convert.ToInt32(dr["idTipo"].ToString());
                string nombre = dr["nombreTipo"].ToString();
                TipoCarga t = new TipoCarga(id, nombre);
                lst.Add(t);
            }
            return lst;
        }
    }
}
