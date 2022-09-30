using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDtransporte.Datos
{
    class Helper
    {
        private static Helper instancia;
        private SqlConnection cnn;

        public Helper()
        {
            cnn = new SqlConnection(Properties.Resources.cnnString);
        }

        public static Helper ObtenerInstancia()
        {
            if(instancia == null)
            {
                instancia = new Helper();
            }
            return instancia;
        }
    }
}
