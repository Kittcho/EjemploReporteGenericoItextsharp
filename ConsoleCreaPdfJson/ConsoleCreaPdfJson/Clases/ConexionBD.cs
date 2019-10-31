using ConsoleCreaPdfJson.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Devart.Data.PostgreSql;

namespace ConsoleCreaPdfJson.Clases
{
    public class ConexionBD : IConexion
    {
        public DataTable ConsultaInformaciónDt(CDatosConexion datosConexion, string consulta)
        {
            DataTable dt = new DataTable();
            switch (datosConexion.opcManejadorBaseDeDatos)
            {
                case Enumeradores.EnumManejadoresBaseDeDatos.SqlServer:
                    using (SqlConnection con = new SqlConnection(datosConexion.RegresaCadenadeConexion()))
                    {
                        SqlCommand comando = new SqlCommand(consulta, con);
                        comando.CommandType = CommandType.Text;
                        SqlDataAdapter adapter = new SqlDataAdapter(comando);
                        adapter.Fill(dt);
                    }
                    break;
                case Enumeradores.EnumManejadoresBaseDeDatos.Postgresql:
                    using (PgSqlConnection con = new PgSqlConnection(datosConexion.RegresaCadenadeConexion()))
                    {
                        PgSqlCommand comando = new PgSqlCommand(consulta, con);
                        comando.CommandType = CommandType.Text;
                        PgSqlDataAdapter adapter = new PgSqlDataAdapter(comando);
                        adapter.Fill(dt);
                    }
                    break;
                default:
                    throw new Exception(string.Format("Manejador de base de datos número '{0}' no es soportado. Favor de revisar los manejadores soportados por este sistema."));
            }

            return dt;
        }
    }
}
