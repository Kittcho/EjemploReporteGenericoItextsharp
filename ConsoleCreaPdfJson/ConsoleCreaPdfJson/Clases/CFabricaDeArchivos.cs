using ConsoleCreaPdfJson.Enumeradores;
using ConsoleCreaPdfJson.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCreaPdfJson.Clases
{
    static class CFabricaDeArchivos
    {
        /// <summary>
        /// Método estatico para crear reportes
        /// </summary>
        /// <param name="tipoPdf">Tipo de objeto de reporte a crear</param>
        /// <returns></returns>
        public static IReporte CreaObjetoDeReporte(EnumTipoPdf tipoPdf)
        {
            IReporte reporte = null;
            switch (tipoPdf)
            {
                case EnumTipoPdf.generico:
                    reporte = new CReportePdf();
                    break;
            }
            return reporte;
        }
    }
}
