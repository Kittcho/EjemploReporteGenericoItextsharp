using ConsoleCreaPdfJson.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCreaPdfJson.Clases
{
    class CParametrosDeEntrada
    {
        public EnumTipoPdf tipoPdf { get; set; }
        public CDatosConexion datosConexion { get; set; }
        public EnumOrientacionHoja orientacionHoja { get; set; }
        public string nom_archivo { get; set; }
        public string rutaArchivo { get; set; }
        public string tituloArchivo { get; set; }
        public string consulta { get; set; }
    }
}
