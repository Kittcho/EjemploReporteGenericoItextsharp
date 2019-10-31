using ConsoleCreaPdfJson.Clases;
using ConsoleCreaPdfJson.Enumeradores;
using ConsoleCreaPdfJson.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCreaPdfJson
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Leer argumentos
                Console.WriteLine("Obteniendo los parametros de entrada...");
                CParametrosDeEntrada param = JsonConvert.DeserializeObject<CParametrosDeEntrada>(args[0].ToString());

                //Consultando datos
                Console.WriteLine("Obteniendo los datos de la consulta...");
                ConexionBD con = new ConexionBD();
                DataTable dt = con.ConsultaInformaciónDt(param.datosConexion, param.consulta);

                //Generando reporte pdf
                Console.WriteLine("Creando el archivo '{0}'...",param.nom_archivo);
                IReporte reporte = CFabricaDeArchivos.CreaObjetoDeReporte(param.tipoPdf);
                reporte.CrearReportePdf(param.orientacionHoja, Path.Combine(param.rutaArchivo,param.nom_archivo), param.tituloArchivo, dt);

                Console.WriteLine("====== Generación de reporte finalizado ======");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }finally
            {
                Console.ReadKey();
            }
        }

        /*
         CParametrosDeEntrada paramJson = new CParametrosDeEntrada()
                {
                    tipoPdf = EnumTipoPdf.generico,
                    datosConexion = new CDatosConexion()
                    {
                        ipServidor = "localhost",
                        puerto = "1433",
                        BaseDeDatos = "AdventureWorksLT2008R2",
                        usuario = "operacion",
                        contrasenia = "1316431",
                        opcManejadorBaseDeDatos = EnumManejadoresBaseDeDatos.SqlServer
                    },
                    orientacionHoja = EnumOrientacionHoja.horizontal,
                    nom_archivo = "ArchivoCreadoDesdeJson.pdf",
                    rutaArchivo = @"C:\Users\pepe_\OneDrive\Documentos\Archivos",
                    tituloArchivo = "Detalles de ordenes",
                    consulta = "select * from SalesLT.SalesOrderDetail"
                };
                string json = JsonConvert.SerializeObject(paramJson);
         */
    }
}
