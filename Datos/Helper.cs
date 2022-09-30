using CRUDtransporte.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
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

        public int ObtenerProximo(string nombreSp, string nombrePOut)
        {
            SqlCommand cmdProx = new SqlCommand();
            cnn.Open();
            cmdProx.Connection = cnn;
            cmdProx.CommandType = CommandType.StoredProcedure;
            cmdProx.CommandText = nombreSp;
            SqlParameter pOut = new SqlParameter();
            pOut.ParameterName = nombrePOut;
            pOut.DbType = DbType.Int32;
            pOut.Direction = ParameterDirection.Output;
            cmdProx.Parameters.Add(pOut);
            cmdProx.ExecuteNonQuery();
            cnn.Close();
            return (int)pOut.Value;
        }

        public DataTable CargarCombo(string sp)
        {
            DataTable table = new DataTable();
            SqlCommand cmdCombo = new SqlCommand();
            cnn.Open();
            cmdCombo.Connection = cnn;
            cmdCombo.CommandType = CommandType.StoredProcedure;
            cmdCombo.CommandText = sp;
            table.Load(cmdCombo.ExecuteReader());
            cnn.Close();
            return table;
        }

        public bool ConfirmarCamion(Camion c)
        {
            bool ok = true;
            SqlTransaction t = null;
            try
            {
                SqlCommand cmdM = new SqlCommand();
                cnn.Open();
                t = cnn.BeginTransaction();
                cmdM.Connection = cnn;
                cmdM.Transaction = t;
                cmdM.CommandType = CommandType.StoredProcedure;
                cmdM.CommandText = "sp_insertar_maestro";
                cmdM.Parameters.AddWithValue("@patente", c.Patente);
                cmdM.Parameters.AddWithValue("@estado", c.Estado);
                cmdM.Parameters.AddWithValue("@pesoMax", c.PesoMax);
                SqlParameter pOut = new SqlParameter();
                pOut.ParameterName = "@idCamion";
                pOut.DbType = DbType.Int32;
                pOut.Direction = ParameterDirection.Output;
                cmdM.Parameters.Add(pOut);
                cmdM.ExecuteNonQuery();

                int idCamion = (int)pOut.Value;

                foreach(Carga cg in c.Cargas)
                {
                    SqlCommand cmdD = new SqlCommand();
                    cmdD.Connection = cnn;
                    cmdD.Transaction = t;
                    cmdD.CommandType = CommandType.StoredProcedure;
                    cmdD.CommandText = "sp_insertar_detalle";
                    cmdD.Parameters.AddWithValue("@peso", cg.Peso);
                    cmdD.Parameters.AddWithValue("@idTipo", cg.Tipo.IdTipo);
                    cmdD.Parameters.AddWithValue("idCamion", idCamion);
                    cmdD.ExecuteNonQuery();
                }
                t.Commit();
            }
            catch(Exception)
            {
                if(t != null)
                {
                    t.Rollback();
                    ok = false;
                }
            }
            finally
            {
                if(cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return ok;

        }
    }
}
