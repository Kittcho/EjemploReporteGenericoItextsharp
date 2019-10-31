using ConsoleCreaPdfJson.Enumeradores;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCreaPdfJson.Interfaces
{
    interface IReporte
    {
        /// <summary>
        /// Crea el reporte PDF a partir de un DataTable
        /// </summary>
        /// <param name="rutaCreacion">Ruta completa donde se creará el reporte PDF, esta debe contener el nombre del archivo con la extesión '.pdf'</param>
        /// <param name="titulo">Titulo a imprimir dentro del reporte</param>
        /// <param name="dtDatos">DataTable con la información a imprimir en el reporte</param>
        void CrearReportePdf(EnumOrientacionHoja orientacion, string rutaCreacion, string titulo, DataTable dtDatos);
    }
}
