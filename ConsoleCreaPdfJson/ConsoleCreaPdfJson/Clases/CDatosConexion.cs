using ConsoleCreaPdfJson.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCreaPdfJson
{
    public class CDatosConexion
    {
        public string ipServidor { get; set; }
        public string BaseDeDatos { get; set; }
        public string puerto { get; set; }
        public string usuario { get; set; }
        public string contrasenia { get; set; }
        public EnumManejadoresBaseDeDatos opcManejadorBaseDeDatos { get; set; }

        public string RegresaCadenadeConexion()
        {
            string cadenaDeConexion = "";
            switch (opcManejadorBaseDeDatos)
            {
                case EnumManejadoresBaseDeDatos.SqlServer:
                    cadenaDeConexion = string.Format("Server={0}, {1};Database={2};User Id={3};Password = {4};",
                                                       this.ipServidor
                                                      ,this.puerto
                                                      ,this.BaseDeDatos
                                                      ,this.usuario
                                                      ,this.contrasenia);
                    break;
                case EnumManejadoresBaseDeDatos.Postgresql:
                    cadenaDeConexion = string.Format("User ID={0};Password={1};Host={2};Port={3};Database={4};",
                                                       this.usuario
                                                      ,this.contrasenia
                                                      ,this.ipServidor
                                                      ,this.puerto
                                                      ,this.BaseDeDatos);
                    break;
            }
            return cadenaDeConexion;
        }
    }
}
